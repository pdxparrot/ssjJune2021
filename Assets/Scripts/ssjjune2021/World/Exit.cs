using System;

using UnityEngine;

using pdxpartyparrot.Core.Util;
using pdxpartyparrot.Game.Cinematics;
using pdxpartyparrot.Game.Interactables;

namespace pdxpartyparrot.ssjjune2021.World
{
    [RequireComponent(typeof(Collider))]
    public sealed class Exit : MonoBehaviour, IInteractable
    {
        public bool CanInteract => IsEnabled;

        public Type InteractableType => typeof(Exit);

        [SerializeField]
        private Dialogue _dialoguePrefab;

        [SerializeField]
        [ReadOnly]
        private bool _enabled;

        public bool IsEnabled
        {
            get => _enabled;
            private set => _enabled = value;
        }

        #region Unity Lifecycle

        private void Awake()
        {
            GameManager.Instance.GameReadyEvent += GameReadyEventHandler;

            GetComponent<Collider>().isTrigger = true;
        }

        private void OnDestroy()
        {
            if(GameManager.HasInstance) {
                GameManager.Instance.GameReadyEvent -= GameReadyEventHandler;

                if(null != GameManager.Instance.BaseLevel) {
                    GameManager.Instance.BaseLevel.Clues.CompleteEvent -= CluesCompleteEventHandler;
                    GameManager.Instance.BaseLevel.UnRegisterExit(this);
                }
            }
        }

        #endregion

        public void Reset()
        {
            Debug.Log("Resetting exit...");

            IsEnabled = false;
            gameObject.SetActive(false);
        }

        public void Interact()
        {
            // TODO: should be a message box
            DialogueManager.Instance.ShowDialogue(_dialoguePrefab, () => {
                GameManager.Instance.BaseLevel.Exit();
            });
        }

        #region Event Handlers

        private void GameReadyEventHandler(object sender, EventArgs args)
        {
            GameManager.Instance.BaseLevel.RegisterExit(this);
            GameManager.Instance.BaseLevel.Clues.CompleteEvent += CluesCompleteEventHandler;

            gameObject.SetActive(false);
        }

        private void CluesCompleteEventHandler(object sender, EventArgs args)
        {
            Debug.Log("Exit enabled");

            IsEnabled = true;
            gameObject.SetActive(true);
        }

        #endregion
    }
}
