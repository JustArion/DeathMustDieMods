﻿namespace Dawn.DMD.StarCruxExpansion.Realms.Builders;

using System.Collections.Generic;
using Death.Run.Core;
using Reflection;

/// <summary>
/// Reminder: Challenge titles should follow the game's original theme of "Capital On Each Letter".
/// Challenge descriptions should follow similarly like: "Capital on start and fullstop at the end."
/// Realm names should follow the challenge title theme.
/// </summary>
/// <param name="realmName"></param>
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
            StatChangeType.BoonMod => throw new NotSupportedException(),
            StatChangeType.ItemMod => throw new NotSupportedException(),
            StatChangeType.Bonus => "{0:stat(STAT_TYPE|0.#|%|s|u)}",
            StatChangeType.TalentMod => throw new NotSupportedException(),
            StatChangeType.AdditionalItemValue => throw new NotSupportedException(),
            StatChangeType.Darkness => throw new NotSupportedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(changeType), changeType, null)
        };
    }

    private readonly List<ChallengeDataBuilder> _challengeBuilders = [];

    public ChallengeDataBuilder WithChallenge(string challengeTitle, ChallengeDescriptionBuilder formattedDescription)
    {
        var challengeBuilder = new ChallengeDataBuilder()
            .WithTitle(challengeTitle)
            .WithDescriptionBuilder(formattedDescription);

        _challengeBuilders.Add(challengeBuilder);

        return challengeBuilder;
    }
    
    public ChallengeDataBuilder WithChallenge(string challengeTitle, string formattedDescription)
    {
        var challengeBuilder = new ChallengeDataBuilder()
            .WithTitle(challengeTitle)
            .WithDescriptionBuilder(_ => formattedDescription);

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
        
        // Check if all challenges' codes are unique
        var codes = challenges.Select(x => x.ChallengeData.Code).ToArray();
        if (codes.Length != codes.Distinct().Count())
            ModLogger.LogWarning(
                $"Realm '{realmName}' has duplicate challenge codes. This may cause name and description issues.");
        
        return new(realmName, challenges);
    }
}