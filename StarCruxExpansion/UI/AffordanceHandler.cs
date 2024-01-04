namespace Dawn.DMD.StarCruxExpansion.UI;

using System.Diagnostics;
using Claw.Core;
using Claw.Core.Utils;
using Death.TimesRealm;
using Death.TimesRealm.Behaviours;
using Reflection;
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
        _id = "sce_" + identifier;
    
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
        
        _realmAffordance.SetActive(show);
    }

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
        #if !DEBUG
        SingletonBehaviour<Facade_Lobby>.Instance.MarkNewIndicatorSeen(_id);
        Plugin.Logger.LogDebug($"{nameof(AffordanceHandler)}::{nameof(DismissAffordance)} Id: {_id} was dismissed.");
        #else
        ModLogger.LogDebug($"{nameof(AffordanceHandler)}::{nameof(DismissAffordance)} Id: {_id} was \"dismissed\".");
        #endif

    }

    private void OnDestroy() => Destroy(_realmAffordance);
}