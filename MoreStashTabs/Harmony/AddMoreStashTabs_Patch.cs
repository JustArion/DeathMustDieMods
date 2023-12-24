namespace Dawn.DMD.MoreStashTabs.Harmony;

using System;
using System.Linq;
using Death.Items;
using HarmonyLib;
using JetBrains.Annotations;

[HarmonyPatch(typeof(StashData))]
public class AddMoreStashTabs_Patch
{
    // This modifies new saves to have stash tabs
    [HarmonyPatch(MethodType.Constructor)]
    [HarmonyPrefix]
    [HarmonyPatch(
    [
        typeof(int)
    ])]
    [UsedImplicitly]
    private static bool Prefix(StashData __instance, ref int defaultPageCount)
    {
        // We do an if check here in-case the dev changes the default page value to 5 or higher then we want this mod to stop doing what its doing
        if (defaultPageCount is not (3 or 4)) 
            return true;
        
        defaultPageCount = 5;
        Plugin._Logger.LogDebug("Stash tabs is now 5 from 3");
        return true;
    }

    [HarmonyPatch(typeof(StashData), "TryParseSaveJson")]
    [HarmonyPostfix]
    private static void Postfix(ref bool __result, ref object __1)
    {
        try
        {
            var mimicState = SaveStateMimic.Create(__1);
            
            Plugin._Logger.LogDebug("Mimic state created");

            // 3 Pages is the known default
            if (mimicState.Pages.Length is not 3)
                return;

            Array.Resize(ref mimicState.Pages, 5);
            
            for (var i = 4; i < mimicState.Pages.Length; i++)
            {
                if (mimicState.Pages[i] != null)
                    continue; // We only populate empty pages

                mimicState.Pages[i] = new ItemGrid.SaveState
                {
                    Width = StashData.PageWidth,
                    Height = StashData.PageHeight,
                    Items = Enumerable.Range(0, StashData.PageWidth * StashData.PageHeight).Select(_ => new ItemSaveData()).ToArray()
                };
            }
            
            SaveStateMimic.SyncOriginal(mimicState, ref __1);
            Plugin._Logger.LogDebug("Mimic state synced");
        }
        catch (Exception e)
        {
            Plugin._Logger.LogError(e);
        }

    }
}