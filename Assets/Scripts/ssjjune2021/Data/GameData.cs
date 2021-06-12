using System;

using UnityEngine;

using pdxpartyparrot.ssjjune2021.Camera;

namespace pdxpartyparrot.ssjjune2021.Data
{
    [CreateAssetMenu(fileName = "GameData", menuName = "pdxpartyparrot/ssjjune2021/Data/Game Data")]
    [Serializable]
    public sealed class GameData : Game.Data.GameData
    {
        public GameViewer GameViewerPrefab => (GameViewer)ViewerPrefab;
    }
}
