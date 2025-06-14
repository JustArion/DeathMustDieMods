namespace Dawn.DMD.StarCruxExpansion.Realms.ModdedGlobalEffects;

using System.Threading;
using Cysharp.Threading.Tasks;
using Death.Darkness;
using Death.Dialogues;
using Death.Dialogues.Core;
using Death.Run.Core;
using Death.Run.Core.Boons;
using Death.Run.Core.Systems;
using Death.Run.Systems;
using Death.Utils;
using UnityEngine;

public class GlobalEffect_BoonLossOverTime(GlobalEffect_BoonLossOverTime.BoonLossOptions options) : ModdedGlobalEffectBase
{
    public record struct BoonLossOptions(double XPLossAfterXMins, Predicate<BoonInstance> BoonDiscriminator);
    private CancellationTokenSource _cts;
    protected override void OnBecomeActive()
    {
        var playerManager = RunSystems.Get<System_PlayerManager>();
        _cts = new();

        ModLogger.LogDebug($"Boon Loss Over Time has become active, waiting {options.XPLossAfterXMins} mins.");
        ApplyBoonLossOverTimeAsync(playerManager, options, _cts.Token).Forget();
    }

    protected override void OnBecomeInactive()
    {
        ModLogger.LogDebug($"Boon loss per {options.XPLossAfterXMins} mins cancelled.");
        _cts.Cancel();
    }

    private static async UniTaskVoid ApplyBoonLossOverTimeAsync(System_PlayerManager playerManager, BoonLossOptions options, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            // Only ticks when the game is unpaused.
            await UniTaskUtils.DelaySeconds((float)(options.XPLossAfterXMins * 60), cancellationToken: token);

            
            var boons = playerManager.Player.Boons.Where(x => options.BoonDiscriminator(x)).ToArray();
            var len = boons.Length;

            // 1 - Dash
            // 2 - Special Skill
            // We don't remove that
            if (len <= 2)
            {
                ModLogger.LogDebug("No boon to remove.");
                continue;
            }

            var randomIndex = Random.Range(2, len);

            var boon = boons[randomIndex];
            playerManager.Player.Boons.Remove(boon);

            ModLogger.LogDebug($"Boon '{boon.Code}' has been lost after {options.XPLossAfterXMins} mins.");
        }
    }
}