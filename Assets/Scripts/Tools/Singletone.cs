using UnityEngine;

public class Singletone<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T instance {
        get
        {
            if (_instance == null)
            {
                GameObject gameObject = new GameObject('[' + typeof(T).Name + ']');
                _instance = gameObject.AddComponent<T>();
            }
            return _instance;
        }
    }
}
