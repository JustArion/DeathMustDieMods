﻿namespace Dawn.DMD.MoreBoons.Harmony;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Death.Run.Core.Boons;
using Death.Run.Systems;
using Death.Run.Systems.Rewards;
using HarmonyLib;
using JetBrains.Annotations;

[HarmonyPatch(typeof(BoonManager))]
public class IgnoreSpecialSlots_Patch1
{
    [UsedImplicitly]
    [HarmonyPostfix]
    [HarmonyPatch(nameof(BoonManager.SlotHoldsDefault), typeof(BoonSlot))]
    private static void SlotHoldsDefault(BoonManager __instance, BoonSlot slot, ref bool __result)
    {
        if (__result) return;
        __result = true;
    }
    
    [UsedImplicitly]
    [HarmonyPostfix]
    [HarmonyPatch(nameof(BoonManager.SlotIsFree), typeof(BoonSlot))]
    private static void SlotIsFree(BoonManager __instance, BoonSlot slot, ref bool __result)
    {
        if (__result) return;
        __result = true;
    }
}

[HarmonyPatch(typeof(RewardGenerator), "CheckSpecialSlotOffer")]
public class IgnoreSpecialSlots_Patch2
{
    [UsedImplicitly]
    private static void Postfix(RewardGenerator __instance, BoonSlot __0, ref bool __result, ref Boon __3)
    {
        try
        {
            if (!__result)
                return;

            var _boonManager = _getBoonManager.Value(__instance);

            var approximateMatch = StripClass(__3!.Code);
            Plugin._Logger.LogDebug($"Found Special Slot Item '{__3!.Code}'");

            if (!_boonManager.Any(x => x.Code.StartsWith(approximateMatch))) 
                return;
            
            Plugin._Logger.LogDebug($"Found Duplicate '{__3.Code}' from approx match {approximateMatch}");
            __result = false;
            __3 = null;
        }
        catch (Exception e)
        {
            Plugin._Logger.LogError(e);
        }

    }

    private static string StripClass(string code)
    {
        var buffer = new Stack<List<char>>();
        foreach (var c in code)
        {
            if (char.IsUpper(c))
                buffer.Push([c]);
            else buffer.Peek().Add(c);
        }

        if (buffer.Count != 3)
            return code;
        
        buffer.Pop();
        var retVal = new List<char>();
        var val1 = buffer.Pop();
        var val2 = buffer.Pop();
        retVal.AddRange(val2);
        retVal.AddRange(val1);
        return new string(retVal.ToArray());
    }

    private static readonly Lazy<Func<RewardGenerator, BoonManager>> _getBoonManager = new(() =>
    {
        var boonManager = typeof(RewardGenerator).GetField("_boonManager", BindingFlags.NonPublic | BindingFlags.Instance);
        return generator => (BoonManager)boonManager!.GetValue(generator);
    });
}