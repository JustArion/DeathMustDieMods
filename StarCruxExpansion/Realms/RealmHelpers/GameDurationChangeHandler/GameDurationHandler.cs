namespace Dawn.DMD.StarCruxExpansion.Realms.RealmHelpers.GameDurationChangeHandler;

using UnityEngine;

public static class GameDurationHandler
{
    public const int DEFAULT_GAME_DURATION_MINS = 20;

    private static int _gameDurationDeltaInMinutes;

    public static int GameDurationDeltaInMinutes
    {
        get => _gameDurationDeltaInMinutes;
        set
        {
            ModLogger.LogDebug($"Game Duration Changed from {DEFAULT_GAME_DURATION_MINS - _gameDurationDeltaInMinutes} to {DEFAULT_GAME_DURATION_MINS - value} minutes");
            _gameDurationDeltaInMinutes = value;           
        }
    }
    
    
    // Minimum game duration is now 5min
    public static int GetGameDuration() => 
        Mathf.Clamp(DEFAULT_GAME_DURATION_MINS + GameDurationDeltaInMinutes, 5, int.MaxValue);
}