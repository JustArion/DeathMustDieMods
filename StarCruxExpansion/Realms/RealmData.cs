namespace Dawn.DMD.StarCruxExpansion.Realms;

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BepInEx.Configuration;
using Death.Darkness;
using Helpers;
using RealmHelpers.GameDurationChangeHandler.Harmony;
using Reflection;

[SuppressMessage("ReSharper", "ParameterTypeCanBeEnumerable.Local")]
[SuppressMessage("ReSharper", "ReturnTypeCanBeEnumerable.Global")]
public class RealmData(string realmName, ChallengeDataInformation[] challenges) : IReadOnlyCollection<ChallengeData>
{
    public readonly DarknessOptions options = GenerateDarknessOptions(realmName, challenges);

    private static DarknessOptions GenerateDarknessOptions(string realmName, ChallengeDataInformation[] challenges)
    {
        var options = challenges.Select(x => x.ChallengeData).ToDarknessOptions();
        
        LoadFromConfig(realmName, options);

        return options;
    }

    public static void SaveToConfig(string realmName, DarknessOptions options)
    {
        foreach (var option in options)
        {
            if (!Instance.Config.TryGetEntry("Realm_" + realmName, option.Code, out ConfigEntry<int> entry))
                continue;

            if (entry.Value != option.Level)
                ModLogger.LogDebug($"'{realmName}' saved Challenge '{option.Code}', {entry.Value} -> {option.Level}");
            entry.Value = option.Level;
        }
        
        Instance.Config.Save();
    }

    private static void LoadFromConfig(string realmName, DarknessOptions options)
    {
        foreach (var option in options)
        {
            var entry = Instance.Config.Bind("Realm_" + realmName, option.Code, 0, "The level of the Challenge");

            
            if (entry.Value != option.Level)
                ModLogger.LogDebug($"'{realmName}' loaded Challenge '{option.Code}' with level [{entry.Value}]");
            
            // TODO: Figure out a way to ensure that the level doesn't exceed the max level for the ChallengeData.
            // Currently we only have access to the Level and Code.
            option.Level = entry.Value;
        }
    }

    public string RealmName { get; } = realmName;

    public ChallengeDataInformation[] ChallengesInformation => challenges;
    public IEnumerable<ChallengeData> Challenges { get; } = challenges.Select(x => x.ChallengeData);

    public IEnumerable<(string Title, ChallengeDescriptionBuilder DescriptionFormat)> ChallengeDataInformation { get; } = challenges.Select(x => x.TextInformation);
    
    public IEnumerator<ChallengeData> GetEnumerator() => Challenges.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count => challenges.Length;

    public override string ToString() => RealmName;
}