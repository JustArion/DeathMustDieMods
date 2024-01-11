namespace Dawn.DMD.StarCruxExpansion.Realms;

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Death.Darkness;
using Helpers;

[SuppressMessage("ReSharper", "ParameterTypeCanBeEnumerable.Local")]
[SuppressMessage("ReSharper", "ReturnTypeCanBeEnumerable.Global")]
public class RealmData(string realmName, ChallengeDataInformation[] challenges) : IReadOnlyCollection<ChallengeData>
{
    public readonly DarknessOptions options = challenges.Select(x => x.ChallengeData).ToDarknessOptions();
    public string RealmName { get; } = realmName;

    public ChallengeDataInformation[] ChallengesInformation => challenges;
    public IEnumerable<ChallengeData> Challenges { get; } = challenges.Select(x => x.ChallengeData);

    public IEnumerable<(string Title, string DescriptionFormat)> ChallengeDataInformation { get; } = challenges.Select(x => x.TextInformation);
    
    public IEnumerator<ChallengeData> GetEnumerator() => Challenges.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count => challenges.Length;
}