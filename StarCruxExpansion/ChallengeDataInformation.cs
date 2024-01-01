namespace Dawn.DMD.StarCruxExpansion;

using Death.Darkness;

public readonly struct ChallengeDataInformation(ChallengeDataTextInformation textInformation, ChallengeData challengeData)
{
    public ChallengeData ChallengeData { get; } = challengeData;
    
    public ChallengeDataTextInformation TextInformation { get; } = textInformation;
}