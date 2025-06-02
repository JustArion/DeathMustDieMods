using BepInEx;
using Dawn.DMD.NoUnfocusedPauses.Harmony;
using Death.Run.Systems;

namespace Dawn.DMD.NoUnfocusedPauses;

using System.Reflection;
using BepInEx.Logging;
using JetBrains.Annotations;

[BepInPlugin("dawn.dmd.nounfocusedpauses", "NoUnfocusedPauses", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource ModLogger { get; private set; }
    internal static Plugin Instance { get; private set; }
    [UsedImplicitly]
    private void Awake()
    {
        ModLogger = Logger;
        Instance = this;
        Logger.LogInfo("Pauses are now optional!");
        HarmonyLib.Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
    }
}