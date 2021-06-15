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
            GetComponent<Collider>().isTrigger = true;
        }

        #endregion

        public void Collect()
        {
            _collected = true;

            gameObject.SetActive(false);
        }
    }
}
