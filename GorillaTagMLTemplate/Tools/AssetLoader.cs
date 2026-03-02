using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace GorillaTagMLTemplate.Tools
{
    public class AssetLoader
    {
        private static AssetBundle loadedBundle;
        private static readonly Dictionary<string, Object> loadedAssets = new Dictionary<string, Object>();

        private static Task bundleLoadTask;

        public static async Task<T> LoadAsset<T>(string assetName) where T : Object
        {
            if (loadedAssets.TryGetValue(assetName, out Object asset) && asset is T) return (T)asset;

            if (loadedBundle is null)
            {
                bundleLoadTask ??= LoadAssetBundle();
                await bundleLoadTask;
            }

            TaskCompletionSource<T> completionSource = new TaskCompletionSource<T>();

            AssetBundleRequest request = loadedBundle.LoadAssetAsync<T>(assetName);
            request.completed += _ => completionSource.SetResult(request.asset is Object asset ? (T)asset : null);

            T result = await completionSource.Task;
            loadedAssets.Add(assetName, result);

            return result;
        }

        private static async Task LoadAssetBundle()
        {
            TaskCompletionSource<AssetBundle> completionSource = new TaskCompletionSource<AssetBundle>();

            // The path here is the namespace of your mod, followed by the folder path to the asset bundle, with dots instead of slashes.
            // So if your mod's namespace is GorillaTagMLTemplate, and you put your asset bundle in a folder called Content in your project, the path would be "GorillaTagMLTemplate.Content.bundle".
            // If you change the name of your mod's namespace, make sure to change it here as well.
            // Also, make sure to make it so the bundle is compiled in with the mod in the .csproj file. An example of how to do this can be found in the .csproj file in this template.
            
            Stream stream = typeof(YourModClass).Assembly.GetManifestResourceStream("GorillaTagMLTemplate.Content.bundle");

            AssetBundleCreateRequest request = AssetBundle.LoadFromStreamAsync(stream);
            request.completed += _ => completionSource.SetResult(request.assetBundle);

            loadedBundle = await completionSource.Task;
        }
    }
}