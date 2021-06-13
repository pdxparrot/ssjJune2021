using UnityEngine;
using UnityEngine.Assertions;

using pdxpartyparrot.Game.Collectables;
using pdxpartyparrot.Game.Data;
using pdxpartyparrot.ssjjune2021.Data;

namespace pdxpartyparrot.ssjjune2021.Items
{
    [RequireComponent(typeof(Collider))]
    public sealed class MemoryFragment : MonoBehaviour, ICollectable
    {
        public enum MemoryFragmentType
        {
            Working,
            Short,
            Long,
        }

        public bool CanBeCollected => true;

        [SerializeField]
        private MemoryFragmentType _fragmentType = MemoryFragmentType.Working;

        public MemoryFragmentType FragmentType => _fragmentType;

        #region Unity Lifecycle

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        #endregion

        public void Initialize(CollectableData data)
        {
            Assert.IsTrue(data is MemoryFragmentData);
        }
    }
}
