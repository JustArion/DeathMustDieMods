namespace Dawn.DMD.StarCruxExpansion.UI;

using UnityEngine;

public class StateMonitor : MonoBehaviour
{
    public event Action Enable;
    public event Action Disable;
    private void OnEnable() => Enable?.Invoke();
    private void OnDisable() => Disable?.Invoke();
}