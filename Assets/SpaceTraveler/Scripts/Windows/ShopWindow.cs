using SpaceTraveler.Scripts.Configs.Skins;
using SpaceTraveler.Scripts.Managers;
using SpaceTraveler.Scripts.Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceTraveler.Scripts.Windows
{
    public class ShopWindow : BaseWindow
    {
        [Header("Main")]
        [SerializeField] private SkinManager _skinManager;

        [Header("TextSettings")]
        [SerializeField] private TextMeshProUGUI _skinNameText;
        [SerializeField] private TextMeshProUGUI _speedStat;
        [SerializeField] private TextMeshProUGUI _healthStat;
        [SerializeField] private TextMeshProUGUI _priceStat;
        [SerializeField] private TextMeshProUGUI _valuePlayerStars;

        [Header("SpaceShipTextSettings")]
        [SerializeField] private string _statSpeedText;
        [SerializeField] private string _statHealthText;
        [SerializeField] private string _priceText;

        [Header("Buttons")]
        [SerializeField] private Button _byeButton;
        [SerializeField] private Button _selectButton;

        [Header("Popup")]
        [SerializeField] private Image _popupUnbye;

        public void UpdateInformation(DataShipConfig currentShip, GameSaveData data)
        {
            _skinNameText.text = currentShip.Name;
            _speedStat.text = $"{_statSpeedText}{currentShip.Speed.ToString()}";
            _healthStat.text = $"{_statHealthText}{currentShip.MaxHealth.ToString()}";
            _priceStat.text = $"{_priceText}{currentShip.Price.ToString()}";
            _valuePlayerStars.text = $"- {data.PlayerData.PlayerStarScore}";
        }

        public void SwitchButtons(bool isBye, bool isSelect)
        {
            _byeButton.gameObject.SetActive(isBye);
            _selectButton.gameObject.SetActive(isSelect);
        }

        public void ActivationPopup(bool isActive)
        {
            _popupUnbye.gameObject.SetActive(isActive);
        }

        public void OnCloseShopWindow() =>
            UIManager.CloseWindow<ShopWindow>();
    }
}