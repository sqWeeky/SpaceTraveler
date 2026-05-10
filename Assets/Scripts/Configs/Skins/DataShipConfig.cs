using System.Collections.Generic;
using Skins;
using UnityEngine;

namespace Configs.Skins
{
    [CreateAssetMenu(menuName = "Configs/Data/" + nameof(ConfigurationSkinConfig))]
    public class ConfigurationSkinConfig : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private List<Skin> _materials;

        public List<Skin> ShipColors => _materials;
        public string Name => _name;
    }
}