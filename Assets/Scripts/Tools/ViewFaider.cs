using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
            if (transform.GetChild(i).TryGetComponent(out ITransparencyChanger image))
            {
                Coroutines.StartRoutine(fadeIn(image));
            }
        }
    }

    public void FadeOutAllView(Transform transform)
    {
        StopRunningCoroutines();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out ITransparencyChanger image))
            {
                Coroutines.StartRoutine(fadeOut(image));
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

    IEnumerator fadeOut(ITransparencyChanger image)
    {
        float counter = 0;

        while (counter < _fadeOutDuration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter / _fadeOutDuration);
            image.ChangeTransparency(alpha);
            yield return null;
        }
        _running—oroutines.Remove(fadeOut(image));
    }

    IEnumerator fadeIn(ITransparencyChanger image)
    {
        float counter = 0;

        while (counter < _fadeInDuration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, counter / _fadeInDuration);
            image.ChangeTransparency(alpha);
            yield return null;
        }
        _running—oroutines.Remove(fadeOut(image));
    }
}
