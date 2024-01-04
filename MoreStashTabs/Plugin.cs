using BepInEx;

namespace Dawn.DMD.MoreStashTabs;

using System.Reflection;
using BepInEx.Logging;
using JetBrains.Annotations;

[BepInPlugin("dawn.dmd.morestashtabs", "MoreStashTabs", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource ModLogger { get; private set; }
    internal static Plugin Instance { get; private set; }
    [UsedImplicitly]
    private void Awake()
    {
        ModLogger = Logger;
        Instance = this;
        Logger.LogInfo("You plead, \"Give us more stash tabs\" and someone answers!");
        HarmonyLib.Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
    }
}
