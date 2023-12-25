using BepInEx;

namespace Dawn.DMD.LongerGames;

using System.Collections.Generic;
using System.Reflection;
using BepInEx.Configuration;
using BepInEx.Logging;
using Harmony;
using JetBrains.Annotations;

[BepInPlugin("dawn.dmd.longergames", "LongerGames", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    private const uint DEFAULT_MODDED_GAME_LENGTH_MINUTES = 35;
    public new static ManualLogSource Logger { get; private set; } = null!;
    internal static Plugin Instance { get; private set; } = null!;

    public static ConfigEntry<uint> GameLengthMinutesConfig = null!;

    [UsedImplicitly]
    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;
        Logger.LogInfo("The underworld torments you further! Your games are longer.");
        GameLengthMinutesConfig = Config.Bind("LongerGames", "GameLengthInMinutes", DEFAULT_MODDED_GAME_LENGTH_MINUTES, "The length of a game run in minutes");

        GameLengthMinutesConfig.SettingChanged += (_, _) => ContinueAfter20Mins_Patch.UpdateDurations();

        HarmonyLib.Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

        OnRunStart_Patch.OnRunStart += MonsterWaveModifier.OnRunStart;
    }
}