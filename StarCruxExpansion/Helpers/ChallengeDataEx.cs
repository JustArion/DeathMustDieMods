namespace Dawn.DMD.StarCruxExpansion.Helpers;

using System.Collections.Generic;
using Death.Darkness;
using Reflection;

public static class ChallengeDataEx
{
    // We'll maybe apply some save-state things if its requested.
    public static DarknessOptions ToDarknessOptions(this IEnumerable<ChallengeData> challengeData)
    {
        var options = new DarknessOptions();
        var challenges = options.Challenges();
        challenges.AddRange(challengeData.Select(data => new DarknessOptions.Challenge(data.Code)));

        return options;
    }

    /* From: Death.Data.Database.Darkness -- foreach -> IconPath
        Interface/StarCrux/CruxIcon_Boots_Spr
        Interface/StarCrux/CruxIcon_Yatagan_Spr
        Interface/StarCrux/CruxIcon_Knife_Spr
        Interface/StarCrux/CruxIcon_Bow_Spr
        Interface/StarCrux/CruxIcon_Velocity_Spr
        Interface/StarCrux/CruxIcon_CrossedSwords_Spr
        Interface/StarCrux/CruxIcon_Angle_Spr
        Interface/StarCrux/CruxIcon_Scythe_Spr
    */
    public enum ChallengeDataIcon
    {
        Boots,
        Yatagan,
        Knife,
        Bow,
        Velocity,
        CrossedSwords,
        Angle,
        Scythe,
    }


    private static readonly Dictionary<ChallengeDataIcon, string> _iconPathMap = new()
    {
        { ChallengeDataIcon.Boots, "Interface/StarCrux/CruxIcon_Boots_Spr" },
        { ChallengeDataIcon.Yatagan, "Interface/StarCrux/CruxIcon_Yatagan_Spr" },
        { ChallengeDataIcon.Knife, "Interface/StarCrux/CruxIcon_Knife_Spr" },
        { ChallengeDataIcon.Bow, "Interface/StarCrux/CruxIcon_Bow_Spr" },
        { ChallengeDataIcon.Velocity, "Interface/StarCrux/CruxIcon_Velocity_Spr" },
        { ChallengeDataIcon.CrossedSwords, "Interface/StarCrux/CruxIcon_CrossedSwords_Spr" },
        { ChallengeDataIcon.Angle, "Interface/StarCrux/CruxIcon_Angle_Spr" },
        { ChallengeDataIcon.Scythe, "Interface/StarCrux/CruxIcon_Scythe_Spr" },
    };
    
    public static string ToIconPath(ChallengeDataIcon icon) => _iconPathMap[icon];
}