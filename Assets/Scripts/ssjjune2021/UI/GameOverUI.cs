using UnityEngine;
using UnityEngine.InputSystem;

using pdxpartyparrot.Core.Input;
using pdxpartyparrot.Core.Time;
using pdxpartyparrot.Core.UI;
using pdxpartyparrot.Core.Util;
using pdxpartyparrot.Game.State;

namespace pdxpartyparrot.ssjjune2021.UI
{
    [RequireComponent(typeof(UIObject))]
    public sealed class GameOverUI : MonoBehaviour
    {
        [SerializeField]
        private float _delay = 0.5f;

        [SerializeField]
        [ReadOnly]
        private ITimer _delayTimer;

        #region Unity Lifecycle

        private void Awake()
        {
            _delayTimer = TimeManager.Instance.AddTimer();
        }

        private void OnDestroy()
        {
            if(TimeManager.HasInstance) {
                TimeManager.Instance.RemoveTimer(_delayTimer);
                _delayTimer = null;
            }
        }

        private void OnEnable()
        {
            _delayTimer.Start(_delay);

            InputManager.Instance.EventSystem.UIModule.submit.action.performed += OnSubmit;
            InputManager.Instance.EventSystem.UIModule.cancel.action.performed += OnCancel;
        }

        private void OnDisable()
        {
            _delayTimer.Stop();

            if(InputManager.HasInstance) {
                InputManager.Instance.EventSystem.UIModule.submit.action.performed -= OnSubmit;
                InputManager.Instance.EventSystem.UIModule.cancel.action.performed -= OnCancel;
            }
        }

        #endregion

        #region Event Handlers

        private void OnSubmit(InputAction.CallbackContext context)
        {
            if(_delayTimer.IsRunning) {
                return;
            }

            GameManager.Instance.Reset();

            GameManager.Instance.TransitionToLevelSelect();
        }

        private void OnCancel(InputAction.CallbackContext context)
        {
            if(_delayTimer.IsRunning) {
                return;
            }

            GameManager.Instance.Reset();

            GameStateManager.Instance.TransitionToInitialStateAsync();
        }

        #endregion
    }
}
