using System;

using UnityEngine;

using pdxpartyparrot.Core.Time;
using pdxpartyparrot.Core.UI;
using pdxpartyparrot.Core.Util;
using pdxpartyparrot.Game.Cinematics;
using pdxpartyparrot.Game.State;

namespace pdxpartyparrot.ssjjune2021.UI
{
    [RequireComponent(typeof(UIObject))]
    public sealed class LevelSelect : Game.UI.LevelSelectUI
    {
        [SerializeField]
        private Dialogue _introDialoguePrefab;

        [SerializeField]
        private float _introDelay = 0.5f;

        [SerializeField]
        [ReadOnly]
        private ITimer _introTimer;

        #region Unity Lifecycle

        protected override void Awake()
        {
            base.Awake();

            _introTimer = TimeManager.Instance.AddTimer();
            _introTimer.TimesUpEvent += IntroTimesUpEventHandler;

            bool completed = true;

            // setup the initial level state
            // and button handlers
            foreach(Level level in Levels) {
                if(GameManager.Instance.IsLevelCompleted(level.name)) {
                    level.Complete();
                } else if(level.enabled) {
                    completed = false;

                    level.button.onClick.AddListener(() => {
                        GameStateManager.Instance.StartLocal(GameManager.Instance.GameData.MainGameStatePrefab, state => {
                            state.OverrideCurrentScene(level.name);
                        });
                    });

                    EnableButtonInteract(level.button, GameManager.Instance.IntroShown);
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

        #region Event Handlers

        private void IntroTimesUpEventHandler(object sender, EventArgs args)
        {
            // enable the buttons that should be enabled
            foreach(Level level in Levels) {
                if(!GameManager.Instance.IsLevelCompleted(level.name) && level.enabled) {
                    EnableButtonInteract(level.button, true);
                }
            }
        }

        #endregion
    }
}
