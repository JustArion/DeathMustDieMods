namespace Dawn.DMD.StarCruxExpansion.Helpers;

using BepInEx.Logging;

public static class LoggerEx
{
    public static void LogErrorMessage(this ManualLogSource logger, Exception e) => logger.LogError(e.Message);
}