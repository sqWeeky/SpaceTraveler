using SpaceTraveler.Scripts.Managers;
using UnityEngine;

namespace SpaceTraveler.Scripts.Windows
{
    public class ShopWindow : BaseWindow
    {
        [SerializeField] private SkinManager _skinManager;

        public void OnCloseShopWindow() => 
            UIManager.CloseWindow<ShopWindow>();
    }
}