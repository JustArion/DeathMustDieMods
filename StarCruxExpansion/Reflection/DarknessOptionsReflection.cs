namespace Dawn.DMD.StarCruxExpansion.Reflection;

using System.Collections.Generic;
using Death.Darkness;
using ReflectionHelpers;

public static class DarknessOptionsReflection
{
    private static readonly Lazy<Func<DarknessOptions, List<DarknessOptions.Challenge>>> _challenges = PrivateFieldsHelper.CreateFieldGetterDelegate<DarknessOptions, List<DarknessOptions.Challenge>>(nameof(_challenges));

    public static List<DarknessOptions.Challenge> Challenges(this DarknessOptions challenge) => _challenges.Value(challenge);
}