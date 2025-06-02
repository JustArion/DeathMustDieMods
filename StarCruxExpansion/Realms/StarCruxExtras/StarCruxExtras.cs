namespace Dawn.DMD.StarCruxExpansion.Realms.StarCruxExtras;

using Builders;
using Death.Darkness;
using Death.Run.Core;
using Helpers;
using ModdedGlobalEffects;
using RealmHelpers;
using Reflection;
using UI;

public static class StarCruxExtras
{
    private const string REALM_NAME = "Star Crux Extras";
    private const string LONGER_GAMES_CHALLENGE_CODE = "longer_runs";
    private const string SHORTER_GAMES_CHALLENGE_CODE = "shorter_runs";

    public static RealmData BuildRealm()
    {

        var builder = new RealmDataBuilder(REALM_NAME);

        builder.WithChallenge("Longer Runs", level => $"Your runs are {level * 5} min longer.")
            .WithCode(LONGER_GAMES_CHALLENGE_CODE)
            .WithIcon(ChallengeDataEx.ChallengeDataIcon.Angle)
            .WithMaxLevel(10)
            .WithPointsPerLevel(0)
            .WithCustomEffect(new([], _ =>
            {
                var currentLevel = Finder.GetRealmChallenge(REALM_NAME, LONGER_GAMES_CHALLENGE_CODE).Level;

                return new GlobalEffect_GameDurationChange(5 * currentLevel);
            }));

        builder.WithChallenge("Shorter Runs", level => $"Your runs are {level * 5} min shorter.")
            .WithCode(SHORTER_GAMES_CHALLENGE_CODE)
            .WithIcon(ChallengeDataEx.ChallengeDataIcon.Boots)
            .WithMaxLevel(3)
            .WithCustomEffect(new([], _ =>
            {
                var currentLevel = Finder.GetRealmChallenge(REALM_NAME, SHORTER_GAMES_CHALLENGE_CODE).Level;

                // Reduces the game duration by 5 mins x the challenge level
                var gameDurationDelta = -(5 * currentLevel);
                return new GlobalEffect_GameDurationChange(gameDurationDelta);
            }));


        return builder.Build();
    }
}