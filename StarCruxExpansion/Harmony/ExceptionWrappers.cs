namespace Dawn.DMD.StarCruxExpansion.Harmony;

internal static class ExceptionWrappers
{
    internal static void Wrap(Action act, Action<Exception> onError = null)
    {
        try
        {
            act();
        }
        catch (Exception e)
        {
            onError?.Invoke(e);
        }
    }

    internal static T Wrap<T>(Func<T> func, T defaultValue, Action<Exception> onError = null)
    {
        try
        {
            return func();
        }
        catch (Exception e)
        {
            onError?.Invoke(e);
            return defaultValue;
        }

    }

    public static void Wrap(Action obj)
    {
        try
        {
            obj();
        }
        catch (Exception e)
        {
            Logger.LogError(e);
        }
    }
}