namespace Dawn.DMD.StarCruxExpansion.Realms.StarCruxExpansion;

using Builders;
using Death.Run.Core;
using Helpers;

public static class StarCruxExpansionRealm
{
    public static RealmData BuildRealm()
    {

        var builder = new RealmDataBuilder("Realm Expansion #1");

        // Description outputs like: 
        //                                  Bosses have {0:stat(mvm|0.#|%|s|u)} movement.
        builder.WithChallenge("The Spood", $"Bosses have {builder.FormatStat(StatId.MovementSpeed, StatChangeType.Bonus)} movement.")
            .WithCode("boss_spood")
            .WithMaxLevel(5)
            .WithPointsPerLevel(2)
            .WithIcon(ChallengeDataEx.ChallengeDataIcon.Boots)
            .WithEffect()
                .AlsoAffects(MonsterType.MidBoss)
                .AlsoAffects(MonsterType.FinalBoss)
                .WithChangeType(StatChangeType.Bonus)
                .WithStatPerLevel(StatId.MovementSpeed, 35 / 100f);

        builder.WithChallenge("The Homies", $"Minions have {builder.FormatStat(StatId.MovementSpeed, StatChangeType.Flat)} movement.")
            .WithCode("trash_spood")
            .WithMaxLevel(5)
            .WithPointsPerLevel(3)
            .WithIcon(ChallengeDataEx.ChallengeDataIcon.Boots)
            .WithEffect()
                .AlsoAffects(MonsterType.Elite)
                .AlsoAffects(MonsterType.Minion)
                .WithChangeType(StatChangeType.Flat)
                .WithStatPerLevel(StatId.MovementSpeed, 10 / 100f);

        builder.WithChallenge("Surf's Up",
                $"Enemies have {builder.FormatStat(StatId.KnockBack, StatChangeType.Flat)} knockback.")
            .WithCode("elite_kb")
            .WithMaxLevel(10)
            .WithPointsPerLevel(1)
            .WithIcon(ChallengeDataEx.ChallengeDataIcon.Scythe)
            .WithEffect()
                .AffectsAllMonsters()
                .WithChangeType(StatChangeType.Flat)
                .WithPrimaryStat(StatId.KnockBack)
                .WithStatPerLevel(StatId.KnockBack, 50 / 100f);

        return builder.Build();
    }
}