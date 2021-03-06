using System;
using System.Collections.Generic;

using UnityEngine;

using pdxpartyparrot.Core.Collections;
using pdxpartyparrot.Core.Effects;
using pdxpartyparrot.Core.Effects.EffectTriggerComponents;
using pdxpartyparrot.Core.Util;
using pdxpartyparrot.Game.Interactables;
using pdxpartyparrot.ssjjune2021.Items;
using pdxpartyparrot.ssjjune2021.UI;
using pdxpartyparrot.ssjjune2021.World;

namespace pdxpartyparrot.ssjjune2021.Players
{
    [RequireComponent(typeof(Interactables3D))]
    public sealed class TailorBehavior : MonoBehaviour
    {
        [SerializeField]
        private Player _owner;

        public Player Owner => _owner;

        [SerializeReference]
        [ReadOnly]
        private /*readonly*/ Dictionary<MemoryFragmentType, int> _fragments = new Dictionary<MemoryFragmentType, int>();

        [SerializeField]
        [ReadOnly]
        private bool _onPlatform;

        private Interactables3D _interactables;

        #region Effects

        [SerializeField]
        private EffectTrigger _levelEnteredEffect;

        [SerializeField]
        private EffectTrigger _collectEffect;

        [SerializeField]
        private RumbleEffectTriggerComponent[] _rumbleEffects;

        #endregion

        #region Unity Lifecycle

        private void Awake()
        {
            _interactables = GetComponent<Interactables3D>();

            GameManager.Instance.LevelEnterEvent += LevelEnterEventHandler;
        }

        private void OnDestroy()
        {
            if(GameManager.HasInstance) {
                GameManager.Instance.LevelEnterEvent -= LevelEnterEventHandler;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            MemoryFragment fragment = other.GetComponent<MemoryFragment>();
            if(null != fragment) {
                CollectFragment(fragment);
                return;
            }
        }

        #endregion

        public void Initialize()
        {
            foreach(RumbleEffectTriggerComponent rumble in _rumbleEffects) {
                rumble.PlayerInput = Owner.PlayerInputHandler.InputHelper;
            }
        }

        public void Reset()
        {
            _fragments.Clear();
        }

        public void Interact()
        {
            if(InteractExit()) {
                return;
            }

            if(InteractClue()) {
                return;
            }
        }

        private bool InteractExit()
        {
            Exit exit = _interactables.GetFirstInteractable<Exit>();
            if(exit == null || !exit.CanInteract) {
                return false;
            }

            exit.Interact();
            return true;
        }

        private bool InteractClue()
        {
            Clue clue = _interactables.GetFirstInteractable<Clue>();
            if(clue == null || !clue.CanInteract) {
                return false;
            }

            clue.Interact(this);
            return true;
        }

        private void CollectFragment(MemoryFragment fragment)
        {
            if(!fragment.CanCollect) {
                return;
            }

            Debug.Log($"Collecting fragment of type {fragment.FragmentType}");
            fragment.Collect();

            int count = _fragments.GetOrAdd(fragment.FragmentType);
            _fragments[fragment.FragmentType] = count + 1;

            GameUIManager.Instance.GameGameUI.PlayerHUD.UpdateMemoryFragments(fragment.FragmentType, _fragments[fragment.FragmentType]);

            _collectEffect.Trigger();
        }

        public int AssembleFragments(MemoryFragmentType fragmentType, int max)
        {
            int count = Mathf.Min(_fragments.GetOrAdd(fragmentType), max);
            if(count < 1) {
                return 0;
            }

            Debug.Log($"Assembling {count} fragments of type {fragmentType}");

            _fragments[fragmentType] -= count;
            GameUIManager.Instance.GameGameUI.PlayerHUD.UpdateMemoryFragments(fragmentType, _fragments[fragmentType]);

            return count;
        }

        #region Event Handlers

        private void LevelEnterEventHandler(object sender, EventArgs args)
        {
            _levelEnteredEffect.Trigger();
        }

        public void OnPlatformEnter()
        {
            _onPlatform = true;
        }

        public void OnPlatformExit()
        {
            _onPlatform = false;
        }

        #endregion
    }
}
