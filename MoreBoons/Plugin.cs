using BepInEx;

namespace Dawn.DMD.MoreBoons;

using System.Reflection;
using BepInEx.Logging;
using JetBrains.Annotations;

[BepInPlugin("dawn.dmd.moreboons", "MoreBoons", "1.0.1")]
public class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource ModLogger { get; private set; }
    internal static Plugin Instance { get; private set; }
    [UsedImplicitly]
    private void Awake()
    {
        ModLogger = Logger;
        Instance = this;
        Logger.LogInfo("Begging the gods for randomness!");
        HarmonyLib.Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
    }
}