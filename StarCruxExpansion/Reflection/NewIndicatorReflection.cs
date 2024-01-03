namespace Dawn.DMD.StarCruxExpansion.Reflection;

using System.Collections.Generic;
using Death.Darkness;
using Death.TimesRealm.Behaviours;
using ReflectionHelpers;

public static class NewIndicatorReflection
{
    private static readonly Lazy<Action<NewIndicator, string>> _id = PrivateFieldsHelper.CreateFieldSetterDelegate<NewIndicator, string>(nameof(_id));

    public static void SetId(this NewIndicator indicator, string value) => _id.Value(indicator, value);
    
    // ---
}