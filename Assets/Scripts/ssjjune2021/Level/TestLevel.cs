using System;

using UnityEngine;
using UnityEngine.Assertions;

using pdxpartyparrot.Game.Level;
using pdxpartyparrot.ssjjune2021.Items;

namespace pdxpartyparrot.ssjjune2021.Level
{
    // TODO: just an example
    [RequireComponent(typeof(Clues))]
    public sealed class TestLevel : LevelHelper, IBaseLevel
    {
        private Clues _clues;

        public Clues Clues => _clues;

        private Exit _exit;

        #region Unity Lifecycle

        protected override void Awake()
        {
            base.Awake();

            _clues = GetComponent<Clues>();
            _clues.CompleteEvent += CluesCompleteEventHandler;
        }

        #endregion

        public void RegisterExit(Exit exit)
        {
            Assert.IsNull(_exit);

            _exit = exit;
            _exit.Enabled = false;
        }

        public void UnRegisterExit(Exit exit)
        {
            Assert.IsTrue(_exit == exit);

            _exit = null;
        }

        #region Event Handlers

        private void CluesCompleteEventHandler(object sender, EventArgs args)
        {
            _exit.Enabled = true;
        }

        #endregion
    }
}
