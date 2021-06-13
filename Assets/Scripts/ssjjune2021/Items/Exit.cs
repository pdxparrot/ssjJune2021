using System;

using UnityEngine;

using pdxpartyparrot.Core.Util;
using pdxpartyparrot.Game.Interactables;

namespace pdxpartyparrot.ssjjune2021.Items
{
    [RequireComponent(typeof(Collider))]
    public sealed class Exit : MonoBehaviour, IInteractable
    {
        public bool CanInteract => true;

        public Type InteractableType => typeof(Exit);

        [SerializeField]
        [ReadOnly]
        private bool _enabled;

        public bool Enabled
        {
            get => _enabled;
            set => _enabled = value;
        }

        #region Unity Lifecycle

        private void Awake()
        {
            GameManager.Instance.BaseLevel.RegisterExit(this);

            GetComponent<Collider>().isTrigger = true;
        }

        private void OnDestroy()
        {
            if(GameManager.HasInstance && null != GameManager.Instance.BaseLevel) {
                GameManager.Instance.BaseLevel.UnRegisterExit(this);
            }
        }

        #endregion

        #region Interactions

        public void Interact()
        {
            // TODO: display dialogue / confirmation
        }

        #endregion
    }
}
