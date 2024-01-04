namespace Dawn.DMD.LongerGames.Harmony;

using System.Collections;
using System.Collections.Generic;
using Death.Run.Systems;
using JetBrains.Annotations;

[HarmonyPatch(MethodType.Constructor)]
[HarmonyPatch(typeof(GameRules))]
public class ContinueAfter20Mins_Patch
{
    
    private static HashSet<WeakReference<GameRules>> Instances { get; } = [];
    [UsedImplicitly]
    private static void Postfix(GameRules __instance) => Instance.StartCoroutine(WaitOnce(__instance));

    private const float EPSILON = 0.001f;
    
    // The class is deserialized. A regular Constructor patch wouldn't have worked. We use a Coroutine instead.
    private static IEnumerator WaitOnce(GameRules rules)
    {
        yield return null;

        try
        {
            Instances.Add(new(rules));

            var defaultRunDurationInMinutes = rules.RunDurationSeconds / 60;
            rules.SetRunDurationInMinutes(GameLengthMinutesConfig.Value);
            if (Math.Abs(defaultRunDurationInMinutes - rules.GetRunDurationInMinutes()) > EPSILON )
                ModLogger.LogDebug($"Run duration changed from '{Math.Round(defaultRunDurationInMinutes, 2)}' minutes to '{Math.Round(rules.GetRunDurationInMinutes(), 2)}' minutes");
        }
        catch (Exception e)
        {
            ModLogger.LogError(e);
        }
    }


    public static void UpdateDurations()
    {
        var weakReferencesToRemove = new List<Action>();
        var updatedInstancesAmount = 0;
        foreach (var weakGameRules in Instances)
        {
            if (weakGameRules.TryGetTarget(out var gameRules))
            {
                gameRules.SetRunDurationInMinutes(GameLengthMinutesConfig.Value);
                updatedInstancesAmount++;
            }
            else
                weakReferencesToRemove.Add(() => Instances.Remove(weakGameRules));
        }
        
        ModLogger.LogDebug($"Updated '{updatedInstancesAmount}' GameRules instances");
        weakReferencesToRemove.ForEach(x => x());
    }
}