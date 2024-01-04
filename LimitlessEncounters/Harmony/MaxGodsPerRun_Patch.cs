namespace Dawn.DMD.LimitlessEncounters.Harmony;

using System.Collections;
using Death.Run.Systems;
using JetBrains.Annotations;

[HarmonyPatch(MethodType.Constructor)]
[HarmonyPatch(typeof(GameRules))]
public class MaxGodsPerRun_Patch
{
    [UsedImplicitly]
    private static void Postfix(GameRules __instance) => Instance.StartCoroutine(WaitOnce(__instance));

    // The class is deserialized. A regular Constructor patch wouldn't have worked. We use a Coroutine instead.
    private static IEnumerator WaitOnce(GameRules rules)
    {
        yield return null;

        try
        {
            var defaultGodsPerRun = rules.GodsPerRun;
            // var newGodsPerRun = Database.Gods.All.Count(); // We Can't use this since its not initialized yet and throws a NRE
            const int NEW_GODS_PER_RUN = int.MaxValue;
            rules.GodsPerRun = NEW_GODS_PER_RUN;
            if (defaultGodsPerRun != NEW_GODS_PER_RUN)
                ModLogger.LogDebug($"Gods per run changed from [{defaultGodsPerRun}] to [{rules.GodsPerRun}] Gods per run");
        }
        catch (Exception e)
        {
            ModLogger.LogError(e);
        }

    }
}