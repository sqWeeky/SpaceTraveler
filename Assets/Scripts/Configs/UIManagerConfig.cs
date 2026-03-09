using System.Collections.Generic;
using Windows;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(UIManagerConfig))]
    public class UIManagerConfig : ScriptableObject
    {
        [SerializeField] private List<BaseWindow> _baseWindows;

        public List<BaseWindow> BaseWindows => _baseWindows;
    }
}