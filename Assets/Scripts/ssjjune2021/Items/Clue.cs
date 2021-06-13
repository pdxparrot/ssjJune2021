using System;

using UnityEngine;

using pdxpartyparrot.Game.Interactables;

namespace pdxpartyparrot.ssjjune2021.Items
{
    [RequireComponent(typeof(Collider))]
    public sealed class Clue : MonoBehaviour, IInteractable
    {
        public bool CanInteract => true;

        public Type InteractableType => typeof(Clue);

        [SerializeField]
        private MemoryFragmentType _fragmentType = MemoryFragmentType.Working;

        public MemoryFragmentType FragmentType => _fragmentType;

        #region Unity Lifecycle

        private void Awake()
        {
            GameManager.Instance.BaseLevel.Clues.RegisterClue(this);
        }

        private void OnDestroy()
        {
            if(GameManager.HasInstance && null != GameManager.Instance.BaseLevel) {
                GameManager.Instance.BaseLevel.Clues.UnRegisterClue(this);
            }
        }

        #endregion
    }
}
