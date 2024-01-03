namespace Dawn.DMD.StarCruxExpansion.UI;

using Claw.Core;
using Death.TimesRealm;
using Death.TimesRealm.Behaviours;
using Reflection;
using UnityEngine;

public class AffordanceHandler : MonoBehaviour
{
    private static GameObject _newIndicatorFab;

    private static GameObject _indicatorFab => _newIndicatorFab ??= FindObjectsOfType<GameObject>().FirstOrDefault(x => x.name == "NpcTalk_Fab");

    private GameObject _newIndicator;
    
    private bool _initialized;
    private string _id;

    public void SetData(string identifier, out Action markSeenCallback)
    {
        markSeenCallback = null;
        
        if (_initialized)
            return;
        _id = "sce_" + identifier;
        
        _newIndicator = Instantiate(_indicatorFab, transform);

        markSeenCallback = DismissAffordance;
        
        _initialized = true;
        
        ShowAffordance();
    }
    
    public void ShowAffordance()
    {
        if (!_initialized)
        {
            Plugin.Logger.LogWarning("Affordance Data has not been set!");
            return;
        }

        var show = SingletonBehaviour<Facade_Lobby>.Instance.ShouldNewIndicatorAppear(_id);

        if (show) 
            Plugin.Logger.LogDebug($"{nameof(AffordanceHandler)}::{nameof(ShowAffordance)} Id: {_id} was shown.");
        
        _newIndicator.SetActive(show);
    }

    public void DismissAffordance()
    {
        if (!_initialized)
        {
            Plugin.Logger.LogWarning("Affordance Data has not been set!");
            return;
        }

        if (!gameObject.activeSelf)
            return;

        // SingletonBehaviour<Facade_Lobby>.Instance.MarkNewIndicatorSeen(_id);
        _newIndicator.SetActive(false);
        
        Plugin.Logger.LogDebug($"{nameof(AffordanceHandler)}::{nameof(DismissAffordance)} Id: {_id} was dismissed.");
    }
}