using System.Collections.Generic;
using SpaceTraveler.Scripts.Windows;
using UnityEngine;

namespace SpaceTraveler.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(UIManagerConfig))]
    public class UIManagerConfig : ScriptableObject
    {
        [SerializeField] private List<BaseWindow> _baseWindows;

        public List<BaseWindow> BaseWindows => _baseWindows;
    }
}