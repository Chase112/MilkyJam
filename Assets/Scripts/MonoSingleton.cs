
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _instance;
    static public T instance
    {
        get
        {
            if (!_instance)
            {
                var questManager = new GameObject(nameof(T));
                _instance = questManager.AddComponent<T>();
            }
            return _instance;
        }
    }

    protected void Awake()
    {
        _instance = this as T;
    }
}