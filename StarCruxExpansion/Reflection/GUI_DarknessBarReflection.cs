namespace Dawn.DMD.StarCruxExpansion.Reflection;

using Death.Darkness;
using Death.TimesRealm.UserInterface.Darkness;
using ReflectionHelpers;

public static class GUI_DarknessBarReflection
{
    private static readonly Lazy<Func<GUI_DarknessBar, IDarknessController>> _controller = PrivateFieldsHelper.CreateFieldGetterDelegate<GUI_DarknessBar, IDarknessController>(nameof(_controller));

    public static IDarknessController Controller(this GUI_DarknessBar challenge) => _controller.Value(challenge);
}