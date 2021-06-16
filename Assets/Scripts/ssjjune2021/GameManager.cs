using System.Collections.Generic;

using UnityEngine;

using pdxpartyparrot.Core.Camera;
using pdxpartyparrot.Core.Util;
using pdxpartyparrot.Game;
using pdxpartyparrot.Game.State;
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

        //[SerializeReference]
        [ReadOnly]
        private /*readonly*/ HashSet<string> _completedLevels = new HashSet<string>();

        public void InitViewer()
        {
            Viewer = ViewerManager.Instance.AcquireViewer<GameViewer>();
            if(null == Viewer) {
                Debug.LogWarning("Unable to acquire game viewer!");
                return;
            }
            Viewer.Initialize(GameGameData);
        }

        public void Exit(string level)
        {
            Debug.Log($"Completed level {level}");

            _completedLevels.Add(level);

            GameUnReady();

            GameStateManager.Instance.TransitionStateAsync(GameGameData.LevelSelectStatePrefab);
        }

        public bool IsLevelCompleted(string level)
        {
            return _completedLevels.Contains(level);
        }
    }
}
