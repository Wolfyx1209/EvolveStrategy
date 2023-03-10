using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewFaider
{
    public void FadeIn(Image image, float duration)
    {
        Coroutines.StartRoutine(fadeIn(image, duration));
    }
    public void FadeOut(Image image, float duration)
    {
        Coroutines.StartRoutine(fadeOut(image, duration));
    }

    public void FadeIn(TextMeshProUGUI text, float duration)
    {
        Coroutines.StartRoutine(fadeIn(text, duration));
    }
    public void FadeOut(TextMeshProUGUI text, float duration)
    {
        Coroutines.StartRoutine(fadeOut(text, duration));
    }

    IEnumerator fadeOut(Image MyRenderer, float duration)
    {
        float counter = 0;
        Color spriteColor = MyRenderer.material.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter / duration);

            MyRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            yield return null;
        }
    }

    IEnumerator fadeIn(Image MyRenderer, float duration)
    {
        float counter = 0;
        Color spriteColor = MyRenderer.material.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, counter / duration);

            MyRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            yield return null;
        }
    }

    IEnumerator fadeOut(TextMeshProUGUI MyRenderer, float duration)
    {
        float counter = 0;
        Color spriteColor = MyRenderer.faceColor;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter / duration);

            MyRenderer.faceColor = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            yield return null;
        }
    }

    IEnumerator fadeIn(TextMeshProUGUI MyRenderer, float duration)
    {
        float counter = 0;
        Color spriteColor = MyRenderer.faceColor;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, counter / duration);

            MyRenderer.faceColor = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            yield return null;
        }
    }
}
