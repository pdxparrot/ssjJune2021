using System;

using pdxpartyparrot.Core.Effects;
using pdxpartyparrot.Core.World;
using pdxpartyparrot.Game.Characters.Players;
using pdxpartyparrot.Game.World;
using pdxpartyparrot.ssjjune2021.Camera;

using UnityEngine;
using UnityEngine.Assertions;

namespace pdxpartyparrot.ssjjune2021.Players
{
    public sealed class Player : Player3D, IWorldBoundaryCollisionListener
    {
        public PlayerBehavior GamePlayerBehavior => (PlayerBehavior)PlayerBehavior;

        private GameViewer PlayerGameViewer => (GameViewer)Viewer;

        #region Effects

        [SerializeField]
        private EffectTrigger _fallOutEffect;

        #endregion

        #region Unity Lifecycle

        protected override void Awake()
        {
            base.Awake();

            Assert.IsTrue(PlayerInputHandler is PlayerInputHandler);

            Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        #endregion

        public override void Initialize(Guid id)
        {
            base.Initialize(id);

            Assert.IsTrue(PlayerBehavior is PlayerBehavior);
        }

        protected override bool InitializeLocalPlayer(Guid id)
        {
            if(!base.InitializeLocalPlayer(id)) {
                return false;
            }

            PlayerViewer = GameManager.Instance.Viewer;

            return true;
        }

        public void Reset()
        {
            GamePlayerBehavior.TailorBehavior.Reset();
        }

        #region Spawn

        public override bool OnSpawn(SpawnPoint spawnpoint)
        {
            if(!base.OnSpawn(spawnpoint)) {
                return false;
            }

            PlayerGameViewer.FollowTarget(GamePlayerBehavior.LookTarget);

            return true;
        }

        public override void OnDeSpawn()
        {
            PlayerGameViewer.FollowTarget(null);

            base.OnDeSpawn();
        }

        #endregion

        #region Event Handlers

        public void OnPlatformEnter(Transform parent)
        {
            transform.SetParent(parent);

            GamePlayerBehavior.TailorBehavior.OnPlatformEnter();
        }

        public void OnPlatformExit()
        {
            PlayerManager.Instance.ReclaimPlayer(this);

            GamePlayerBehavior.TailorBehavior.OnPlatformExit();
        }

        #endregion

        #region IWorldBoundaryCollisionListener

        public void OnWorldBoundaryCollisionEnter(WorldBoundary boundary)
        {
            Debug.Log("Fell out of the world!");

            //_fallOutEffect.Trigger();
        }

        public void OnWorldBoundaryCollisionExit(WorldBoundary boundary)
        {
            PlayerManager.Instance.RespawnPlayerPosition(this, GamePlayerBehavior.GroundCheckBehaviorComponent.LastGroundedPosition + Vector3.up * PlayerManager.Instance.GamePlayerData.WorldRespawnOffset);
        }

        #endregion
    }
}
