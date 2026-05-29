using UnityEngine;

namespace Skins
{
    public class Skin : MonoBehaviour
    {
        [SerializeField] private string _colorName;

        public string ColorName => _colorName;
    }
}