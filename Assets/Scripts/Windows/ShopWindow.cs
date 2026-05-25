using System;
using Windows;
using Managers;
using UnityEngine;

public class ShopWindow : BaseWindow
{
    [SerializeField] private SkinManager _skinManager;

    public void OnEnable()
    {
       // _skinManager.Init(GameConfig);
        _skinManager.Activate();
    }
    
    public void OnCloseShopWindow() => 
        UIManager.CloseWindow<ShopWindow>();
}