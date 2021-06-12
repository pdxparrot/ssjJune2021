using pdxpartyparrot.Game.Players;
using pdxpartyparrot.ssjjune2021.Data.Players;

namespace pdxpartyparrot.ssjjune2021.Players
{
    public sealed class PlayerManager : PlayerManager<PlayerManager>
    {
        public PlayerData GamePlayerData => (PlayerData)PlayerData;
    }
}
