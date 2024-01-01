namespace Dawn.DMD.StarCruxExpansion.UI;

using UnityEngine;

public class AffordanceHandler : MonoBehaviour
{
    private bool _initialized;
    private AffordanceOptions _options;

    public void SetData(AffordanceOptions options)
    {
        if (_initialized)
            return;
        _options = options;
        _initialized = true;
    }
    
    public void ShowAffordance()
    {
        if (!_initialized)
        {
            Plugin.Logger.LogWarning("Affordance Data has not been set!");
            return;
        }
    }

    public void DismissAffordance()
    {
        if (!_initialized)
        {
            Plugin.Logger.LogWarning("Affordance Data has not been set!");
            return;
        }
    }

    private void OnDestroy()
    {
        throw new NotImplementedException();
    }
}