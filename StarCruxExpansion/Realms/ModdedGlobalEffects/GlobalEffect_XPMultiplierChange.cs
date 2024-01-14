namespace Dawn.DMD.StarCruxExpansion.Realms.ModdedGlobalEffects;

using Death.Run.Core;
using Death.Run.Core.Systems;
using Death.Run.Systems;

public class GlobalEffect_XPMultiplierChange(int xpMultiplier) : ModdedGlobalEffectBase
{
    protected override void OnBecomeActive()
    {
        var playerManager = RunSystems.Get<System_PlayerManager>();

        ModLogger.LogDebug($"XP Multiplier on Player is {xpMultiplier}x");
        playerManager.Player.Unit.Entity.Stats.AddBonus(StatId.XpMultiplier, xpMultiplier);
    }

    protected override void OnBecomeInactive()
    {
        var playerManager = RunSystems.Get<System_PlayerManager>();

        playerManager.Player.Unit.Entity.Stats.AddBonus(StatId.XpMultiplier, -xpMultiplier);
    }
}