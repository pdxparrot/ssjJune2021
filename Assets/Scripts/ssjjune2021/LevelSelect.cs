using UnityEngine;

using pdxpartyparrot.Game.State;

namespace pdxpartyparrot.ssjjune2021
{
    public sealed class LevelSelect : MonoBehaviour
    {
        #region Event Handlers

        public void OnSelectLevel(string level)
        {
            GameStateManager.Instance.StartLocal(GameManager.Instance.GameData.MainGameStatePrefab);
        }

        #endregion
    }
}
