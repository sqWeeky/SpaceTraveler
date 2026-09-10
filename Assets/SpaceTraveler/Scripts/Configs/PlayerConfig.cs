using SpaceTraveler.Scripts.Configs.Skins;
using SpaceTraveler.Scripts.Skins;
using UnityEngine;

namespace SpaceTraveler.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(PlayerConfig))]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private string _playerName = "Player";
        [SerializeField] private int _crystalsScore;
        [SerializeField] private int _starScore;
        [SerializeField] private DataShipConfig _defaultDataShip;
        [SerializeField] private Skin _defaultSkinShip;

        public string PlayerName => _playerName;
        public int CrystalsScore => _crystalsScore;
        public int StarScore => _starScore;
        public DataShipConfig DefaultDataShip => _defaultDataShip;
        public Skin DefaultSkinShip => _defaultSkinShip;
    }
}