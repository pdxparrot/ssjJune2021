using UnityEngine;
using UnityEngine.Assertions;

using pdxpartyparrot.ssjjune2021.World;

namespace pdxpartyparrot.ssjjune2021.Level
{
    [RequireComponent(typeof(Clues))]
    public sealed class TestSceneHelper : Game.Level.TestSceneHelper, IBaseLevel
    {
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
    }
}
