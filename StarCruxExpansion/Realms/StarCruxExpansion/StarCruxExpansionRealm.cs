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
            .WithIcon(ChallengeDataEx.ChallengeDataIcon.Velocity)
            .WithEffect()
                .AlsoAffects(MonsterType.MidBoss)
                .AlsoAffects(MonsterType.FinalBoss)
                .WithChangeType(StatChangeType.Bonus)
                .WithStatPerLevel(StatId.MovementSpeed, 0.35f);

        return builder.Build();
    }
}