namespace Dawn.DMD.TwixStats.Harmony;

using Death.Data.Tables;
using Death.Run.Core.Boons;
using HarmonyLib;

[HarmonyPatch(typeof(BoonTable), nameof(BoonTable.Add))]
[HarmonyPatch(
[
    typeof(Boon)
])]
public class InterceptLoadingBoons_Patch
{
    private static bool Prefix(ref Boon boon)
    {
        try
        {
            
            
        }
        catch (Exception e)
        {
            Logger.LogError(e);
        }

        return true;
    }
}