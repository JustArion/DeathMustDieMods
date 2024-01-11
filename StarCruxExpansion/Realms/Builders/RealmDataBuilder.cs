namespace Dawn.DMD.StarCruxExpansion.Realms.Builders;

using System.Collections.Generic;
using Death.Run.Core;
using Reflection;

public class RealmDataBuilder(string realmName)
{
    public string FormatStat(StatId id, StatChangeType type)
    {
        var statIdMap = StatIdUtilsReflection.ShortCodeToStatId();
        
        var statCode = statIdMap.First(x => x.Value  == id).Key;
        
        var format = GetStatChangeTypeFormat(type);
        
        return format.Replace(STAT_TYPE_HOLDER, statCode);
    }
    
    private const string STAT_TYPE_HOLDER = "STAT_TYPE";
    private static string GetStatChangeTypeFormat(StatChangeType changeType)
    {
        return changeType switch
        {
            StatChangeType.Flat => "{0:stat(STAT_TYPE|0.#|s|u|*100)}",
            StatChangeType.LevelMod => throw new NotSupportedException(),
            StatChangeType.BoonMod => throw new NotSupportedException(),
            StatChangeType.ItemMod => throw new NotSupportedException(),
            StatChangeType.Bonus => "{0:stat(STAT_TYPE|0.#|%|s|u)}",
            _ => throw new ArgumentOutOfRangeException(nameof(changeType), changeType, null)
        };
    }

    private readonly List<ChallengeDataBuilder> _challengeBuilders = [];

    public ChallengeDataBuilder WithChallenge(string challengeTitle, string formattedDescription)
    {
        var challengeBuilder = new ChallengeDataBuilder()
            .WithTitle(challengeTitle)
            .WithDescription(formattedDescription);

        _challengeBuilders.Add(challengeBuilder);

        return challengeBuilder;
    }

    public RealmData Build()
    {
        // We need to call ToArray() here since the RealmData constructor iterates over the IEnumerable multiple times, which can cause multiple ChallengeData builds.
        var challenges = _challengeBuilders.Select(x => x.Build()).ToArray();
        ModLogger.LogDebug($"Building Realm with '{challenges.Length}' challenges. \nTitles: [ {string.Join(", ", challenges.Select(x => $"\"{x.TextInformation.Title}\""))} ]");

        if (challenges.Length == 0) 
            ModLogger.LogWarning($"Realm '{realmName}' was created with 0 challenges!");
        return new(realmName, challenges);
    }
}