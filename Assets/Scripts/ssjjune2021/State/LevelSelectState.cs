using UnityEngine;

using pdxpartyparrot.Game.State;

namespace pdxpartyparrot.ssjjune2021.State
{
    public class LevelSelectState : Game.State.LevelSelectState
    {
        [SerializeField]
        private GameOverState _gameOverState;

        #region Unity Lifecycle

        // copied from the main game state loop
        public override void OnUpdate(float dt)
        {
            if(GameStateManager.Instance.GameManager.IsGameOver) {
                GameStateManager.Instance.PushSubState(_gameOverState, state => {
                    state.Initialize();
                });
            }
        }

        #endregion
    }
}
