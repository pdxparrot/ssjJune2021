using pdxpartyparrot.ssjjune2021.Players;

using UnityEngine;

namespace pdxpartyparrot.ssjjune2021.World
{
    [RequireComponent(typeof(Collider))]
    public sealed class Platform : Game.World.Platform
    {
        #region Unity Lifecycle

        protected override void OnDestroy()
        {
            // this may not actually be safe to do here
            for(int i = 0; i < ActorContainer.childCount; ++i) {
                Transform child = ActorContainer.GetChild(i);

                Player player = child.GetComponentInParent<Player>();
                if(null != player) {
                    OnPlayerExit(player);
                    continue;
                }
            }

            base.OnDestroy();
        }

        #endregion

        protected override void OnEnterPlatform(GameObject gameObject)
        {
            Player player = gameObject.GetComponentInParent<Player>();
            if(null != player) {
                OnPlayerEnter(player);
                return;
            }
        }

        protected override void OnExitPlatform(GameObject gameObject)
        {
            Player player = gameObject.GetComponentInParent<Player>();
            if(null != player) {
                OnPlayerExit(player);
                return;
            }
        }

        private void OnPlayerEnter(Player player)
        {
            player.OnPlatformEnter(ActorContainer);
        }

        private void OnPlayerExit(Player player)
        {
            player.OnPlatformExit();
        }
    }
}
