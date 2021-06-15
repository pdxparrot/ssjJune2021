using System;

using UnityEngine;

using pdxpartyparrot.Core.Util;
using pdxpartyparrot.Game.Interactables;

namespace pdxpartyparrot.ssjjune2021.World
{
    [RequireComponent(typeof(Collider))]
    public sealed class Exit : MonoBehaviour, IInteractable
    {
        public bool CanInteract => Enabled;

        public Type InteractableType => typeof(Exit);

        [SerializeField]
        [ReadOnly]
        private bool _enabled;

        public bool Enabled
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

        #region Interactions

        public void Interact()
        {
            // TODO: display dialogue / confirmation

            Debug.Log("Exit");
        }

        #endregion

        #region Event Handlers

        private void GameReadyEventHandler(object sender, EventArgs args)
        {
            GameManager.Instance.BaseLevel.RegisterExit(this);
            GameManager.Instance.BaseLevel.Clues.CompleteEvent += CluesCompleteEventHandler;
        }

        private void CluesCompleteEventHandler(object sender, EventArgs args)
        {
            Debug.Log("Exit enabled");

            Enabled = true;
        }

        #endregion
    }
}
