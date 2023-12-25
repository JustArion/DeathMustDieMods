namespace Dawn.DMD.LongerGames;

using Death.Run.Systems;
using ReflectionHelpers;

internal static class GameRulesEx
{
    internal static void SetRunDurationInMinutes(this GameRules rules, float runDurationMinutes) =>
        Set_RunDurationInMinutes.Value(rules, runDurationMinutes);

    internal static float GetRunDurationInMinutes(this GameRules rules) => Get_RunDurationInMinutes.Value(rules);
    
    private static readonly Lazy<Action<GameRules, float>> Set_RunDurationInMinutes = new(() =>
    {
        var field = typeof(GameRules).GetField("_runDurationMinutes", BindingFlags.NonPublic | BindingFlags.Instance);

        if (field == null)
            throw new NullReferenceException(nameof(field));

        return field.CreateFieldSetter<GameRules, float>();
    });
    private static readonly Lazy<Func<GameRules, float>> Get_RunDurationInMinutes = new(() =>
    {
        var field = typeof(GameRules).GetField("_runDurationMinutes", BindingFlags.NonPublic | BindingFlags.Instance);

        if (field == null)
            throw new NullReferenceException(nameof(field));

        return field.CreateFieldGetter<GameRules, float>();
    });
}