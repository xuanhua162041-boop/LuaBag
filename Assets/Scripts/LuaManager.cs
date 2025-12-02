using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

/// <summary>
/// lua管理器  
/// 提供解析器
/// 保证解析器的唯一性
/// </summary>
public class LuaManager:BaseManager<LuaManager>
{
    private LuaManager()
    {

    }

    private LuaEnv luaEnv;

    /// <summary>
    /// 得到lua中的_G
    /// </summary>
    public LuaTable Global
    {
        get
        {
            return luaEnv.Global;
        }
    }

    /// <summary>
    /// 初始化解析器
    /// </summary>
    public void Init()
    {
        if (luaEnv != null)
            return;
        //初始化
        luaEnv = new LuaEnv();

        //加载lua重定向   执行require时  会依次调用这些方法  直到找到脚本 或者执行完也没找到(使用默认的加载方式)  
        luaEnv.AddLoader(MySustomLoader);
        luaEnv.AddLoader(MyCustomABLoader);
        Debug.Log("-LuaManager 初始化完成");
    }
    /// <summary>
    /// 自动执行 自定义loader 通过函数中的逻辑 去加载lua文件
    /// </summary>
    /// <param name="filePath">require执行的 lua文件名</param>
    /// <returns></returns>
    private byte[] MySustomLoader(ref string filePath)//这个自定义loader  自己转到AddLoader 的定义中去看  
    {
        //拼接一个lua文件所在路径
        string path = Application.dataPath + "/Lua/" + filePath + ".Lua";

        Debug.Log(filePath);

        //有路径  就去加载文件
        if (File.Exists(path))//判断文件是否存在
        {
            return File.ReadAllBytes(path);//根据路径把 内容加载成字节数组的形式

        }
        else
        {
            Debug.Log("重定向失败,文件名为:" + filePath);
        }

        return null;
    }

    /// <summary>
    /// 开发时不使用
    /// Lua脚本 最终放到AB包
    /// 最终 会通过 加载AB包 再加载其中的Lua脚本资源 来执行它
    /// 重定向加载AB包中的Lua脚本
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private byte[]MyCustomABLoader(ref string filePath)
    {
        //Debug.Log("进入AB包加载重定向函数~");
        ////从AB包中加载文件
        ////加载AB包
        //string path = Application.streamingAssetsPath + "/lua";//加载AB包的路径
        //AssetBundle ab = AssetBundle.LoadFromFile(path);
        ////加载Lua文件 //TextAsset 将项目中的原始文本文件用作资源，通过此类获取其 内容。
        //TextAsset tx = ab.LoadAsset<TextAsset>(filePath+".lua");//传入文件名  .lua也是文件名 别忘了 他的真实格式是文本txt
        ////返回 byte数组
        //return tx.bytes;

        //加载lua文件 使用同步加载   因为重定向 需要立刻返回
        //通过AB包管理器 加载的lua脚本资源
        TextAsset lua = ABMgr.Instance.LoadRes<TextAsset>("lua", filePath + ".lua");
        if (lua != null)
            return lua.bytes;//返回 byte数组
        else
            Debug.Log("MyCustomABLoader 重定向失败,文件名为:" + filePath);
        return null;
    }

    /// <summary>
    /// 传入lua脚本名  调用DoString
    /// </summary>
    /// <param name="fileName"></param>
    public void DoLuaFile(string fileName)
    {
        string str = string.Format("require('{0}')",fileName);
        DoString(str);
    }


    /// <summary>
    /// 执行lua语言
    /// </summary>
    /// <param name="str"></param>
    public void DoString(string str)
    {
        if(luaEnv == null)
        {
            Debug.Log("LuaManager解析器未初始化");
            return;
        }
        luaEnv.DoString(str);
    }

    /// <summary>
    /// 释放lua垃圾
    /// </summary>
    public void Tick() 
    { 
        luaEnv.Tick();
    }

    /// <summary>
    /// 销毁解析器
    /// </summary>
    public void Dispose()
    {
        luaEnv.Dispose();
        luaEnv = null;
    }
}
