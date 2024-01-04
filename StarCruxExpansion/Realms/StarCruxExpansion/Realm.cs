namespace Dawn.DMD.StarCruxExpansion.Realms.StarCruxExpansion;

using Death.Darkness;
using Death.Run.Core;
using Death.Utils.Collections;
using Helpers;

public static class Realm
{
    public static RealmData BuildRealm()
    {
        return GenerateDebugRealm();
    }
    
    private static RealmData GenerateDebugRealm()
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
        
        
        return new RealmData(realmName: "Star Crux Expansion", challenges: new[]
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