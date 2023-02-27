using UnityEngine;

public class DontDestroyOnLoadSingletone<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameObject = new GameObject('[' + typeof(T).Name + ']');
                _instance = gameObject.AddComponent<T>();
                DontDestroyOnLoad(gameObject);
            }
            return _instance;
        }
    }

    public virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
