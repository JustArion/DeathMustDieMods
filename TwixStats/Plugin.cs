using BepInEx;

namespace Dawn.DMD.TwixStats;

using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using BepInEx.Logging;
using JetBrains.Annotations;
using Death.Data;

[BepInPlugin("dawn.dmd.twixstats", "TwixStats", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    internal new static ManualLogSource Logger { get; private set; }
    internal static Plugin Instance { get; private set; }
    [UsedImplicitly]
    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;
        base.Logger.LogInfo("As reality bends, things arn't like they used to be, things are different; yet the same!");
        HarmonyLib.Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        
        
    }
}