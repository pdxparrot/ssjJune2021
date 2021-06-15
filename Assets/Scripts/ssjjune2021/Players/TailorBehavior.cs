using System.Collections.Generic;

using UnityEngine;

using pdxpartyparrot.Core.Collections;
using pdxpartyparrot.Core.Util;
using pdxpartyparrot.Game.Interactables;
using pdxpartyparrot.ssjjune2021.Items;

namespace pdxpartyparrot.ssjjune2021.Players
{
    [RequireComponent(typeof(Interactables3D))]
    public sealed class TailorBehavior : MonoBehaviour
    {
        [SerializeReference]
        [ReadOnly]
        private /*readonly*/ Dictionary<MemoryFragmentType, int> _fragments = new Dictionary<MemoryFragmentType, int>();

        #region Unity Lifecycle

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("trigger enter");
        }

        #endregion

        public int CollectFragments(MemoryFragmentType type)
        {
            _fragments.Remove(type, out int count);
            return count;
        }
    }
}
