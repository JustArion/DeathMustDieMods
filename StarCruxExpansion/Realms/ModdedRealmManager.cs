namespace Dawn.DMD.StarCruxExpansion.Realms.UI;

using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Death.TimesRealm.UserInterface.Darkness;
using DMD.StarCruxExpansion.UI;
using Harmony;
using Helpers;
using Reflection;
using UnityEngine;

[RequireComponent(typeof(Screen_Darkness))]
[DisallowMultipleComponent]
public class ModdedRealmManager : MonoBehaviour
{
    internal static readonly List<RealmData> _moddedRealms = [];

    internal static readonly HashSet<ChallengeDataInformation> _allModdedChallenges = [];    

    private Screen_Darkness _darknessManager;
    private GUI_Darkness _darknessGui;
    private GUI_RealmNameCarousel _realmNameCarousel;
    private ModdedDarknessController _darknessController;
    private AffordanceHandler _affordanceHandler;
    
    
    public void Awake()
    {
        _darknessManager = GetComponent<Screen_Darkness>();
        _darknessGui = GetComponentInChildren<GUI_Darkness>(includeInactive: true);
        
        var realmNamePanel = _darknessGui.transform.Find(UI_Constants.GAMEOBJECT_REALM_NAME);
        _realmNameCarousel = realmNamePanel.gameObject.AddComponent<GUI_RealmNameCarousel>();

        #if DEBUG
        // _realmNameCarousel.EnableDebugMode();
        #endif

        _realmNameCarousel.SubscribeToDefaultRealm(ReturnToOuterCircle);

        _affordanceHandler = _realmNameCarousel.NextRealmButton.AddComponent<AffordanceHandler>();
        _affordanceHandler.SetData("ModdedRealm_Affordance");
        _realmNameCarousel.NextRealmButton.onClick.AddListener(_affordanceHandler.DismissAffordance);
        
        foreach (var realm in _moddedRealms) 
            _realmNameCarousel.AddRealmName(realm.RealmName, OnRealmNavigate);
    }

    public bool HasPages => _moddedRealms.Count > 1;

    public void OnRealmNavigate(string realmName)
    {
        var realm = _moddedRealms.FirstOrDefault(x => x.RealmName == realmName);
        if (realm == null)
        {
            ModLogger.LogWarning($"Realm controller not found for realm '{realmName}'");
            return;
        }

        var challengeData = realm.Challenges.ToArray();

        _darknessGui.SetDataAsync(_darknessController = new(realm.options, challengeData)).Forget();
    }


    public bool NavigateToNextRealm() => _realmNameCarousel.NavigateNextRealm();

    public bool NavigateToPreviousRealm() => _realmNameCarousel.NavigatePreviousRealm();

    // This is a visual fix for re-opening the page and having the carousel going to the same page.
    private void OnDisable() => _realmNameCarousel.ResetToDefaultRealm();

    private void ReturnToOuterCircle()
    {
        _realmNameCarousel.ResetToDefaultRealm();
        _darknessGui.SetDataAsync(InterceptStarCruxData_Patch.VanillaDarknessController).Forget();
    }

    private void OnDestroy() => Destroy(_realmNameCarousel);


    public static void AddModdedRealm(RealmData realmData)
    {
        if (string.IsNullOrWhiteSpace(realmData.RealmName))
        {
            ModLogger.LogWarning("You can not add a realm with a null or empty name!");
            return;
        }

        if (!realmData.Challenges.Any())
        {
            ModLogger.LogWarning("You can not add a realm with no challenges!");
            return; 
        }

        _moddedRealms.Add(realmData);
        
        foreach (var challengeData in realmData.ChallengesInformation) 
            _allModdedChallenges.Add(challengeData);
        
        ModLogger.LogDebug($"Added realm {realmData.RealmName} with {realmData.Challenges.Count()} challenges");
        ModLogger.LogDebug($"Total modded realms: {_moddedRealms.Count}");
    }

    public static void RemoveModdedRealm(string realmName)
    {
        if (string.IsNullOrWhiteSpace(realmName))
        {
            ModLogger.LogWarning("You can not remove the default realm!");
            return;
        }

        var realm = _moddedRealms.FirstOrDefault(x => x.RealmName == realmName);

        if (realm == null)
        {
            ModLogger.LogWarning($"Realm '{realmName}' not found!");
            return;
        }
        
        _moddedRealms.Remove(realm);
        
        foreach (var challengeData in realm.ChallengesInformation) 
            _allModdedChallenges.Remove(challengeData);
    }
}