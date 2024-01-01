namespace Dawn.DMD.StarCruxExpansion.Helpers;

using UnityEngine;

internal static class TransformEx
{
    internal static Transform Find(this Transform transform, params string[] paths) => transform.Find(string.Join("/", paths));
}