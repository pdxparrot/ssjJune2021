using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using pdxpartyparrot.ssjjune2021.Players;

namespace pdxpartyparrot.ssjjune2021.Menu
{
    // TODO: the invert option can probably move down to the Game level
    public sealed class SettingsMenu : Game.Menu.SettingsMenu
    {
        [Space(10)]

        [SerializeField]
        private Toggle _invertYAxisToggle;

        #region Unity Lifecycle

        protected override void OnEnable()
        {
            base.OnEnable();

            _invertYAxisToggle.isOn = PlayerManager.Instance.Players.ElementAt(0).PlayerInputHandler.InvertLookVertical;
        }

        #endregion

        #region Events

        public void OnInvertYAxisToggle(bool state)
        {
            PlayerManager.Instance.Players.ElementAt(0).PlayerInputHandler.InvertLookVertical = state;
        }

        #endregion
    }
}
