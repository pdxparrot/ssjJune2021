using UnityEngine;

using pdxpartyparrot.Game.Level;

namespace pdxpartyparrot.ssjjune2021.Level
{
    // TODO: just an example
    public sealed class TestLevel : LevelHelper, IBaseLevel
    {
        [SerializeField]
        private int _memoryFragmentsCollected = 0;

        public int MemoryFragmentsCollected
        {
            get => _memoryFragmentsCollected;
            private set => _memoryFragmentsCollected = value;
        }
    }
}
