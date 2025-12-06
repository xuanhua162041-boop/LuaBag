using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

/// <summary>
/// 1.ab包相关api
/// 2.单例
/// 3.委托 lambda 表达式
/// 4.携程
/// 5.字典
/// </summary>
public class ABMgr : SingletonAutoMono<ABMgr>
{
    //AB包 管理器目的是 让外部 更方便的 进行资源加载

    //主包
    private AssetBundle mainAB = null;
    //依赖包 获取用的配置文件
    private AssetBundleManifest manifest = null;

    //ab包 不能重复加载  所以需要容器  采用字典 键值对
    private Dictionary<string, AssetBundle>abDic = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// AB包存放路径
    /// </summary>
    private string PathUrl
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }

    /// <summary>
    /// 主包名称
    /// </summary>
    private string MainABName
    {
        get
        {
#if UNITY_IOS
            return "iOS";
#elif UNITY_ANDROID
            return "Android";
#else 
            return "PC";
#endif
        }
    }

    /// <summary>
    /// 加载ab包
    /// </summary>
    /// <param name="abName"></param>
    public void LoadAB(string abName)
    {
        //加载主包
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }

        //根据主包的 manifes 获取依赖包信息
        AssetBundle ab = null;
        string[] strs = manifest.GetAllDependencies(abName);//获取依赖包名称数组
        //遍历 并加载 依赖包
        for (int i = 0; i < strs.Length; i++)
        {
            //判断包 是否已经加载
            if (!abDic.ContainsKey(strs[i]))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);//加载依赖包
                abDic.Add(strs[i], ab);//存储到 已加载的ab包 字典中
            }
        }
        //加载目标包
        if (!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
            abDic.Add(abName, ab);

        }
    }

    //同步加载 不指定类型
    public Object LoadRes(string abName, string resName)
    { 
        //加载ab包
        LoadAB(abName);
        //加载资源（resname为完整路径）可以理解为 从压缩包 取出相应文件
        //为了外部调用方便， 在加载资源时判断 资源是否为gameobject 如果是直接实例化 并返回给外界
        //return abDic[abName].LoadAsset(resName);
        Object obj = abDic[abName].LoadAsset(resName);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;


    }

    //同步加载 指定类型 typeof   
    public Object LoadRes(string abName,string resName,Type type)
    {
        LoadAB(abName);
        Object obj = abDic[abName].LoadAsset(resName,type);//避免 相同名称 不同类型资源加载错误
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }

    //同步加载 根据泛型指定类型
    public T LoadRes<T>(string abName,string resName) where T: Object
    {
        LoadAB(abName);
        T obj = abDic[abName].LoadAsset<T>(resName);//避免 相同名称 不同类型资源加载错误

        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;

    }


    //异步加载
    //这里并未 让ab包异步加载 只是让资源异步加载 
    //根据名字异步加载资源
    public void LoadResAsync(string abName,string resName,UnityAction<Object> callBack)//提供给外部使用
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName,callBack));
    }
    private IEnumerator ReallyLoadResAsync(string abName, string resName, UnityAction<Object> callBack)//真正的协程方法
    {
        LoadAB(abName);
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName);//避免 相同名称 不同类型资源加载错误
        yield return abr;//等待加载完成
        //异步加载结束后 通过委托传递给外部使用
        if (abr.asset is GameObject)
            callBack( Instantiate(abr.asset));
        else
            callBack(abr.asset);
    }

    //根据Type异步加载资源
    public void LoadResAsync(string abName, string resName,Type type, UnityAction<Object> callBack)//提供给外部使用
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName,type, callBack));
    }
    private IEnumerator ReallyLoadResAsync(string abName, string resName, Type type, UnityAction<Object> callBack)//真正的协程方法
    {
        LoadAB(abName);
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName,type);//避免 相同名称 不同类型资源加载错误
        yield return abr;//等待加载完成
        //异步加载结束后 通过委托传递给外部使用
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset));
        else
            callBack(abr.asset);
    }

    //根据泛型异步加载资源
    public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callBack) where T : Object//提供给外部使用
    {
        StartCoroutine(ReallyLoadResAsync<T>(abName, resName, callBack));
    }
    private IEnumerator ReallyLoadResAsync<T>(string abName, string resName, UnityAction<T> callBack) where T : Object//真正的协程方法
    {
        LoadAB(abName);
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);//避免 相同名称 不同类型资源加载错误
        yield return abr;//等待加载完成
        //异步加载结束后 通过委托传递给外部使用
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset)as T);
        else
            callBack(abr.asset as T);
    }




    //单个包卸载
    public void UnLoad(string abName)
    {
        if (abDic.ContainsKey(abName))
        {
            abDic[abName].Unload(false);
            abDic.Remove(abName);
        }
    }


    //所有包加载
    public void ClearAB()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB = null;
        manifest = null;
    }


}
