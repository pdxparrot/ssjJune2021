using System;

using UnityEngine;

using pdxpartyparrot.Core.DebugMenu;
using pdxpartyparrot.Core.Util;
using pdxpartyparrot.Game.State;

namespace pdxpartyparrot.Game.Cinematics
{
    public sealed class DialogueManager : SingletonBehavior<DialogueManager>
    {
        [SerializeField]
        [ReadOnly]
        private Dialogue _currentDialogue;

        public bool ShowingDialogue => null != _currentDialogue;

        private Action _onComplete;

        private Action _onCancel;

        #region Unity Lifecycle

        private void Awake()
        {
            InitDebugMenu();
        }

        #endregion

        public void ShowDialogue(Dialogue dialoguePrefab, Action onComplete, Action onCancel = null)
        {
            if(null == dialoguePrefab) {
                onComplete?.Invoke();

                _onComplete = null;
                _onCancel = null;
                return;
            }

            _currentDialogue = GameStateManager.Instance.GameUIManager.InstantiateUIPrefab(dialoguePrefab);
            _onComplete = onComplete;
            _onCancel = onCancel;
        }

        public void AdvanceDialogue()
        {
            if(!ShowingDialogue) {
                return;
            }

            Dialogue previousDialogue = _currentDialogue;
            _currentDialogue = null;

            ShowDialogue(previousDialogue.NextDialogue, _onComplete, _onCancel);

            Destroy(previousDialogue.gameObject);
        }

        public void CancelDialogue()
        {
            if(!ShowingDialogue) {
                return;
            }

            Destroy(_currentDialogue.gameObject);
            _currentDialogue = null;

            _onCancel?.Invoke();

            _onComplete = null;
            _onCancel = null;
        }

        private void InitDebugMenu()
        {
            DebugMenuNode debugMenuNode = DebugMenuManager.Instance.AddNode(() => "Core.DialogueManager");
            debugMenuNode.RenderContentsAction = () => {
                GUILayout.Label(ShowingDialogue ? $"Showing dialogue ${_currentDialogue.name}" : "Not showing dialogue");
            };
        }
    }
}
