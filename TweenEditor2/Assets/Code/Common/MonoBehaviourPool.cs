using System.Collections.Generic;
using UnityEngine;
using Object=UnityEngine.Object;

/// <summary>
/// 这个缓存池的特点是没有key，但是会修改prefab，只能留下transform组件
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoBehaviourPool<T> where T : MonoBehaviour
{
    protected Transform mRootTran;

    protected T mTemplete;

    protected string mTempleteName;

    protected List<GameObject> mCacheList = new List<GameObject>();

    private List<Component> mComponments = new List<Component>();

    /// <summary>
    /// 模板名字(属性)
    /// </summary>
    protected string TempleteName
    {
        get
        {
            if (string.IsNullOrEmpty(mTempleteName))
            {
                mTempleteName = typeof(T).Name;
            }
            return mTempleteName;
        }
    }

    /// <summary>
    /// 根root(属性)
    /// </summary>
    protected Transform RootTran
    {
        get
        {
            if (mRootTran == null)
            {
                GameObject gameObject = new GameObject($"{TempleteName}_Pool");
                Object.DontDestroyOnLoad(gameObject);
                mRootTran = gameObject.transform;
                mRootTran.parent = null;
            }
            return mRootTran;
        }
    }

    /// <summary>
    /// 模板(属性)
    /// </summary>
    protected T Templete
    {
        get
        {
            if (mTemplete == null && RootTran != null)
            {
                GameObject gameObject = new GameObject($"{TempleteName}_t");
                mTemplete = gameObject.AddComponent<T>();
                Transform transform = gameObject.transform;
                transform.localPosition = Vector3.zero;
                transform.localEulerAngles = Vector3.zero;
                transform.localScale = Vector3.one;
                transform.parent = RootTran;
                gameObject.SetActive(false);
            }
            return mTemplete;
        }
    }
    
    /// <summary>
    /// 从缓存池获取一个gameObject，并且添加T组件
    /// </summary>
    /// <returns></returns>
    public T GetMonoBehaviour()
    {
        T val = null;
        if (mCacheList.Count > 0)
        {
            int index = mCacheList.Count - 1;
            GameObject gameObject = mCacheList[index];
            mCacheList.RemoveAt(index);
            return gameObject.AddComponent<T>();
        }
        if (Templete == null)
        {
            return null;
        }
        T val2 = Object.Instantiate(Templete);
        val = val2.GetComponent<T>();
        if (val == null)
        {
            return null;
        }
        val.gameObject.name = TempleteName;
        return val;
    }

    public void AddToCache(T instance)
    {
        if (instance != null && RootTran != null)
        {
            GameObject gameObject = instance.gameObject;
            //当把对象还回对象池时，为了保证对象时干净的，把对象身上所有的componments都销毁掉
            gameObject.GetComponents(mComponments);
            if (mComponments != null)
            {
                for (int num = mComponments.Count - 1; num >= 0; num--)
                {
                    if (!(mComponments[num] is Transform))
                    {
                        Object.Destroy(mComponments[num]);
                    }
                }
            }
            gameObject.SetActive(false);
            gameObject.name = TempleteName;
            Transform transform = gameObject.transform;
            if (transform != null)
            {
                transform.parent = RootTran;
                transform.localEulerAngles = Vector3.zero;
                transform.localPosition = Vector3.zero;
                transform.localScale = Vector3.one;
            }
            if (!mCacheList.Contains(gameObject))
            {
                mCacheList.Add(gameObject);
            }
        }
    }

    public void Clear()
    {
        if (RootTran != null)
        {
            Object.Destroy(RootTran.gameObject);
            mRootTran = null;
        }
        mTemplete = null;
        if (mCacheList != null)
        {
            mCacheList.Clear();
        }
        else
        {
            mCacheList = new List<GameObject>();
        }
    }
}

