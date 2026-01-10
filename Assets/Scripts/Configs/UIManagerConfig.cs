using System.Collections.Generic;
using Managers;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/" + nameof(UIManagerConfig))]
public class UIManagerConfig : ScriptableObject
{
    [SerializeField] private List<BaseWindow> _baseWindows;

    public List<BaseWindow> BaseWindows => _baseWindows;
}