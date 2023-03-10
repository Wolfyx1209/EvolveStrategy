using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ViewFaider
{
    private float _fadeInDuration = 0.5f;
    private float _fadeOutDuration = 0.5f;

    List<IEnumerator> _running—oroutines = new();
    public void FadeInAllView(Transform transform)
    {
        StopRunningCoroutines();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Image image))
            {
                Coroutines.StartRoutine(fadeIn(image));
            }
            if (transform.GetChild(i).TryGetComponent(out TextMeshProUGUI text))
            {
                Coroutines.StartRoutine(fadeIn(text));
            }
        }
    }
    public void FadeOutAllView(Transform transform)
    {
        StopRunningCoroutines();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Image image))
            {
                Coroutines.StartRoutine(fadeOut(image));
            }
            if (transform.GetChild(i).TryGetComponent(out TextMeshProUGUI text))
            {
                Coroutines.StartRoutine(fadeOut(text));
            }
        }
    }

    private void StopRunningCoroutines()
    {
        foreach (IEnumerator coroutine in _running—oroutines)
        {
            Coroutines.StopRoutine(coroutine);
        }
    }

    IEnumerator fadeOut(Image image)
    {
        float counter = 0;

        while (counter < _fadeOutDuration)
        {
            Color spriteColor = image.material.color;
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter / _fadeOutDuration);

            image.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            yield return null;
        }
        _running—oroutines.Remove(fadeOut(image));
    }

    IEnumerator fadeIn(Image image)
    {
        float counter = 0;

        while (counter < _fadeInDuration)
        {
            Color spriteColor = image.material.color;
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, counter / _fadeInDuration);

            image.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            yield return null;
        }
        _running—oroutines.Remove(fadeOut(image));
    }

    IEnumerator fadeOut(TextMeshProUGUI text)
    {
        float counter = 0;

        while (counter < _fadeOutDuration)
        {
            Color spriteColor = text.faceColor;
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter / _fadeOutDuration);

            text.faceColor = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            yield return null;
        }
        _running—oroutines.Remove(fadeOut(text));
    }

    IEnumerator fadeIn(TextMeshProUGUI text)
    {
        float counter = 0;

        while (counter < _fadeInDuration)
        {
            Color spriteColor = text.faceColor;
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, counter / _fadeInDuration);

            text.faceColor = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            yield return null;
        }
        _running—oroutines.Remove(fadeOut(text));
    }
}
