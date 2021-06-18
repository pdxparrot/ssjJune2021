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

        #region Unity Lifecycle

        private void Awake()
        {
            InitDebugMenu();
        }

        #endregion

        public void ShowDialogue(Dialogue dialoguePrefab, Action onComplete)
        {
            if(null == dialoguePrefab) {
                onComplete?.Invoke();
                _onComplete = null;
                return;
            }

            _currentDialogue = GameStateManager.Instance.GameUIManager.InstantiateUIPrefab(dialoguePrefab);
            _onComplete = onComplete;
        }

        public void AdvanceDialogue()
        {
            if(!ShowingDialogue) {
                return;
            }

            Dialogue previousDialogue = _currentDialogue;
            ShowDialogue(previousDialogue.NextDialogue, _onComplete);
            Destroy(previousDialogue.gameObject);
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
