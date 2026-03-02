using HarmonyLib;
using MelonLoader;

namespace GorillaTagMLTemplate.Patches
{
    // Patching can be a little complicated, so I'd recommend looking at the Patching docs on MelonLoader's wiki.
    // https://melonwiki.xyz/#/modders/patching
    
    // This patch will log multiple messages as the method is used in the actual game many times.
    
    // !!!
    // Do not use a patch for OnPlayerSpawned if you just want to run code when the player spawns. You can just use GorillaTagger.OnPlayerSpawned, which is much easier to use.
    // An example is already in Plugin.cs.
    // !!!
    
    [HarmonyPatch(typeof(GorillaTagger), "OnPlayerSpawned")]
    public static class ExamplePatch
    {
        // Prefix runs code BEFORE the original method.
        private static void Prefix()
        {
            MelonLogger.Msg("Prefix ran");
        }
        
        // Postfix runs code AFTER the original method.
        private static void Postfix()
        {
            MelonLogger.Msg("Postfix ran");
        }
    }
}