using System.Collections;
using UnityEngine;

public sealed class Coroutines : MonoBehaviour
{
    private static Coroutines instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject gameObject = new GameObject("Corutine Manager");
                m_instance = gameObject.AddComponent<Coroutines>();
                DontDestroyOnLoad(gameObject);
            }
            return m_instance;
        }
    }
    private static Coroutines m_instance;
    public static Coroutine StartRoutine(IEnumerator corutine)
    {
        return instance.StartCoroutine(corutine);
    }
    public static void StopRoutine(IEnumerator corutine)
    {
        instance.StopCoroutine(corutine);
    }

}
