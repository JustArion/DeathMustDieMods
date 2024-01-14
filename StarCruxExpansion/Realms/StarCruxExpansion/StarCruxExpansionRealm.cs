namespace Dawn.DMD.StarCruxExpansion.Realms.StarCruxExpansion;

using System.Text;
using Builders;
using Death.Darkness;
using Death.Run.Core;
using Helpers;
using ModdedGlobalEffects;
using RealmHelpers;
using UnityEngine;

public static class StarCruxExpansionRealm
{
    private const string REALM_NAME = "Realm Expansion #1";
    private const string DEVILS_BARGAIN_CHALLENGE_CODE = "lose_boons_over_time";
    public static RealmData BuildRealm()
    {

        var builder = new RealmDataBuilder(REALM_NAME);

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

        builder.WithChallenge("Devil's Bargain",
                lvl => lvl > 0 
                    ? $"+{lvl * 100}% exp. Lose a boon every {MinsToLoseBoon(lvl)} mins." 
                    : "You get 0% more experience.")
            .WithCode(DEVILS_BARGAIN_CHALLENGE_CODE)
            .WithMaxLevel(5)
            .WithPointsPerLevel(5)
            .WithIcon(ChallengeDataEx.ChallengeDataIcon.Knife)
            .WithCustomEffect(new(Array.Empty<StatId>(), _ =>
            {
                var currentLevel = Finder.GetRealmChallenge(REALM_NAME, DEVILS_BARGAIN_CHALLENGE_CODE).Level;
                var minsToLoseBoon = MinsToLoseBoon(currentLevel);

                // If the boon is Master level or higher, we don't remove them. We shouldn't remove fun :)
                return new GlobalEffect_BoonLossOverTime(new GlobalEffect_BoonLossOverTime.BoonLossOptions(minsToLoseBoon, 
                    x => !x.IsTemporary && (int)x.Rarity < (int)Rarity.Master));
            }))
            .WithCustomEffect(new(Array.Empty<StatId>(), _ =>
            {
                var currentLevel = Finder.GetRealmChallenge(REALM_NAME, DEVILS_BARGAIN_CHALLENGE_CODE).Level;

                return new GlobalEffect_XPMultiplierChange(currentLevel);
            }));

            

        return builder.Build();
    }
    
    // every (7 / challenge level) a boon gets lost that's not a Master rarity or higher. (Includes cursed boons)
    private static double MinsToLoseBoon(int level) => 
        Math.Round(
            Mathf.Clamp(7f / Mathf.Clamp(level, 1, int.MaxValue), 1.5f, float.MaxValue), 1);
}