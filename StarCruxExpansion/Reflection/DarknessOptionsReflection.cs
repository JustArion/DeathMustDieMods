namespace Dawn.DMD.StarCruxExpansion.Reflection;

using System.Collections.Generic;
using Death.Darkness;
using ReflectionHelpers;

public static class DarknessOptionsReflection
{
    private static readonly Lazy<Func<DarknessOptions, List<DarknessOptions.Challenge>>> _challenges = new(() =>
    {
        var challengesInfo = typeof(DarknessOptions).GetField(nameof(_challenges), BindingFlags.NonPublic | BindingFlags.Instance);

        if (challengesInfo != null)
            return challengesInfo.CreateFieldGetter<DarknessOptions, List<DarknessOptions.Challenge>>();
        
        
        Logger.LogError($"Unable to find field {nameof(_challenges)}.");
        throw new NullReferenceException(nameof(challengesInfo));

    });

    public static List<DarknessOptions.Challenge> Challenges(this DarknessOptions options) => _challenges.Value(options);
}