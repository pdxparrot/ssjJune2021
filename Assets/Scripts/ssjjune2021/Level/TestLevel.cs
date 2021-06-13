using System;

using UnityEngine;

using pdxpartyparrot.Game.Level;

namespace pdxpartyparrot.ssjjune2021.Level
{
    // TODO: just an example
    [RequireComponent(typeof(Clues))]
    public sealed class TestLevel : LevelHelper, IBaseLevel
    {
        private Clues _clues;

        public Clues Clues => _clues;

        #region Unity Lifecycle

        protected override void Awake()
        {
            _clues = GetComponent<Clues>();
            _clues.CompleteEvent += CluesCompleteEventHandler;
        }

        #endregion

        #region Event Handlers

        private void CluesCompleteEventHandler(object sender, EventArgs args)
        {
            Debug.Log("clues are complete, enable exit");
        }

        #endregion
    }
}
