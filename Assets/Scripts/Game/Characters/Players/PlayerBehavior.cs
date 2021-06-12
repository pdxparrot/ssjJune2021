using JetBrains.Annotations;

using pdxpartyparrot.Core.Data.Actors.Components;
using pdxpartyparrot.Core.Util;
using pdxpartyparrot.Game.Data.Characters;

using UnityEngine;
using UnityEngine.Assertions;

namespace pdxpartyparrot.Game.Characters.Players
{
    public abstract class PlayerBehavior : CharacterBehavior
    {
        [CanBeNull]
        public PlayerBehaviorData PlayerBehaviorData => (PlayerBehaviorData)CharacterBehaviorData;

        public IPlayer Player => (IPlayer)Owner;

        [Space(10)]

        [SerializeField]
        [ReadOnly]
        private Vector3 _moveDirection;

        public Vector3 MoveDirection => _moveDirection;

        [SerializeField]
        [ReadOnly]
        private Vector3 _lookRotation;

        public Vector3 LookRotation => _lookRotation;

        public virtual Transform LookTarget { get; } = null;

        public float HorizontalLookSpeed => PlayerBehaviorData == null ? 0.0f : PlayerBehaviorData.HorizontalLookSpeed;

        public float VerticalLookSpeed => PlayerBehaviorData == null ? 0.0f : PlayerBehaviorData.VerticalLookSpeed;

        #region Unity Lifecycle

        protected override void Update()
        {
            base.Update();

            float dt = Time.deltaTime;

            // set move direction from input
            Vector3 moveDirection = Vector3.MoveTowards(MoveDirection, Player.PlayerInputHandler.LastMove, dt * Player.PlayerInputHandler.PlayerInputData.MovementLerpSpeed);
            SetMoveDirection(moveDirection);

            // set look rotation from input
            Vector3 lookRotation = Vector3.MoveTowards(LookRotation, Player.PlayerInputHandler.LastLook, dt * Player.PlayerInputHandler.PlayerInputData.LookLerpSpeed);
            SetLookRotation(lookRotation);
        }

        protected virtual void LateUpdate()
        {
            float dt = Time.deltaTime;

            if(null != LookTarget) {
                if(PlayerBehaviorData.AllowLookHorizontal) {
                    float velocity = LookRotation.x * HorizontalLookSpeed;
                    Vector3 rotation = LookTarget.transform.eulerAngles;
                    rotation.y += velocity * dt;

                    LookTarget.transform.eulerAngles = rotation;
                }

                if(PlayerBehaviorData.AllowLookVertical) {
                    float velocity = LookRotation.y * VerticalLookSpeed;
                    Vector3 rotation = LookTarget.transform.eulerAngles;
                    rotation.x += velocity * dt;

                    LookTarget.transform.eulerAngles = rotation;
                }
            }

            Owner.IsMoving = !Mathf.Approximately(MoveDirection.sqrMagnitude, 0.0f);
        }

        #endregion

        public override void Initialize(ActorBehaviorComponentData behaviorData)
        {
            Assert.IsTrue(Owner is IPlayer);
            Assert.IsTrue(behaviorData is PlayerBehaviorData);

            base.Initialize(behaviorData);

            _moveDirection = Vector3.zero;
            _lookRotation = Vector3.zero;
        }

        public virtual void InitializeLocalPlayerBehavior()
        {
        }

        public void SetMoveDirection(Vector3 moveDirection)
        {
            _moveDirection = CanMove ? Vector3.ClampMagnitude(moveDirection, 1.0f) : Vector3.zero;
        }

        public void SetLookRotation(Vector3 lookRotation)
        {
            _lookRotation = lookRotation;
        }

        // TODO: not sure if the alignmovement with viewer code here works anymore

        protected override void AnimationUpdate(float dt)
        {
            if(!CanMove) {
                base.AnimationUpdate(dt);
                return;
            }

            Vector3 forward = MoveDirection;
            if(PlayerBehaviorData.AlignMovementWithViewer && null != Player.Viewer) {
                // align with the camera instead of the movement
                forward = (Quaternion.AngleAxis(LookRotation.y, Vector3.up) * MoveDirection).normalized;
            }

            AlignToMovement(forward);

            if(null != Animator) {
                Animator.SetFloat(CharacterBehaviorData.MoveXAxisParam, CanMove ? Mathf.Abs(MoveDirection.x) : 0.0f);
                Animator.SetFloat(CharacterBehaviorData.MoveZAxisParam, CanMove ? Mathf.Abs(MoveDirection.y) : 0.0f);
            }

            base.AnimationUpdate(dt);
        }

        protected override void PhysicsUpdate(float dt)
        {
            if(!CanMove) {
                base.PhysicsUpdate(dt);
                return;
            }

            if(!CharacterBehaviorData.AllowAirControl && IsFalling) {
                return;
            }

            // TODO: this interferes with forces :(

            Vector3 velocity = MoveDirection * MoveSpeed;
            Quaternion rotation = Owner.Movement.Rotation;
            if(PlayerBehaviorData.AlignMovementWithViewer && null != Player.Viewer) {
                // rotate with the camera instead of the movement
                rotation = Quaternion.AngleAxis(LookRotation.y, Vector3.up);
            }
            velocity = rotation * velocity;

            if(Owner.Movement.IsKinematic) {
                Owner.Movement.Move(velocity * dt);
            } else {
                velocity.y = Owner.Movement.Velocity.y;
                Owner.Movement.Velocity = velocity;
            }

            base.PhysicsUpdate(dt);
        }
    }
}
