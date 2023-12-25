using BepInEx;

namespace Dawn.DMD.LimitlessEncounters;

using System.Reflection;
using BepInEx.Logging;
using JetBrains.Annotations;

[BepInPlugin("dawn.dmd.allgods", "LimitlessEncounters", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    internal new static ManualLogSource Logger { get; private set; }
    internal static Plugin Instance { get; private set; }
    [UsedImplicitly]
    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;
        base.Logger.LogInfo("Waking up all the gods.");
        HarmonyLib.Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
    }
}