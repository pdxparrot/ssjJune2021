using System;

using UnityEngine;

namespace pdxpartyparrot.ssjjune2021.Data.Players
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "pdxpartyparrot/ssjjune2021/Data/Players/Player Data")]
    [Serializable]
    public sealed class PlayerData : Game.Data.Players.PlayerData
    {
        [SerializeField]
        private float _worldRespawnOffset;

        public float WorldRespawnOffset => _worldRespawnOffset;
    }
}
