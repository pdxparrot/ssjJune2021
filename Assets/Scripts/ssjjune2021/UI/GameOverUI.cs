using UnityEngine;
using UnityEngine.InputSystem;

using pdxpartyparrot.Core.Input;
using pdxpartyparrot.Core.UI;
using pdxpartyparrot.Game.State;

namespace pdxpartyparrot.ssjjune2021.UI
{
    [RequireComponent(typeof(UIObject))]
    public sealed class GameOverUI : MonoBehaviour
    {
        #region Unity Lifecycle

        private void OnEnable()
        {
            InputManager.Instance.EventSystem.UIModule.submit.action.performed += OnSubmit;
            InputManager.Instance.EventSystem.UIModule.cancel.action.performed += OnCancel;
        }

        private void OnDisable()
        {
            if(InputManager.HasInstance) {
                InputManager.Instance.EventSystem.UIModule.submit.action.performed -= OnSubmit;
                InputManager.Instance.EventSystem.UIModule.cancel.action.performed -= OnCancel;
            }
        }

        #endregion

        #region Event Handlers

        private void OnSubmit(InputAction.CallbackContext context)
        {
            GameManager.Instance.Reset();

            GameManager.Instance.TransitionToLevelSelect();
        }

        private void OnCancel(InputAction.CallbackContext context)
        {
            GameStateManager.Instance.TransitionToInitialStateAsync();
        }

        #endregion
    }
}
