namespace Dawn.DMD.StarCruxExpansion.UI;

using System.Collections.Generic;
using Claw.UserInterface.Selection;
using Cysharp.Threading.Tasks;
using Death.Darkness;
using Death.TimesRealm.UserInterface.Darkness;
using Harmony;
using Reflection;
using UnityEngine;

[RequireComponent(typeof(Screen_Darkness))]
[DisallowMultipleComponent]
public class ModdedRealmManager : MonoBehaviour
{
    internal static readonly Dictionary<string, HashSet<ChallengeData>> _moddedRealms = new();

    private Screen_Darkness _darknessManager;
    private GUI_Darkness _darknessGui;
    private GUI_RealmNameCarousel _realmNameCarousel;
    private ModdedDarknessController _darknessController;
    
    
    public void Awake()
    {
        _darknessManager = GetComponent<Screen_Darkness>();
        _darknessGui = GetComponentInChildren<GUI_Darkness>(includeInactive: true);
        
        var realmNamePanel = _darknessGui.transform.Find(UI_Constants.GAMEOBJECT_REALM_NAME);
        _realmNameCarousel = realmNamePanel.gameObject.AddComponent<GUI_RealmNameCarousel>();

        #if DEBUG
        _realmNameCarousel.EnableDebugMode();
        #endif

        _realmNameCarousel.SubscribeToDefaultRealm(ReturnToOuterCircle);
        
        foreach (var realm in _moddedRealms) 
            _realmNameCarousel.AddRealmName(realm.Key, OnRealmNavigate);
        
        
    }

    public void OnRealmNavigate(string realmName)
    {
        if (!_moddedRealms.TryGetValue(realmName, out var realmController))
        {
            Plugin.Logger.LogWarning($"Realm controller not found for realm '{realmName}'");
            return;
        }

        _darknessGui.SetDataAsync(realmController).Forget();
    }


    public void NavigateToNextRealm() => _realmNameCarousel.NavigateNextRealm();

    public void NavigateToPreviousRealm() => _realmNameCarousel.NavigatePreviousRealm();


    private void ReturnToOuterCircle()
    {
        _realmNameCarousel.ResetToDefaultRealm();
        _darknessGui.SetDataAsync(InterceptStarCruxData_Patch.VanillaDarknessController).Forget();
    }

    private void OnDestroy() => Destroy(_realmNameCarousel);


    public static void AddModdedRealm(string realmName, params ChallengeData[] challengeData)
    {
        if (string.IsNullOrWhiteSpace(realmName))
        {
            Plugin.Logger.LogWarning("You can not add a realm with a null or empty name!");
            return;
        }

        _moddedRealms.Add(realmName, challengeData.ToHashSet());
        Plugin.Logger.LogDebug($"Added realm {realmName} with {challengeData.Length} challenges");
        Plugin.Logger.LogDebug($"Total modded realms: {_moddedRealms.Count}");
    }

    public static void RemoveModdedRealm(string realmName)
    {
        if (string.IsNullOrWhiteSpace(realmName))
        {
            Plugin.Logger.LogWarning("You can not remove the default realm!");
            return;
        }

        _moddedRealms.Remove(realmName);
    }
}