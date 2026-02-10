using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;

    // 全局访问点
    public static T Instance
    {
        get
        {
            Time.timeScale = 1;
            if (_instance == null)
            {
                // 尝试从场景中查找
                _instance = FindObjectOfType<T>();

                // 如果找不到，自动创建一个
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();
                   DontDestroyOnLoad(singletonObject);
                    Debug.Log($"[Singleton] Auto-created {typeof(T).Name}");
                }
                else
                {
                    // 找到了就确保不被销毁
                   DontDestroyOnLoad(_instance.gameObject);
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        // 防止重复实例
        if (_instance == null)
        {
            _instance = this as T;
           // DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 已存在实例，销毁自己
            Destroy(gameObject);
        }
    }
}
