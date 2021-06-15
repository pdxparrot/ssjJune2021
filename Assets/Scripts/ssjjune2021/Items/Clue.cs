using System;

using UnityEngine;

using pdxpartyparrot.Core.Util;
using pdxpartyparrot.Game.Interactables;
using pdxpartyparrot.ssjjune2021.Players;

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

        [SerializeField]
        private int _requiredFragments = 1;

        [SerializeField]
        [ReadOnly]
        private int _collectedFragments;

        [SerializeField]
        [TextArea]
        private string _dialogueText;

        public bool IsComplete => _collectedFragments >= _requiredFragments;

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
                    GameManager.Instance.BaseLevel.Clues.UnRegisterClue(this);
                }
            }
        }

        #endregion

        #region Interactions

        public void Interact(TailorBehavior tailor)
        {
            _collectedFragments += tailor.CollectFragments(FragmentType);

            // TODO: display dialogue
        }

        #endregion

        #region Event Handlers

        private void GameReadyEventHandler(object sender, EventArgs args)
        {
            GameManager.Instance.BaseLevel.Clues.RegisterClue(this);
        }

        #endregion
    }
}
