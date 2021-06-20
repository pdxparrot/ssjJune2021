using System;

using pdxpartyparrot.Game.Characters.BehaviorComponents;
using pdxpartyparrot.Game.Players.Input;
using pdxpartyparrot.ssjjune2021.Data.Players;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace pdxpartyparrot.ssjjune2021.Players
{
    public sealed class PlayerInputHandler : ThirdPersonPlayerInputHandler
    {
        private Player GamePlayer => (Player)Player;

        #region Unity Lifecycle

        protected override void Awake()
        {
            base.Awake();

            Assert.IsTrue(PlayerInputData is PlayerInputData);
            Assert.IsTrue(Player is Player);

            GameManager.Instance.Settings.SettingsUpdatedEvent += SettingsUpdatedEventHandler;
        }

        protected override void OnDestroy()
        {
            if(GameManager.HasInstance) {
                GameManager.Instance.Settings.SettingsUpdatedEvent -= SettingsUpdatedEventHandler;
            }

            base.OnDestroy();
        }

        #endregion

        public override void Initialize(short playerControllerId)
        {
            base.Initialize(playerControllerId);

            InvertLookVertical = GameManager.Instance.Settings.InvertLookVertical;
        }

        #region Actions

        public void OnJumpAction(InputAction.CallbackContext context)
        {
            if(!IsInputAllowed(context)) {
                return;
            }

            if(Core.Input.InputManager.Instance.EnableDebug) {
                Debug.Log($"Jump: {context.action.phase}");
            }

            if(context.performed) {
                Player.PlayerBehavior.ActionPerformed(JumpBehaviorComponent.JumpAction.Default);
            }
        }

        public void OnInteractAction(InputAction.CallbackContext context)
        {
            if(!IsInputAllowed(context)) {
                return;
            }

            if(Core.Input.InputManager.Instance.EnableDebug) {
                Debug.Log($"Interact: {context.action.phase}");
            }

            if(context.performed) {
                GamePlayer.GamePlayerBehavior.TailorBehavior.Interact();
            }
        }

        #endregion

        #region Event Handlers

        private void SettingsUpdatedEventHandler(object sender, EventArgs args)
        {
            InvertLookVertical = GameManager.Instance.Settings.InvertLookVertical;
        }

        #endregion
    }
}
