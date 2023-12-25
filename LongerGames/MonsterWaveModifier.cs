namespace Dawn.DMD.LongerGames;

using BepInEx.Configuration;
using Claw.Core;
using Death.Run.Systems;
using Death.Run.Systems.Spawning;

public static class MonsterWaveModifier
{
    public static void OnRunStart(Facade_Run run)
    {
        // If the value is 0, we reset the game length to 20mins
        var gameLength = GameLengthMinutesConfig.Value == 0 ? 20 : GameLengthMinutesConfig.Value;
        var defaultGameLength = run.Options.WaveData.Last().BeginTimeSec / 60;
        
        var gameLengthMultiplier = (float)gameLength / defaultGameLength;
        
        Logger.LogDebug($"New run started! The game length multiplier is '{Math.Round(gameLengthMultiplier, 2)}'");

        for (var index = 0; index < run.Options.WaveData.Count; index++)
        {
            var wave = run.Options.WaveData[index];
            if (wave.Mode == WaveData.EventMode.CustomEvent)
            {
                var nextWave = index + 1 < run.Options.WaveData.Count ? run.Options.WaveData[index + 1] : null;
                var priorBeginTime = wave.BeginTimeSec;
                var newBeginTime = priorBeginTime * gameLengthMultiplier;

                var eventName = GenerateEventName(wave.EndOnMonsterKills, nextWave);

                Logger.LogDebug(
                    $"{eventName} - BeginTime: {Math.Round(priorBeginTime / 60f, 2)} mins -> {Math.Round(newBeginTime / 60f, 2)} mins");
            }

            wave.BeginTimeSec = (int)(wave.BeginTimeSec * gameLengthMultiplier);
            wave.EndTimeSec = (int)(wave.EndTimeSec * gameLengthMultiplier);
        }
    }

    private static string GenerateEventName(string[] UniqueMonsters, WaveData.Event? nextWave)
    {
        if (UniqueMonsters.Length == 0 && nextWave != null)
            return $"[ {nextWave.MonsterCode} ] Event";
        
        return $"[ {string.Join(", ", UniqueMonsters)} ] Event";
    }
}