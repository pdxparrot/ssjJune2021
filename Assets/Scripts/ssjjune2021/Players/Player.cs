using System;

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

        #region Spawn

        public override bool OnSpawn(SpawnPoint spawnpoint)
        {
            if(!base.OnSpawn(spawnpoint)) {
                return false;
            }

            PlayerGameViewer.FollowTarget(gameObject);

            return true;
        }

        public override void OnDeSpawn()
        {
            PlayerGameViewer.FollowTarget(null);

            base.OnDeSpawn();
        }

        #endregion

        #region IWorldBoundaryCollisionListener

        public void OnWorldBoundaryCollisionEnter(WorldBoundary boundary)
        {
        }

        public void OnWorldBoundaryCollisionExit(WorldBoundary boundary)
        {
        }

        #endregion
    }
}
