namespace Dawn.DMD.StarCruxExpansion.Realms.RealmHelpers.GameDurationChangeHandler;

using Death.Run.Systems;
using Death.Run.Systems.Spawning;

public static class MonsterWaveModifier
{
    public static void OnRunStart(Facade_Run run)
    {
        var gameLength = GameDurationHandler.GetGameDurationInMinutes();
        var defaultGameLengthInMinutes = run.Options.WaveData.Last().BeginTimeSec / 60;
        
        var gameLengthMultiplier = (float)gameLength / defaultGameLengthInMinutes;
        
        ModLogger.LogDebug($"New run started! The game length multiplier is '{Math.Round(gameLengthMultiplier, 2)}'. The game should take approx {gameLength} minutes.");

        for (var index = 0; index < run.Options.WaveData.Count; index++)
        {
            var wave = run.Options.WaveData[index];
            if (wave.Mode == WaveData.EventMode.CustomEvent) 
                HandleBossFight(run, index, wave, gameLengthMultiplier);

            wave.BeginTimeSec = (int)(wave.BeginTimeSec * gameLengthMultiplier);
            wave.EndTimeSec = (int)(wave.EndTimeSec * gameLengthMultiplier);
        }
    }

    private static void HandleBossFight(Facade_Run run, int index, WaveData.Event wave, float gameLengthMultiplier)
    {
        var nextWave = index + 1 < run.Options.WaveData.Count ? run.Options.WaveData[index + 1] : null;
        var priorBeginTime = wave.BeginTimeSec;
        var newBeginTime = priorBeginTime * gameLengthMultiplier;

        var eventName = GenerateEventName(wave.EndOnMonsterKills, nextWave);

        ModLogger.LogDebug(
            $"{eventName} - BeginTime: {Math.Round(priorBeginTime / 60f, 2)} mins -> {Math.Round(newBeginTime / 60f, 2)} mins");
    }

    private static string GenerateEventName(string[] UniqueMonsters, WaveData.Event? nextWave)
    {
        if (UniqueMonsters.Length == 0 && nextWave != null)
            return $"[ {nextWave.MonsterCode} ] Event";
        
        return $"[ {string.Join(", ", UniqueMonsters)} ] Event";
    }
}