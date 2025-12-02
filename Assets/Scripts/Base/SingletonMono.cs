using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂载式继承Mono的单例基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    protected virtual void Awake()//awake 时 进行赋值instance    子类可重写
    {
        instance = this as T;
    }
}
