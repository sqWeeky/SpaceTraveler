using SpaceTraveler.Scripts.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceTraveler.Scripts.UI
{
    public class VisualizerSkinShop : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<string, GameObject> _parentSkinsColor;
        [SerializeField] private GameObject _shipPlacement;

        [Header("Preview")]
        [SerializeField, Range(128, 2048)] private int _textureSize = 512;
        [SerializeField, Min(1f)] private float _framePadding = 1.2f;
        [SerializeField] private Vector2 _previewCenter = new Vector2(0.5f, 0.6f);
        [SerializeField] private Vector2 _previewSize = new Vector2(0.6f, 0.45f);

        private Transform _newColorShip;
        private GameObject _previewRoot;
        private UnityEngine.Camera _previewCamera;
        private RenderTexture _renderTexture;
        private RawImage _previewImage;
        private RectTransform _windowRect;
        private bool _initialized;

        private void OnEnable()
        {
            if (EnsurePreview())
                _previewRoot.SetActive(true);
        }

        private void OnDisable()
        {
            if (_previewRoot != null)
                _previewRoot.SetActive(false);
        }

        public void VisualizeSkin(string nameSkin, string colorSkin = "Red_White")
        {
            // SkinManager.OnEnable can call this before this component's OnEnable.
            if (!EnsurePreview())
                return;

            if (!_parentSkinsColor.TryGetValue(nameSkin, out var ship) || ship == null)
            {
                Debug.LogWarning($"Shop preview: ship '{nameSkin}' not found.", this);
                return;
            }

            var selectedColor = ship.transform.Find(colorSkin);
            if (selectedColor == null)
                selectedColor = ship.transform.Find("Red_White");

            if (selectedColor == null)
            {
                Debug.LogWarning($"Shop preview: color '{colorSkin}' not found for '{nameSkin}'.", this);
                return;
            }

            foreach (var pair in _parentSkinsColor)
            {
                if (pair.Value == null)
                    continue;

                foreach (Transform color in pair.Value.transform)
                    color.gameObject.SetActive(false);

                pair.Value.SetActive(pair.Value == ship);
            }

            _newColorShip = selectedColor;
            _newColorShip.gameObject.SetActive(true);
            UpdatePreview();
        }

        private bool EnsurePreview()
        {
            if (_initialized)
                return true;

            var window = GetComponentInParent<SpaceTraveler.Scripts.Windows.ShopWindow>(true);
            int previewLayer = LayerMask.NameToLayer("ShopPreview");
            if (_shipPlacement == null || window == null || previewLayer < 0)
            {
                Debug.LogError("Shop preview requires Placement, ShopWindow parent and ShopPreview layer.", this);
                return false;
            }

            _windowRect = window.GetComponent<RectTransform>();
            _previewRoot = new GameObject("ShopPreviewRoot");
            // Keep world units independent of the Canvas and away from gameplay cameras.
            _previewRoot.transform.position = new Vector3(10000f, 10000f, 10000f);
            if (Application.isPlaying)
                DontDestroyOnLoad(_previewRoot);

            _shipPlacement.transform.SetParent(_previewRoot.transform, false);
            _shipPlacement.transform.localPosition = Vector3.zero;
            _shipPlacement.transform.localScale = Vector3.one;
            _shipPlacement.SetActive(true);
            SetLayerRecursively(_shipPlacement.transform, previewLayer);

            _renderTexture = new RenderTexture(Mathf.Clamp(_textureSize, 128, 2048),
                Mathf.Clamp(_textureSize, 128, 2048), 24, RenderTextureFormat.ARGB32)
            {
                name = "ShopPreviewTexture",
                antiAliasing = 1
            };
            _renderTexture.Create();

            var cameraObject = new GameObject("ShopPreviewCamera", typeof(UnityEngine.Camera));
            cameraObject.transform.SetParent(_previewRoot.transform, false);
            _previewCamera = cameraObject.GetComponent<UnityEngine.Camera>();
            _previewCamera.orthographic = true;
            _previewCamera.clearFlags = CameraClearFlags.SolidColor;
            _previewCamera.backgroundColor = Color.clear;
            _previewCamera.cullingMask = 1 << previewLayer;
            _previewCamera.targetTexture = _renderTexture;
            _previewCamera.allowHDR = false;
            _previewCamera.allowMSAA = false;
            _previewCamera.useOcclusionCulling = false;

            CreateLight("PreviewKeyLight", new Vector3(-3f, 4f, -4f), 1.5f, previewLayer);
            CreateLight("PreviewFillLight", new Vector3(3f, 1f, -2f), 0.8f, previewLayer);

            var imageObject = new GameObject("ShipPreview", typeof(RectTransform),
                typeof(CanvasRenderer), typeof(RawImage));
            imageObject.layer = window.gameObject.layer;
            var content = _windowRect.Find("Fill");
            imageObject.transform.SetParent(content != null ? content : _windowRect, false);
            // Render after the content background, before its buttons and labels.
            imageObject.transform.SetAsFirstSibling();
            _previewImage = imageObject.GetComponent<RawImage>();
            _previewImage.texture = _renderTexture;
            _previewImage.raycastTarget = false;
            _initialized = true;
            _previewRoot.SetActive(isActiveAndEnabled);
            return true;
        }

        private void LateUpdate()
        {
            if (_initialized)
                UpdatePreview();
        }

        private void UpdatePreview()
        {
            var rect = _previewImage.rectTransform;
            rect.anchorMin = _previewCenter;
            rect.anchorMax = _previewCenter;
            rect.anchoredPosition = Vector2.zero;
            float size = Mathf.Max(1f, Mathf.Min(_windowRect.rect.width * _previewSize.x,
                _windowRect.rect.height * _previewSize.y));
            rect.sizeDelta = new Vector2(size, size);

            if (_newColorShip == null)
                return;

            var renderers = _newColorShip.GetComponentsInChildren<Renderer>();
            bool hasBounds = false;
            Bounds bounds = default;
            foreach (var renderer in renderers)
            {
                if (!renderer.enabled || !renderer.gameObject.activeInHierarchy)
                    continue;
                // Particle/trail bounds can still point at the old world position after
                // reparenting. Frame the ship geometry, not its exhaust effects.
                if (!(renderer is MeshRenderer) && !(renderer is SkinnedMeshRenderer))
                    continue;
                if (!hasBounds)
                    bounds = renderer.bounds;
                else
                    bounds.Encapsulate(renderer.bounds);
                hasBounds = true;
            }

            if (!hasBounds)
                return;

            float radius = Mathf.Max(0.1f, bounds.extents.magnitude);
            _previewCamera.transform.position = bounds.center - Vector3.forward * (radius * 3f + 1f);
            _previewCamera.transform.rotation = Quaternion.identity;
            _previewCamera.orthographicSize = Mathf.Max(0.1f,
                Mathf.Max(bounds.extents.x, bounds.extents.y) * Mathf.Max(1f, _framePadding));
            _previewCamera.aspect = 1f;
            _previewCamera.nearClipPlane = 0.01f;
            _previewCamera.farClipPlane = radius * 6f + 2f;
        }

        private void CreateLight(string lightName, Vector3 position, float intensity, int layer)
        {
            var lightObject = new GameObject(lightName, typeof(Light));
            lightObject.transform.SetParent(_previewRoot.transform, false);
            lightObject.transform.localPosition = position;
            var light = lightObject.GetComponent<Light>();
            light.type = LightType.Point;
            light.range = 30f;
            light.intensity = intensity;
            light.cullingMask = 1 << layer;
            light.shadows = LightShadows.None;
        }

        private static void SetLayerRecursively(Transform target, int layer)
        {
            target.gameObject.layer = layer;
            foreach (Transform child in target)
                SetLayerRecursively(child, layer);
        }

        private void OnDestroy()
        {
            if (_previewCamera != null)
                _previewCamera.targetTexture = null;
            if (_previewImage != null)
                Destroy(_previewImage.gameObject);
            if (_renderTexture != null)
            {
                _renderTexture.Release();
                Destroy(_renderTexture);
            }
            if (_previewRoot != null)
                Destroy(_previewRoot);
        }
    }
}
