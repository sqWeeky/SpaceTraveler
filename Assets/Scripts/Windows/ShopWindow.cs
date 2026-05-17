using System.Collections;
using System.Collections.Generic;
using Windows;
using Configs.Skins;
using Managers;
using Skins;
using UnityEngine;

public class ShopWindow : BaseWindow
{
    [SerializeField] private SkinManager _skinManager;
    
    public void OnEnable()
    {
        _skinManager.VisualizerSkinShop.InitVisualize();
    }
    
    public void OnCloseShopWindow() => 
        UIManager.CloseWindow<ShopWindow>();
}