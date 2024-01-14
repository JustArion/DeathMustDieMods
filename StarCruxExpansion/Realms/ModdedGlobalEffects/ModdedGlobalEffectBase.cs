namespace Dawn.DMD.StarCruxExpansion.Realms.ModdedGlobalEffects;

using Death.Darkness;

public abstract class ModdedGlobalEffectBase : IGlobalEffect
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

    protected abstract void OnBecomeActive();


    protected abstract void OnBecomeInactive();

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