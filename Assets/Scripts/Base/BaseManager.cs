using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 单例模式基类 不继承mono
/// </summary>
/// <typeparam name="T">自定义类</typeparam>
public abstract class BaseManager<T> where T : class//意为只支持引用类型
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                //利用反射来得到无参私有的构造函数 用来对象的实例化
                Type type  = typeof(T);
                ConstructorInfo info = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, 
                    null,
                    Type.EmptyTypes, 
                    null);     
                if (info != null)
                {
                    instance = info.Invoke(null) as T;
                }
                else
                {
                    Debug.LogError("没有到对应的无参构造函数,脚本名:" + typeof(T).Name);
                }
            }
            return instance;
        }
    }

}
