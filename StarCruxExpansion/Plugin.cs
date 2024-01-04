using BepInEx;

namespace Dawn.DMD.StarCruxExpansion;

using System.Reflection;
using BepInEx.Logging;
using Harmony;
using Helpers;
using JetBrains.Annotations;
using Realms.StarCruxExpansion;
using Realms.UI;

[BepInPlugin("dawn.dmd.starcruxexpansion", "StarCruxExpansion", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource ModLogger { get; private set; }
    internal static Plugin Instance { get; private set; }
    [UsedImplicitly]
    private void Awake()
    {
        ModLogger = Logger;
        Instance = this;
        ModLogger.LogInfo("The heavens open, the stars expand; more opportunity awaits!");
        HarmonyLib.Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

        InterceptStarCruxUI_Patch.OnDarknessInit += darkness => darkness.AddComponent<ModdedRealmManager>();

        ModdedRealmManager.AddModdedRealm(Realm.BuildRealm());
    }
    
}