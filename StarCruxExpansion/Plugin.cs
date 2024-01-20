using BepInEx;

namespace Dawn.DMD.StarCruxExpansion;

using System.Reflection;
using BepInEx.Logging;
using Death.Items;
using Death.Run.Behaviours;
using Death.Run.Systems;
using Harmony;
using Helpers;
using JetBrains.Annotations;
using Realms;
using Realms.Persistence;
using Realms.RealmHelpers.GameDurationChangeHandler;
using Realms.RealmHelpers.GameDurationChangeHandler.Harmony;
using Realms.StarCruxExpansion;
using Realms.StarCruxExtras;
using Realms.UI;
using UI;

[BepInPlugin("dawn.dmd.starcruxexpansion", "StarCruxExpansion", "1.1.1")]
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
        InterceptStarCruxUI_Patch.OnDarknessInit += darkness => darkness.AddComponent<StateMonitor>().Disable += RealmStateManager.SaveAll;

        ModdedRealmManager.AddModdedRealm(StarCruxExpansionRealm.BuildRealm());
        ModdedRealmManager.AddModdedRealm(StarCruxExtras.BuildRealm());
        
        OnRunStart_Patch.OnRunStart += MonsterWaveModifier.OnRunStart;
    }
    
}