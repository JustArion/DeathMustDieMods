using BepInEx;

namespace Dawn.DMD.StarCruxExpansion;

using System.Collections.Generic;
using System.Reflection;
using BepInEx.Logging;
using Death.Darkness;
using Harmony;
using Helpers;
using JetBrains.Annotations;
using UI;
using UnityEngine;

[BepInPlugin("dawn.dmd.starcruxexpansion", "StarCruxExpansion", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    internal new static ManualLogSource Logger { get; private set; }
    internal static Plugin Instance { get; private set; }
    [UsedImplicitly]
    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;
        Logger.LogInfo("The heavens open, the stars expand; more opportunity awaits!");
        HarmonyLib.Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

        InterceptStarCruxUI_Patch.OnDarknessInit += darkness => darkness.AddComponent<ModdedRealmManager>();

        var debugRealm = GenerateDebugRealm();

        ModdedRealmManager.AddModdedRealm(debugRealm);
    }

    private RealmData GenerateDebugRealm()
    {
        return new RealmData("Debug Realm", new[]
        {
            new ChallengeDataInformation(new ChallengeDataTextInformation("Debug Title", "Debug Descriptions have {0:stat(per|0.#|%|s|u)} more epicness"),
                new ChallengeData("sce_debug", 10, 1, 0, ChallengeDataEx.ToIconPath(ChallengeDataEx.ChallengeDataIcon.Velocity),
                    []))
        });
    }
    
}