﻿using Death.TimesRealm.UserInterface.Darkness;

namespace Dawn.DMD.StarCruxExpansion.Realms;

using System.Collections.Generic;
using Death.Darkness;
using Harmony;

public class ModdedDarknessController(DarknessOptions options, IEnumerable<ChallengeData> challengeData) : IDarknessController
// public abstract class ModdedDarknessController(DarknessOptions options, IEnumerable<ChallengeData> challengeData, DarknessDropBonus dropBonus, Unlocks unlocks) : IDarknessController
{
    protected readonly DarknessOptions _options = options;
    // protected readonly DarknessDropBonus _dropBonus = dropBonus;
    // protected readonly Unlocks _unlocks = unlocks;

    public DarknessOptions Options => _options;

    public virtual IEnumerable<ChallengeData> GetAllChallenges() => challengeData;


    public virtual bool IsUnlocked(ChallengeData challenge) => true;
    // For the future maybe, if persistence is implemented
    // public virtual bool IsUnlocked(ChallengeData challenge) => _unlocks.IsUnlocked(challenge);


    public virtual int GetLevelFor(ChallengeData challenge) => _options.GetLevel(challenge.Code);


    public virtual bool TryIncreaseLevel(ChallengeData challenge)
    {
        if (!CanIncreaseLevel(challenge))
            return false;

        _options.IncreaseChallenge(challenge.Code);
        return true;
    }


    public virtual bool TryDecreaseLevel(ChallengeData challenge)
    {
        if (!CanDecreaseLevel(challenge))
                return false;

        _options.DecreaseChallenge(challenge.Code);
        return true;
    }

    public virtual bool CanIncreaseLevel(ChallengeData challenge) =>
        _options.GetLevel(challenge.Code) < challenge.MaxLevel;

    public virtual bool CanDecreaseLevel(ChallengeData challenge) => _options.GetLevel(challenge.Code) > 0;

    public virtual int MaxPoints { get; } = new Lazy<int>(() => challengeData.Sum(challenge => challenge.MaxLevel)).Value;
    public virtual int TotalPoints => _options.GetTotalPoints();

    int IDarknessController.MaxPoints
    {
        get
        {
            // MaxPoints + InterceptStarCruxData_Patch.VanillaDarknessController.MaxPoints;
            var vanillaMaxPoints = RaiseItemRarityCeiling_Patch.GetVanillaMaxPoints(InterceptStarCruxData_Patch.VanillaDarknessController);
            var moddedMaxPoints = RaiseItemRarityCeiling_Patch.GetModdedMaxPoints();

            return vanillaMaxPoints + moddedMaxPoints;
        }
    }

    int IDarknessController.TotalPoints => TotalPoints;
}