// #define ALWAYS_SHOW_AFFORDANCES
namespace Dawn.DMD.StarCruxExpansion.UI;

using Claw.Core;
using Death.TimesRealm;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AffordanceHandler : MonoBehaviour
{
    private GameObject _realmAffordance;
    
    private bool _initialized;
    private string _id;

    public void SetData(string identifier)
    {
        if (_initialized)
            return;
        _id = "SCE_" + identifier;
    
        PrepareIndiactor();
        
        _initialized = true;
        
        ShowAffordance();
    }

    private void PrepareIndiactor()
    {
        _realmAffordance = new GameObject("Affordance", typeof(Image), typeof(AffordanceAnimator));
        var realmTransform = _realmAffordance.transform;
        
        realmTransform.SetParent(transform, false);
        realmTransform.localPosition = new Vector2(30, 45);
    }

    public void ShowAffordance()
    {
        if (!_initialized)
        {
            ModLogger.LogWarning("Affordance Data has not been set!");
            return;
        }

        var show = SingletonBehaviour<Facade_Lobby>.Instance.ShouldNewIndicatorAppear(_id);

        if (show) 
            ModLogger.LogDebug($"{nameof(AffordanceHandler)}::{nameof(ShowAffordance)} Id: {_id} was shown.");
        
        #if ALWAYS_SHOW_AFFORDANCES
        _realmAffordance.SetActive(true);
        #else
        _realmAffordance.SetActive(show);
        #endif
    }
    
    #if ALWAYS_SHOW_AFFORDANCES
    private bool _dismissed;
    #endif

    public void DismissAffordance()
    {
        if (!_initialized)
        {
            ModLogger.LogWarning("Affordance Data has not been set!");
            return;
        }

        if (!gameObject.activeSelf)
            return;

        _realmAffordance.SetActive(false);
        #if ALWAYS_SHOW_AFFORDANCES
        if (_dismissed)
            return;

        ModLogger.LogDebug($"{nameof(AffordanceHandler)}::{nameof(DismissAffordance)} Id: {_id} was \"dismissed\".");
        _dismissed = true;
        #else
        if (!SingletonBehaviour<Facade_Lobby>.Instance.ShouldNewIndicatorAppear(_id)) 
            return;
        SingletonBehaviour<Facade_Lobby>.Instance.MarkNewIndicatorSeen(_id);
        
        ModLogger.LogDebug($"{nameof(AffordanceHandler)}::{nameof(DismissAffordance)} Id: {_id} was dismissed.");
        #endif

    }

    private void OnDestroy() => Destroy(_realmAffordance);
}