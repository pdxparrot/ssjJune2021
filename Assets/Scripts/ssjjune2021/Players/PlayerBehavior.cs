using UnityEngine;
using UnityEngine.Assertions;

using pdxpartyparrot.Core.Data.Actors.Components;
using pdxpartyparrot.Game.Characters.BehaviorComponents;
using pdxpartyparrot.ssjjune2021.Data.Players;

namespace pdxpartyparrot.ssjjune2021.Players
{
    [RequireComponent(typeof(TailorBehavior))]
    public sealed class PlayerBehavior : Game.Characters.Players.PlayerBehavior
    {
        [SerializeField]
        private Transform _lookTarget;

        public override Transform LookTarget => _lookTarget;

        public PlayerBehaviorData GamePlayerBehaviorData => (PlayerBehaviorData)PlayerBehaviorData;

        public TailorBehavior TailorBehavior { get; private set; }

        private GroundCheckBehaviorComponent _groundCheckBehaviorComponent;

        public GroundCheckBehaviorComponent GroundCheckBehaviorComponent => _groundCheckBehaviorComponent;

        public override void Initialize(ActorBehaviorComponentData behaviorData)
        {
            Assert.IsTrue(Owner is Player);
            Assert.IsTrue(behaviorData is PlayerBehaviorData);

            base.Initialize(behaviorData);

            TailorBehavior = GetComponent<TailorBehavior>();
            TailorBehavior.Initialize();

            _groundCheckBehaviorComponent = GetBehaviorComponent<GroundCheckBehaviorComponent>();
        }
    }
}
