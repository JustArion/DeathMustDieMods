namespace Dawn.DMD.StarCruxExpansion;

public readonly struct ChallengeDataTextInformation(string title, string descriptionFormat)
{
    public readonly string Title = title;
    /// Description format are as follows
    /// Percent - "Bosses have {0:stat(per|0.#|%|s|u)} damage."
    /// eg. "Bosses have +35% more life per rank."
    /// Flat - "Elite enemies have {0:stat(val|0.#|s|u|*100)} movement."
    /// eg. "Elite enemies have +60 movement."
    public readonly string DescriptionFormat = descriptionFormat;
}