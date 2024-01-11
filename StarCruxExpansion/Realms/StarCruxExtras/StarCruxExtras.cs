namespace Dawn.DMD.StarCruxExpansion.Realms.StarCruxExtras;

using Builders;
using Death.Darkness;
using Death.Run.Core;
using Helpers;
using ModdedGlobalEffects;
using Reflection;
using UI;

public static class StarCruxExtras
{
    private const string REALM_NAME = "Star Crux Extras";
    private const string LONGER_GAMES_CHALLENGE_CODE = "longer_games";
    private const string SHORTER_GAMES_CHALLENGE_CODE = "shorter_games";

    private static RealmData GetCurrentRealm() => ModdedRealmManager.moddedRealms.First(x => x.RealmName == REALM_NAME);
    public static RealmData BuildRealm()
    {

        var builder = new RealmDataBuilder(REALM_NAME);

        builder.WithChallenge("Longer Games", $"Your games are {builder.FormatStat()} minutes longer")
            .WithCode(LONGER_GAMES_CHALLENGE_CODE)
            .WithIcon(ChallengeDataEx.ChallengeDataIcon.Angle)
            .WithMaxLevel(10)
            .WithPointsPerLevel(0)
            .WithCustomEffect(new GlobalEffectCreator(Array.Empty<StatId>(), _ =>
            {
                var currentLevel = GetCurrentRealm().options.Challenges().First(x => x.Code == LONGER_GAMES_CHALLENGE_CODE).Level;

                return new GlobalEffect_GameDurationChange(5 * currentLevel);
            }));

        builder.WithChallenge("Shorter Games", "Your games are minutes shorter")
            .WithCode(SHORTER_GAMES_CHALLENGE_CODE)
            .WithIcon(ChallengeDataEx.ChallengeDataIcon.Boots)
            .WithMaxLevel(3)
            .WithCustomEffect(new GlobalEffectCreator(Array.Empty<StatId>(), _ =>
            {
                var currentLevel = GetCurrentRealm().options.Challenges().First(x => x.Code == SHORTER_GAMES_CHALLENGE_CODE).Level;

                
                // Reduces the game duration by 5 mins x the challenge level
                var gameDurationDelta = -(5 * currentLevel);
                return new GlobalEffect_GameDurationChange(gameDurationDelta);
            }));


        return builder.Build();
    }
}