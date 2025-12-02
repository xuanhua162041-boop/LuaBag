using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 自动挂载式  继承mono 单例 
/// 无需挂在 无需动态添加 无需担心切换场景问题
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonAutoMono <T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                //动态创建 动态挂载
                GameObject obj = new GameObject();

                //修改obj的名字  得到t脚本的类名
                obj.name =typeof(T).Name;
                instance = obj.AddComponent<T>();

                //切换场景时不移除对象   即使返回对象 也不会重复存在, 因为这是游戏动态挂载的  而非一开始存在于场景中的
                DontDestroyOnLoad(obj);
                
            }
            return instance;
        }
    }

   
}
