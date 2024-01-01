namespace Dawn.DMD.StarCruxExpansion.Helpers;

using UnityEngine;

internal static class MonoBehaviourEx
{
    internal static T AddComponent<T>(this MonoBehaviour behaviour) where T : MonoBehaviour => behaviour.gameObject.AddComponent<T>();
}