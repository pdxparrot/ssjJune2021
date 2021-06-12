using UnityEngine;

using pdxpartyparrot.Core.Camera;
using pdxpartyparrot.Game;
using pdxpartyparrot.ssjjune2021.Camera;
using pdxpartyparrot.ssjjune2021.Data;
using pdxpartyparrot.ssjjune2021.Level;

namespace pdxpartyparrot.ssjjune2021
{
    public sealed class GameManager : GameManager<GameManager>
    {
        public GameData GameGameData => (GameData)GameData;

        public IBaseLevel BaseLevel => (IBaseLevel)LevelHelper;

        public GameViewer Viewer { get; private set; }

        public void InitViewer()
        {
            Viewer = ViewerManager.Instance.AcquireViewer<GameViewer>();
            if(null == Viewer) {
                Debug.LogWarning("Unable to acquire game viewer!");
                return;
            }
            Viewer.Initialize(GameGameData);
        }
    }
}
