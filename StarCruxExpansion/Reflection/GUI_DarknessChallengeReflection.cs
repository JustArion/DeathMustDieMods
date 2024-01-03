namespace Dawn.DMD.StarCruxExpansion.Reflection;

using Death.Darkness;
using Death.TimesRealm.UserInterface.Darkness;
using Death.UserInterface.Localization;
using ReflectionHelpers;
using TMPro;

public static class GUI_DarknessChallengeReflection
{
    private static readonly Lazy<Func<GUI_DarknessChallenge, TextMeshProUGUI>> _titleText = PrivateFieldsHelper.CreateFieldGetterDelegate<GUI_DarknessChallenge, TextMeshProUGUI>(nameof(_titleText));

    public static TextMeshProUGUI TitleText(this GUI_DarknessChallenge challenge) => _titleText.Value(challenge);
    
    // ---
    
    private static readonly Lazy<Func<GUI_DarknessChallenge, TextMeshProUGUI>> _descriptionText = PrivateFieldsHelper.CreateFieldGetterDelegate<GUI_DarknessChallenge, TextMeshProUGUI>(nameof(_descriptionText));

    public static TextMeshProUGUI DescriptionText(this GUI_DarknessChallenge challenge) => _descriptionText.Value(challenge);
    
    // ---

    private static readonly Lazy<Func<GUI_DarknessChallenge, LocalizedTable>> _descriptionTable = PrivateFieldsHelper.CreateFieldGetterDelegate<GUI_DarknessChallenge, LocalizedTable>(nameof(_descriptionTable));

    public static LocalizedTable DescriptionTable(this GUI_DarknessChallenge challenge) => _descriptionTable.Value(challenge);
    
    // ---

    private static readonly Lazy<Func<GUI_DarknessChallenge, ChallengeData>> _challenge =
        PrivateFieldsHelper.CreateFieldGetterDelegate<GUI_DarknessChallenge, ChallengeData>(nameof(_challenge));

    public static ChallengeData Challenge(this GUI_DarknessChallenge challenge) => _challenge.Value(challenge);
    
    // ---
    
    private static readonly Lazy<Func<GUI_DarknessChallenge, int>> _level =
        PrivateFieldsHelper.CreateFieldGetterDelegate<GUI_DarknessChallenge, int>(nameof(_level));
    
    
    public static int Level(this GUI_DarknessChallenge challenge) => _level.Value(challenge);
}