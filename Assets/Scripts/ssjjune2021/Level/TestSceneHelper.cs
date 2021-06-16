using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

using pdxpartyparrot.Core.Util;
using pdxpartyparrot.ssjjune2021.Items;
using pdxpartyparrot.ssjjune2021.Players;
using pdxpartyparrot.ssjjune2021.World;

namespace pdxpartyparrot.ssjjune2021.Level
{
    [RequireComponent(typeof(Clues))]
    public sealed class TestSceneHelper : Game.Level.TestSceneHelper, IBaseLevel
    {
        //[SerializeReference]
        [ReadOnly]
        private /*readonly*/ HashSet<MemoryFragment> _memoryFragments = new HashSet<MemoryFragment>();

        private Clues _clues;

        public Clues Clues => _clues;

        private Exit _exit;

        #region Unity Lifecycle

        protected override void Awake()
        {
            base.Awake();

            _clues = GetComponent<Clues>();
        }

        #endregion

        public void RegisterMemoryFragment(MemoryFragment fragment)
        {
            _memoryFragments.Add(fragment);
        }

        public void UnRegisterMemoryFragment(MemoryFragment fragment)
        {
            _memoryFragments.Remove(fragment);
        }

        public void RegisterExit(Exit exit)
        {
            Assert.IsNull(_exit);

            _exit = exit;
        }

        public void UnRegisterExit(Exit exit)
        {
            Assert.IsTrue(_exit == exit);

            _exit = null;
        }

        public void Exit()
        {
            foreach(MemoryFragment fragment in _memoryFragments) {
                fragment.Reset();
            }

            _clues.Reset();
            _exit.Reset();

            foreach(Player player in PlayerManager.Instance.Players) {
                player.Reset();
            }
        }
    }
}
