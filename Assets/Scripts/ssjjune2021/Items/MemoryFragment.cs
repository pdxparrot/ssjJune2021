using System;

using UnityEngine;

using pdxpartyparrot.Core.Util;

namespace pdxpartyparrot.ssjjune2021.Items
{
    [RequireComponent(typeof(Collider))]
    public sealed class MemoryFragment : MonoBehaviour
    {
        public bool CanBeCollected => !_collected;

        [SerializeField]
        [ReadOnly]
        private bool _collected;

        [SerializeField]
        private MemoryFragmentType _fragmentType = MemoryFragmentType.Working;

        public MemoryFragmentType FragmentType => _fragmentType;

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

            gameObject.SetActive(true);
        }

        public void Collect()
        {
            _collected = true;

            gameObject.SetActive(false);
        }

        #region Event Handlers

        private void GameReadyEventHandler(object sender, EventArgs args)
        {
            GameManager.Instance.BaseLevel.RegisterMemoryFragment(this);
        }

        #endregion
    }
}
