using UnityEngine;
using UnityEngine.Assertions;

using pdxpartyparrot.Core.Data.Actors.Components;
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

        private TailorBehavior _tailorBehavior;

        public override void Initialize(ActorBehaviorComponentData behaviorData)
        {
            Assert.IsTrue(Owner is Player);
            Assert.IsTrue(behaviorData is PlayerBehaviorData);

            base.Initialize(behaviorData);

            _tailorBehavior = GetComponent<TailorBehavior>();
        }
    }
}
