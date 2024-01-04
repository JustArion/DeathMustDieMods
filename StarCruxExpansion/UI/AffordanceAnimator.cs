namespace Dawn.DMD.StarCruxExpansion.UI;

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AffordanceAnimator : MonoBehaviour
{
    public Image Image { get; private set; }
    public static Sprite[] Sprites { get; private set; }
    public int FramesPerSecond { get; set; }
    private void Start()
    {
        Image = GetComponent<Image>();
        Sprites ??= Resources.FindObjectsOfTypeAll<Sprite>()
            .Where(x => x.name.StartsWith("NpcTalk_Spr_"))
                .OrderBy(x => x.name)
                    .ToArray();

        FramesPerSecond = 7;

        StartCoroutine(AnimateImage());
    }
    
    private IEnumerator AnimateImage()
    {
        ModLogger.LogDebug("Starting AffordanceAnimator Coroutine");
        while (true)
        {
            foreach (var sprite in Sprites)
            {
                Image.sprite = sprite;
                yield return new WaitForSecondsRealtime(1f / FramesPerSecond);
            }
        }
    }
}