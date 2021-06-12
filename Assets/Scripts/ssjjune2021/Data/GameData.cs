using System;

using UnityEngine;

using pdxpartyparrot.ssjjune2021.Camera;
using pdxpartyparrot.ssjjune2021.State;

namespace pdxpartyparrot.ssjjune2021.Data
{
    [CreateAssetMenu(fileName = "GameData", menuName = "pdxpartyparrot/ssjjune2021/Data/Game Data")]
    [Serializable]
    public sealed class GameData : Game.Data.GameData
    {
        public GameViewer GameViewerPrefab => (GameViewer)ViewerPrefab;

        #region Project Game States

        [Header("Project Game States")]

        [SerializeField]
        private LevelSelectState _levelSelectStatePrefab;

        public LevelSelectState LevelSelectStatePrefab => _levelSelectStatePrefab;

        #endregion
    }
}
