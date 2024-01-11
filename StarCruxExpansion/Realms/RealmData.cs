namespace Dawn.DMD.StarCruxExpansion.Realms;

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BepInEx.Configuration;
using Death.Darkness;
using Helpers;
using RealmHelpers.GameDurationChangeHandler.Harmony;

[SuppressMessage("ReSharper", "ParameterTypeCanBeEnumerable.Local")]
[SuppressMessage("ReSharper", "ReturnTypeCanBeEnumerable.Global")]
public class RealmData(string realmName, ChallengeDataInformation[] challenges) : IReadOnlyCollection<ChallengeData>
{
    public readonly DarknessOptions options = GenerateDarknessOptions(realmName, challenges);

    private static DarknessOptions GenerateDarknessOptions(string realmName, ChallengeDataInformation[] challenges)
    {
        var options = challenges.Select(x => x.ChallengeData).ToDarknessOptions();
        
        SyncFromConfig(realmName, options);

        OnRunStart_Patch.OnRunStart += _ => SyncToConfig(realmName, options);

        return challenges.Select(x => x.ChallengeData).ToDarknessOptions();
    }

    private static void SyncToConfig(string realmName, DarknessOptions options)
    {
        foreach (var option in options)
        {
            if (!Instance.Config.TryGetEntry("Realm_" + realmName, option.Code, out ConfigEntry<int> entry))
                continue;

            entry.Value = option.Level;
        }
        
        Instance.Config.Save();
    }

    private static void SyncFromConfig(string realmName, DarknessOptions options)
    {
        foreach (var option in options)
        {
            var entry = Instance.Config.Bind("Realm_" + realmName, option.Code, 0, "The level of the Challenge");

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
}