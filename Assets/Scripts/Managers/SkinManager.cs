using Skins;
using UI;
using UnityEngine;

namespace Managers
{
    public class SkinManager : BaseManager<SkinManager>
    {
        //переключает скины
        [SerializeField] private VisualizerSkinShop _visualizerSkinShop;
        
        private Skin _currentSkin;
        
        public VisualizerSkinShop VisualizerSkinShop => _visualizerSkinShop;

        public void ChangeSkin(string nameSkin)
        {
            _visualizerSkinShop.VisualizeSkin(nameSkin);
        }
    }
}
