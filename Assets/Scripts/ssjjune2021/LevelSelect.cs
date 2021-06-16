using System;

using UnityEngine;
using UnityEngine.UI;

using pdxpartyparrot.Game.State;

namespace pdxpartyparrot.ssjjune2021
{
    public sealed class LevelSelect : MonoBehaviour
    {
        [Serializable]
        class Level
        {
            [SerializeField]
            public string name;

            [SerializeField]
            public Button button;
        }

        [SerializeField]
        private Level[] _levels;

        #region Unity Lifecycle

        private void Awake()
        {
            foreach(Level level in _levels) {
                if(GameManager.Instance.IsLevelCompleted(level.name)) {
                    level.button.interactable = false;
                } else {
                    level.button.onClick.AddListener(() => {
                        GameStateManager.Instance.StartLocal(GameManager.Instance.GameData.MainGameStatePrefab, state => {
                            state.OverrideCurrentScene(level.name);
                        });
                    });
                }
            }
        }

        #endregion
    }
}
