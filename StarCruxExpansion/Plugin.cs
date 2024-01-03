using BepInEx;

namespace Dawn.DMD.StarCruxExpansion;

using System.Reflection;
using BepInEx.Logging;
using Death.Darkness;
using Death.Run.Core;
using Death.Utils.Collections;
using Harmony;
using Helpers;
using JetBrains.Annotations;
using UI;

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
        var mask = new EnumArray<MonsterType, bool>();
        mask.Set(index: MonsterType.MidBoss, value: true);
        mask.Set(index: MonsterType.FinalBoss, value: true);


        var effect = new ChallengeData.Effect(statsPerLevel: new ModdedStatArray<float>(stats: (StatId.MovementSpeed, 0.25f)),
            creator: new GlobalEffectCreator(requiredStats: [StatId.MovementSpeed],
                callback: stats => new GlobalEffect_MonsterStatChange(
                    conditions: new GlobalEffectConditions(), 
                    stat: StatId.MovementSpeed,
                    value: stats.Get(stat: StatId.MovementSpeed),
                    change: StatChangeType.Bonus, monsterTypeMask: mask)));
        
        
        return new RealmData(realmName: "Debug Realm", challenges: new[]
        {
            new ChallengeDataInformation(textInformation: (Title: "The Spood", DescriptionFormat: $"Bosses have {FormatHelper.SetDataType(StatId.MovementSpeed, StatChangeType.Bonus)} movement."),
                challengeData: new ChallengeData(
                    code: "sce_boss_spood", 
                    maxLevel: 10, 
                    pointsPerLevel: 1, 
                    winsToUnlock: 0, 
                    iconPath: ChallengeDataEx.ToIconPath(icon: ChallengeDataEx.ChallengeDataIcon.Velocity),
                    effects: [effect]
                    ))
        });
    }
    
}