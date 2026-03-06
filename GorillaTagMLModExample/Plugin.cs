using GorillaTagMLModExample.Tools;
using MelonLoader;
using GorillaTagMLModExample;
using UnityEngine;

// !!!
// NOTE: When you build the project, your IDE will automatically try to move the compiled DLL to the Mods folder, with the name of GorillaTagMLModExample.dll/GorillaTagMLModExample.pdb. If you change the name of the project, make sure to change the name of the DLL in the .csproj file.
// !!!

// Change the MelonInfo to correspond to your mod info. The first parameter is the class that inherits from MelonMod, so if you change the class name below, make sure to change it here as well.
// Make sure to also change the info in Constants.cs if you are going to use that. Constants dont seem to work here, so it isn't used in the MelonInfo attribute, but you can still use it in your mod code.
[assembly: MelonInfo(typeof(YourModClass), "GorillaTagMLModExample", "1.0.0", "Lapis")]
[assembly: MelonGame("Another Axiom", "Gorilla Tag")]
namespace GorillaTagMLModExample
{
    public class YourModClass : MelonMod
    {
        public GameObject testObject;
        // InitializeMelon runs as soon as the mod loads. If you instead want to run code when the player is spawned, put your code in GorillaTagger.OnPlayerSpawned.
        public override void OnInitializeMelon()
        {
            MelonLogger.Msg($"{Constants.ModName} v{Constants.Version} has loaded!");
            
            GorillaTagger.OnPlayerSpawned(() =>
            {
                MelonLogger.Msg("Player has spawned!");
                LoadBundleObject();
            });
        }

        public async void LoadBundleObject()
        {
            // Example of how to load an asset from an asset bundle. Make sure to change the asset name to the name of the asset in your bundle.
            // Also, make sure to change the path in AssetLoader.cs to the correct path for your mod and bundle.
            testObject = await AssetLoader.LoadAsset<GameObject>("Head");
            
            // If you load bundles in another script, and it's a MonoBehaviour, you shouldn't need "Object.", it should just work as Instantiate(testObject);
            testObject = Object.Instantiate(testObject);
            
            // Positioning the object to be inside the Stump.
            testObject.transform.position = new Vector3(-67.0938f, 11.7505f, -82.6406f);
        }
    }
}