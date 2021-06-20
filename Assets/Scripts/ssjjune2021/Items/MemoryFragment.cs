using System;

using UnityEngine;

using pdxpartyparrot.Core.Effects;
using pdxpartyparrot.Core.Util;

namespace pdxpartyparrot.ssjjune2021.Items
{
    [RequireComponent(typeof(Collider))]
    public sealed class MemoryFragment : MonoBehaviour
    {
        [SerializeField]
        private GameObject _model;

        [SerializeField]
        private MemoryFragmentType _fragmentType = MemoryFragmentType.Working;

        public MemoryFragmentType FragmentType => _fragmentType;

        [SerializeField]
        [ReadOnly]
        private bool _collected;

        public bool CanCollect => !_collected;

        #region Effects

        [SerializeField]
        private EffectTrigger _collectEffect;

        #endregion

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
                    GameManager.Instance.BaseLevel.UnRegisterMemoryFragment(this);
                }
            }
        }

        #endregion

        public void Reset()
        {
            _collected = false;

            _model.SetActive(true);
        }

        public void Collect()
        {
            _collected = true;

            _model.SetActive(false);

            _collectEffect.Trigger();
        }

        #region Event Handlers

        private void GameReadyEventHandler(object sender, EventArgs args)
        {
            GameManager.Instance.BaseLevel.RegisterMemoryFragment(this);
        }

        #endregion
    }
}
