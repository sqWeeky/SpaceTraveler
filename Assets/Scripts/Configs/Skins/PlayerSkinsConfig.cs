using System.Collections.Generic;
using Skins;
using UnityEngine;

namespace Configs.Skins
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(PlayerSkinsConfig))]
    public class PlayerSkinsConfig : ScriptableObject
    {
        [SerializeField] private List<ConfigurationSkinConfig> _playerSkins;

        public List<ConfigurationSkinConfig> PlayerSkins => _playerSkins;
    }
}
