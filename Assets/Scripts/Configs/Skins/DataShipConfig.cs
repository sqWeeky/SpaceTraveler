using System.Collections.Generic;
using Skins;
using UnityEngine;

namespace Configs.Skins
{
    [CreateAssetMenu(menuName = "Configs/Data/" + nameof(DataShipConfig))]
    public class DataShipConfig : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private List<Skin> _materials;
        [SerializeField] private int _speed;
        [SerializeField] private int _maxHealth;
        
        public List<Skin> ShipColors => _materials;
        public string Name => _name;
        public int Speed => _speed;
        public int MaxHealth => _maxHealth;
    }
}