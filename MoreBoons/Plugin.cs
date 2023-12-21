using BepInEx;

namespace Dawn.DMD.MoreBoons;

using System.Reflection;
using BepInEx.Logging;
using JetBrains.Annotations;

[BepInPlugin("dawn.dmd.moreboons", "MoreBoons", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource _Logger { get; private set; }
    internal static Plugin _Instance { get; private set; }
    [UsedImplicitly]
    private void Awake()
    {
        _Logger = Logger;
        _Instance = this;
        Logger.LogInfo("Begging the gods for randomness!");
        HarmonyLib.Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
    }
}