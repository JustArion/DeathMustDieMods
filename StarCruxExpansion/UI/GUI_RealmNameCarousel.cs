namespace Dawn.DMD.StarCruxExpansion.UI;

using System.Collections.Generic;
using Harmony;
using Helpers;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class GUI_RealmNameCarousel : MonoBehaviour
{
    private static readonly Vector3 HalfScale = new(0.5f, 0.5f, 0.5f);
    private LocalizeStringEvent _Text_Name;
    private readonly List<(string RealmName, List<Action<string>> NavigationCallbacks)> RealmNames = [];
    private int _index;
    private bool _debug;
    public Button NextRealmButton;
    public Button PreviousRealmButton;
    
    private void Awake()
    {
        _Text_Name = transform.Find(UI_Constants.GAMEOBJECT_REALM_NAME_TEXT).GetComponent<LocalizeStringEvent>();

        var go_nextRealm = SetRight(CreateRealmCarouselButton("NextRealm", gameObject));
        var go_previousRealm = SetLeft(CreateRealmCarouselButton("PreviousRealm", gameObject));
        
        NextRealmButton = go_nextRealm.GetComponent<Button>();
        PreviousRealmButton = go_previousRealm.GetComponent<Button>();

        AddRealmName(string.Empty, ResetToDefaultRealm);

        NextRealmButton.onClick.AddListener(Internal_NavigateNextRealm);
        PreviousRealmButton.onClick.AddListener(Internal_NavigatePrevious);
    }

    private void OnDestroy()
    {
        ResetToDefaultRealm();
        Destroy(NextRealmButton.gameObject);
        Destroy(PreviousRealmButton.gameObject);
    }

    private void Internal_NavigateNextRealm() => NavigateNextRealm();
    private void Internal_NavigatePrevious() => NavigatePreviousRealm();
    public bool NavigateNextRealm()
    {
        var previousIndex = _index;
        IncrementIndex();

        if (previousIndex == _index)
        {
            Plugin.Logger.LogDebug("Did not navigate to next realm, since there is none.");
            return false; // Did not increment, there's only 1 realm page.
        }
        
        var (realmName, navigationCallbacks) = RealmNames[_index];


        if (_debug) 
            Plugin.Logger.LogDebug($"On '{nameof(NavigateNextRealm)}' -> Realm Name: {(string.IsNullOrWhiteSpace(realmName) ? "The Outer Circle" : realmName)}");

        SetText(realmName);
        navigationCallbacks.ForEach(x => ExceptionWrappers.Wrap(() => x(realmName), Plugin.Logger.LogError));
        return true;
    }

    public bool NavigatePreviousRealm()
    {
        var previousIndex = _index;
        DecrementIndex();

        if (previousIndex == _index)
        {
            Plugin.Logger.LogDebug("Did not navigate to previous realm, since there is none.");
            return false; // Did not increment, there's only 1 realm page.
        }
        
        var (realmName, navigationCallbacks) = RealmNames[_index];


        if (_debug)
            Plugin.Logger.LogDebug($"On '{nameof(NavigatePreviousRealm)}' -> Realm Name: {realmName}");
        
        SetText(realmName);
        navigationCallbacks.ForEach(x => ExceptionWrappers.Wrap(() => x(realmName), Plugin.Logger.LogError));
        return true;
    }

    private void SetText(string text) => _Text_Name.OnUpdateString.Invoke(text);

    public void ResetToDefaultRealm(string _ = null)
    {
        _Text_Name.StringReference.RefreshString();
        _index = 0;
    }

    public void SubscribeToDefaultRealm(Action navigationCallback)
    {
        var defaultRealm = RealmNames.FirstOrDefault();

        if (defaultRealm == default)
        {
            Plugin.Logger.LogWarning("Unable to subscribe to default realm, it does not exist.");
            return;
        }
        
        defaultRealm.NavigationCallbacks.Add(_ => navigationCallback());
    }
    
    
    public void AddRealmName(string realmName, Action<string> navigationCallback)
    {
        if (realmName == null || navigationCallback == null)
        {
            Plugin.Logger.LogWarning("Unable to add Realm Name, either the RealmName is null or the Callback action is.");
            return;
        }

        var priorRegistration = RealmNames.FirstOrDefault(x => x.RealmName == realmName);

        if (priorRegistration != default) 
            priorRegistration.NavigationCallbacks.Add(navigationCallback);
        else
            RealmNames.Add((realmName, [ navigationCallback ]));
        
    }

    public void RemoveRealmName(string realmName)
    {
        if (realmName == string.Empty)
        {
            Plugin.Logger.LogWarning("The default realm should not be removed!");
            return;
        }

        RealmNames.Remove(RealmNames.First(x => x.RealmName == realmName));
    }

    public void EnableDebugMode()
    {
        _debug = true;
        NextRealmButton.onClick.AddListener(() => Plugin.Logger.LogDebug($"{nameof(NextRealmButton)} clicked"));
        NextRealmButton.GetComponent<Image>().color = Color.white;
        
        PreviousRealmButton.onClick.AddListener(() => Plugin.Logger.LogDebug($"{nameof(PreviousRealmButton)} clicked"));
        PreviousRealmButton.GetComponent<Image>().color = Color.white;
    }
    
    private static GameObject CreateRealmCarouselButton(string gameObject, GameObject parent)
    {
        var go = new GameObject(gameObject);
        var img = go.AddComponent<Image>();
        img.color = Color.clear;
        go.AddComponent<Button>();
        go.transform.SetParent(parent.transform, false);
        go.transform.localScale = HalfScale;
        return go;
    }

    private static GameObject SetLeft(GameObject button)
    {
        button.transform.localPosition = new(-235f, 0, 0);
        return button;
    }

    private static GameObject SetRight(GameObject button)
    {
        button.transform.localPosition = new(235f, 0, 0);
        return button;
    }

    private void IncrementIndex() => _index = (_index + 1) % RealmNames.Count;
    private void DecrementIndex() => _index = (_index > 0) ? _index - 1 : RealmNames.Count - 1;
}