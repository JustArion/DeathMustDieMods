﻿namespace Dawn.DMD.MoreStashTabs.Harmony;

using Death.Items;
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
    private static void Prefix(StashData __instance, ref int defaultPageCount)
    {
        var origCount = defaultPageCount;
        // We do an if check here in-case the dev changes the default page value to 5 or higher than we want this mod to stop doing what its doing
        if (defaultPageCount > 5) 
            return;
        
        defaultPageCount = 5;
        ModLogger.LogDebug($"Your Stash tabs are now {defaultPageCount} from {origCount}");
    }

    [HarmonyPatch(typeof(StashData), "TryParseSaveJson")]
    [HarmonyPostfix]
    private static void Postfix(ref bool __result, ref object __1)
    {
        try
        {
            var mimicState = SaveStateMimic.Create(__1);

            // 3 Pages is the known default
            if (mimicState.Pages.Length > 5)
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
            ModLogger.LogDebug("Mimic state synced");
        }
        catch (Exception e)
        {
            ModLogger.LogError(e);
        }

    }
}