namespace Dawn.DMD.StarCruxExpansion;

using Death.Run.Core;

public class ModdedStatArray<T> : StatArray<T>
{
    public ModdedStatArray(params (StatId StatType, T stat)[] stats)
    {
        foreach (var (statType, stat) in stats) 
            Set(statType, stat);
    }
}