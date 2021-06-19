using System;

using UnityEngine;
using UnityEngine.UI;

using pdxpartyparrot.Core.UI;
using pdxpartyparrot.Game.State;

namespace pdxpartyparrot.ssjjune2021
{
    // TODO:: can this be core?
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
            bool completed = true;

            foreach(Level level in _levels) {
                if(GameManager.Instance.IsLevelCompleted(level.name)) {
                    level.button.interactable = false;
                } else {
                    completed = false;
                    level.button.onClick.AddListener(() => {
                        GameStateManager.Instance.StartLocal(GameManager.Instance.GameData.MainGameStatePrefab, state => {
                            state.OverrideCurrentScene(level.name);
                        });
                    });

                    level.button.Select();
                    level.button.Highlight();
                }
            }

            if(completed) {
                GameManager.Instance.GameOver();

                // TODO: hide the UI
            }
        }

        #endregion
    }
}
