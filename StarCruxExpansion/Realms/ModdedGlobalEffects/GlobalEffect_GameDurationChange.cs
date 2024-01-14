namespace Dawn.DMD.StarCruxExpansion.Realms.ModdedGlobalEffects;

using Death.Darkness;
using RealmHelpers.GameDurationChangeHandler;

public class GlobalEffect_GameDurationChange(int durationDeltaMinutes) : ModdedGlobalEffectBase
{
    protected override void OnBecomeActive() => GameDurationHandler.GameDurationDeltaInMinutes += durationDeltaMinutes;


    protected override void OnBecomeInactive() => GameDurationHandler.GameDurationDeltaInMinutes -= durationDeltaMinutes;
}