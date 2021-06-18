using JetBrains.Annotations;

using UnityEngine;
using UnityEngine.InputSystem;

using pdxpartyparrot.Core.Effects;
using pdxpartyparrot.Core.Input;
using pdxpartyparrot.Core.UI;

namespace pdxpartyparrot.Game.Cinematics
{
    [RequireComponent(typeof(UIObject))]
    public class Dialogue : MonoBehaviour
    {
        [SerializeField]
        private Dialogue _nextDialogue;

        public Dialogue NextDialogue => _nextDialogue;

        [SerializeField]
        private bool _allowCancel;

        #region Effects

        [SerializeField]
        [CanBeNull]
        private EffectTrigger _enableEffect;

        [SerializeField]
        [CanBeNull]
        private EffectTrigger _disableEffect;

        #endregion

        #region Unity Lifecycle

        private void OnEnable()
        {
            InputManager.Instance.EventSystem.UIModule.submit.action.performed += OnSubmit;
            InputManager.Instance.EventSystem.UIModule.cancel.action.performed += OnCancel;

            if(null != _enableEffect) {
                _enableEffect.Trigger();
            }
        }

        private void OnDisable()
        {
            if(null != _disableEffect) {
                _disableEffect.Trigger();
            }

            if(InputManager.HasInstance) {
                InputManager.Instance.EventSystem.UIModule.submit.action.performed -= OnSubmit;
                InputManager.Instance.EventSystem.UIModule.cancel.action.performed += OnCancel;
            }
        }

        #endregion

        #region Event Handlers

        private void OnSubmit(InputAction.CallbackContext context)
        {
            DialogueManager.Instance.AdvanceDialogue();
        }

        private void OnCancel(InputAction.CallbackContext context)
        {
            if(!_allowCancel) {
                return;
            }

            DialogueManager.Instance.CancelDialogue();
        }

        #endregion
    }
}
