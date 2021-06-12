using JetBrains.Annotations;

using pdxpartyparrot.Game.UI;

namespace pdxpartyparrot.ssjjune2021.UI
{
    public sealed class GameUIManager : GameUIManager<GameUIManager>
    {
        [CanBeNull]
        public GameUI GameGameUI => (GameUI)GameUI;
    }
}
