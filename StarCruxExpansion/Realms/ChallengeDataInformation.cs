namespace Dawn.DMD.StarCruxExpansion.Realms;

using Death.Darkness;

public delegate string ChallengeDescriptionBuilder(int challengeLevel);
public readonly struct ChallengeDataInformation((string Title, ChallengeDescriptionBuilder DescriptionFormatBuilder) textInformation, ChallengeData challengeData)
{
    public ChallengeData ChallengeData { get; } = challengeData;
    
    /// Description format are as follows
    /// Percent - "Bosses have {0:stat(per|0.#|%|s|u)} damage."
    /// eg. "Bosses have +35% more life per rank."
    /// Flat - "Elite enemies have {0:stat(val|0.#|s|u|*100)} movement."
    /// eg. "Elite enemies have +60 movement."
    /// See Documentation/Deep-Dives/ChallengeDataSmartFormatter.md for more information
    public (string Title, ChallengeDescriptionBuilder DescriptionFormatBuilder) TextInformation { get; } = textInformation;
}