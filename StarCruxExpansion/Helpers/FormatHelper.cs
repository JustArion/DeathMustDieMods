namespace Dawn.DMD.StarCruxExpansion.Helpers;

using Death.Run.Core;
using Reflection;

public static class FormatHelper
{
    public static string SetDataType(StatId statType, StatChangeType statChangeType)
    {
        var statIdMap = StatIdUtilsReflection.ShortCodeToStatId();
        
        var statCode = statIdMap.First(x => x.Value  == statType).Key;
        
        var format = GetStatChangeTypeFormat(statChangeType);
        
        return format.Replace(STAT_TYPE_HOLDER, statCode);
    }

    private const string STAT_TYPE_HOLDER = "STAT_TYPE";
    private static string GetStatChangeTypeFormat(StatChangeType changeType)
    {
        return changeType switch
        {
            StatChangeType.Flat => "{0:stat(STAT_TYPE_HOLDER|0.#|s|u|*100)}",
            StatChangeType.BoonMod => throw new NotSupportedException(),
            StatChangeType.ItemMod => throw new NotSupportedException(),
            StatChangeType.Bonus => "{0:stat(STAT_TYPE|0.#|%|s|u)}",
            StatChangeType.TalentMod => throw new NotSupportedException(),
            StatChangeType.AdditionalItemValue => throw new NotSupportedException(),
            StatChangeType.Darkness => throw new NotSupportedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(changeType), changeType, null)
        };
    }
}