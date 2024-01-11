namespace Dawn.DMD.StarCruxExpansion.Realms.ModdedGlobalEffects;

using Death.Darkness;
using RealmHelpers.GameDurationChangeHandler;

public class GlobalEffect_GameDurationChange(int durationDeltaMinutes) : IGlobalEffect
{
    public void Enable()
    {
        _isEnabled = true;
        Update();
    }

    public void Disable()
    {
        _isEnabled = false;
        Update();
    }

    public void Update() => SetActive(_isEnabled);

    private void OnBecomeActive() => GameDurationHandler.GameDurationDeltaInMinutes += durationDeltaMinutes;


    private void OnBecomeInactive() => GameDurationHandler.GameDurationDeltaInMinutes -= durationDeltaMinutes;

    public string Code { get; set; }

    private void SetActive(bool value)
    {
        if (IsActive == value)
            return;
        
        IsActive = value;
        
        if (IsActive) 
            OnBecomeActive();
        else 
            OnBecomeInactive();
    }

    private bool _isEnabled;

    public bool IsActive { get; private set; }
}