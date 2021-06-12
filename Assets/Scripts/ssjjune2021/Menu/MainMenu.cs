using pdxpartyparrot.Game.State;

namespace pdxpartyparrot.ssjjune2021.Menu
{
    public sealed class MainMenu : Game.Menu.MainMenu
    {
        #region Events

        public override void OnStart()
        {
            base.OnStart();

            GameStateManager.Instance.TransitionStateAsync(GameManager.Instance.GameGameData.LevelSelectStatePrefab);
        }

        #endregion
    }
}
