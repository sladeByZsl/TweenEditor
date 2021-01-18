using System;
using System.Diagnostics;
using UnityEngine;
using Object = UnityEngine.Object;
public abstract class MonoBehaviourSingle<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;

    public static bool HasInstance => Instance != null;

    public static bool IsAlive = true;

    public static T Instance
    {
        get
        {
            if (!IsAlive)
            {
                UnityEngine.Debug.LogError($"SingletonMonoBehaviour<{typeof(T).Name}> is Released.");
                return null;
            }
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
            }
            if (_instance == null)
            {
                GameObject gameObject = new GameObject();
                _instance = gameObject.AddComponent<T>();
                string name = typeof(T).Name;
                gameObject.name = $"!_{name}";
                if (Application.isEditor)
                {
                    UnityEngine.Debug.Log($"<color=green> Create Instance {gameObject.name} </color>", _instance);
                }
                if (Application.isPlaying)
                {
                    DontDestroyOnLoad(_instance.gameObject);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        OnAwake();
    }

    private void OnDestroy()
    {
        if (IsAlive)
        {
            UnityEngine.Debug.LogError($"{GetType()} is Destory but not call Release()");
            OnRelease();
            IsAlive = false;
        }
    }

    protected abstract void OnAwake();

    protected abstract void OnRelease();

    public static void ReleaseSingle()
    {
        if (_instance != null)
        {
            (_instance as MonoBehaviourSingle<T>).Release();
        }
    }

    public static T GetInstance()
    {
        return Instance;
    }

    public static T Create(Action<T> doAfterInit)
    {
        if (HasInstance)
        {
            UnityEngine.Debug.LogError($"{typeof(T)} is Init before Create");
        }
        if ((bool)Instance)
        {
            doAfterInit(Instance);
        }
        return Instance;
    }

    public void Release()
    {
        OnRelease();
        IsAlive = false;
        if (_instance != null)
        {
            DestroyImmediate(_instance);
            _instance = null;
        }
    }
}

