using System.Collections.Generic;
using Skins;
using UnityEngine;

namespace Configs.Skins
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(PlayerSkinsConfig))]
    public class PlayerSkinsConfig : ScriptableObject
    {
        [SerializeField] private List<DataShipConfig> _playerSkins;

        public List<DataShipConfig> PlayerSkins => _playerSkins;
    }
}
