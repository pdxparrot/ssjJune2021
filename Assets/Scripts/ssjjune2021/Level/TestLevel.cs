using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using pdxpartyparrot.Core.Util;
using pdxpartyparrot.Game.Level;
using pdxpartyparrot.ssjjune2021.Items;
using pdxpartyparrot.ssjjune2021.World;

namespace pdxpartyparrot.ssjjune2021.Level
{
    // TODO: just an example
    [RequireComponent(typeof(Clues))]
    public sealed class TestLevel : LevelHelper, IBaseLevel
    {
        [SerializeField]
        private Key _cheatKey = Key.T;

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

        private void FixedUpdate()
        {
#if UNITY_EDITOR
            if(Keyboard.current[_cheatKey].wasPressedThisFrame) {
                GameManager.Instance.Exit(SceneManager.GetActiveScene().name);
            }
#endif
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
            GameManager.Instance.Exit(SceneManager.GetActiveScene().name);
        }

        #region Event Handlers

        protected override void GameStartClientEventHandler(object sender, EventArgs args)
        {
            GameManager.Instance.Reset();

            base.GameStartClientEventHandler(sender, args);
        }

        #endregion
    }
}
