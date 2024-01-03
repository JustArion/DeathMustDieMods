namespace Dawn.DMD.StarCruxExpansion.Reflection;

using System.Collections.Generic;
using Death.Darkness;
using ReflectionHelpers;

public static class ChallengeDataReflection
{
    private static readonly Lazy<Func<ChallengeData, List<ChallengeData.Effect>>> _effects = PrivateFieldsHelper.CreateFieldGetterDelegate<ChallengeData, List<ChallengeData.Effect>>(nameof(_effects));

    public static List<ChallengeData.Effect> Effects(this ChallengeData challenge) => _effects.Value(challenge);
}