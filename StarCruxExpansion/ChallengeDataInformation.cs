namespace Dawn.DMD.StarCruxExpansion;

using Death.Darkness;

public readonly struct ChallengeDataInformation((string Title, string DescriptionFormat) textInformation, ChallengeData challengeData)
{
    public ChallengeData ChallengeData { get; } = challengeData;
    
    /// Description format are as follows
    /// Percent - "Bosses have {0:stat(per|0.#|%|s|u)} damage."
    /// eg. "Bosses have +35% more life per rank."
    /// Flat - "Elite enemies have {0:stat(val|0.#|s|u|*100)} movement."
    /// eg. "Elite enemies have +60 movement."
    /// See Documentation/Deep-Dives/ChallengeDataSmartFormatter.md for more information
    public (string Title, string DescriptionFormat) TextInformation { get; } = textInformation;
}