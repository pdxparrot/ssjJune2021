using System;

using UnityEngine;
using UnityEngine.UI;

using pdxpartyparrot.Core.Time;
using pdxpartyparrot.Core.UI;
using pdxpartyparrot.Core.Util;
using pdxpartyparrot.Game.Cinematics;
using pdxpartyparrot.Game.State;

namespace pdxpartyparrot.ssjjune2021.UI
{
    // TODO: could this move down to the Game level?
    [RequireComponent(typeof(UIObject))]
    public sealed class LevelSelect : MonoBehaviour
    {
        [Serializable]
        class Level
        {
            [SerializeField]
            public string name;

            [SerializeField]
            public Image image;

            [SerializeField]
            public Button button;

            [SerializeField]
            public Sprite incomplete;

            [SerializeField]
            public Sprite complete;
        }

        [SerializeField]
        private Level[] _levels;

        [SerializeField]
        private Dialogue _introDialoguePrefab;

        [SerializeField]
        private float _introDelay = 0.5f;

        [SerializeField]
        [ReadOnly]
        private ITimer _introTimer;

        #region Unity Lifecycle

        private void Awake()
        {
            _introTimer = TimeManager.Instance.AddTimer();
            _introTimer.TimesUpEvent += IntroTimesUpEventHandler;

            bool completed = true;

            foreach(Level level in _levels) {
                // hook up the level button onclicks
                if(GameManager.Instance.IsLevelCompleted(level.name)) {
                    level.image.sprite = level.complete;
                    level.button.interactable = false;
                } else {
                    completed = false;

                    level.image.sprite = level.incomplete;

                    level.button.onClick.AddListener(() => {
                        GameStateManager.Instance.StartLocal(GameManager.Instance.GameData.MainGameStatePrefab, state => {
                            state.OverrideCurrentScene(level.name);
                        });
                    });

                    if(GameManager.Instance.IntroShown) {
                        EnableButtonInteract(level.button);
                    } else {
                        level.button.interactable = false;
                    }
                }
            }

            if(completed) {
                GameManager.Instance.GameOver();
            } else if(!GameManager.Instance.IntroShown) {
                DialogueManager.Instance.ShowDialogue(_introDialoguePrefab, () => {
                    // have to delay this so the buttons
                    // don't consume the intro submit action
                    _introTimer.Start(_introDelay);

                    GameManager.Instance.IntroShown = true;
                });
            }
        }

        private void OnDestroy()
        {
            if(TimeManager.HasInstance) {
                TimeManager.Instance.RemoveTimer(_introTimer);
                _introTimer = null;
            }
        }

        #endregion

        private void EnableButtonInteract(Button button)
        {
            button.Select();
            button.Highlight();

            button.interactable = true;
        }

        #region Event Handlers

        private void IntroTimesUpEventHandler(object sender, EventArgs args)
        {
            // enable the buttons that should be enabled
            foreach(Level level in _levels) {
                if(!GameManager.Instance.IsLevelCompleted(level.name)) {
                    EnableButtonInteract(level.button);
                }
            }
        }

        #endregion
    }
}
