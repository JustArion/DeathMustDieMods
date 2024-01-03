namespace Dawn.DMD.StarCruxExpansion.Reflection;

using System.Collections.Generic;
using Death.Run.Core;
using ReflectionHelpers;

public static class StatIdUtilsReflection
{
    private static readonly Lazy<Func<Dictionary<string, StatId>>> _ShortCodeToStatId = PrivateFieldsHelper.CreateStaticFieldGetterDelegate<Dictionary<string, StatId>>(
        typeof(StatIdUtils), nameof(ShortCodeToStatId));

    public static Dictionary<string, StatId> ShortCodeToStatId() => _ShortCodeToStatId.Value();
}