using System;
using System.IO;
using System.Reflection;
using SpaceTraveler.Scripts.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class ShopPreviewVerification
{
    private const string Request = "Temp/VerifyShopPreview.request";

    static ShopPreviewVerification()
    {
        EditorApplication.delayCall += RunRequested;
    }

    [MenuItem("Tools/SpaceTraveler/Verify Shop Preview")]
    private static void Verify()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            Debug.LogWarning("Stop Play Mode before verifying shop preview.");
            return;
        }

        var scene = EditorSceneManager.NewPreviewScene();
        GameObject instance = null;
        GameObject root = null;
        RenderTexture texture = null;
        Texture2D pixels = null;
        RenderTexture previous = RenderTexture.active;
        try
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/SpaceTraveler/Resources/UI/Windows/ShopWindow.prefab");
            instance = UnityEngine.Object.Instantiate(prefab);
            SceneManager.MoveGameObjectToScene(instance, scene);
            instance.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 800);
            var visualizer = instance.GetComponentInChildren<VisualizerSkinShop>(true);
            visualizer.VisualizeSkin("Arrow", "Red_White");
            var flags = BindingFlags.Instance | BindingFlags.NonPublic;
            root = (GameObject)typeof(VisualizerSkinShop).GetField("_previewRoot", flags).GetValue(visualizer);
            var camera = (Camera)typeof(VisualizerSkinShop).GetField("_previewCamera", flags).GetValue(visualizer);
            texture = camera.targetTexture;
            SceneManager.MoveGameObjectToScene(root, scene);
            camera.scene = scene;
            camera.overrideSceneCullingMask = EditorSceneManager.GetSceneCullingMask(scene);
            root.SetActive(true);
            typeof(VisualizerSkinShop).GetMethod("UpdatePreview", flags).Invoke(visualizer, null);
            camera.Render();
            RenderTexture.active = texture;
            pixels = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
            pixels.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
            pixels.Apply();
            int visible = 0;
            foreach (var pixel in pixels.GetPixels32())
                if (pixel.a > 16)
                    visible++;
            var image = instance.GetComponentInChildren<UnityEngine.UI.RawImage>(true);
            bool order = image != null && image.transform.parent.name == "Fill" && image.transform.GetSiblingIndex() == 0;
            Directory.CreateDirectory("Temp/ShopPreviewVerification");
            File.WriteAllBytes("Temp/ShopPreviewVerification/Arrow.png", pixels.EncodeToPNG());
            string result = $"Visible pixels: {visible}/{texture.width * texture.height}; UI inside Fill before controls: {order}; Camera: {camera.transform.position}, size: {camera.orthographicSize}; Active renderers: {root.GetComponentsInChildren<Renderer>().Length}";
            File.WriteAllText("Temp/ShopPreviewVerification/result.txt", result);
            if (visible == 0 || !order)
                throw new Exception(result);
            Debug.Log("Shop preview verification passed: " + result);
        }
        catch (Exception exception)
        {
            Directory.CreateDirectory("Temp/ShopPreviewVerification");
            File.WriteAllText("Temp/ShopPreviewVerification/result.txt", exception.ToString());
            Debug.LogException(exception);
        }
        finally
        {
            RenderTexture.active = previous;
            if (pixels != null) UnityEngine.Object.DestroyImmediate(pixels);
            if (root != null) UnityEngine.Object.DestroyImmediate(root);
            if (texture != null) { texture.Release(); UnityEngine.Object.DestroyImmediate(texture); }
            if (instance != null)
            {
                var image = instance.GetComponentInChildren<UnityEngine.UI.RawImage>(true);
                if (image != null) UnityEngine.Object.DestroyImmediate(image.gameObject);
                UnityEngine.Object.DestroyImmediate(instance);
            }
            EditorSceneManager.ClosePreviewScene(scene);
        }
    }

    private static void RunRequested()
    {
        if (!File.Exists(Request)) return;
        File.Delete(Request);
        Verify();
    }
}
