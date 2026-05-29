using Windows;
using Managers;
using UnityEngine;

public class ShopWindow : BaseWindow
{
    [SerializeField] private SkinManager _skinManager;

    public void OnCloseShopWindow() => 
        UIManager.CloseWindow<ShopWindow>();
}