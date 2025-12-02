--常用别名都在此定位
--准备导入的脚本
require("Object")--面向对象
require("SplitTools")--字符串拆分
Json = require("JsonUtility")--json解析

--Unity 相关别名
GameObject = CS.UnityEngine.GameObject
Resources = CS.UnityEngine.Resources
Transform = CS.UnityEngine.Transform
RectTransform = CS.UnityEngine.RectTransform
SpriteAtlas = CS.UnityEngine.SpriteAtlas--图集对象类

Vector3 = CS.UnityEngine.Vector3
Vector2 = CS.UnityEngine.Vector2
TextAsset = CS.UnityEngine.TextAsset

--UI相关
UI = CS.UnityEngine.UI
Image = UI.Image
Text = UI.Text
Button = UI.Button
Toggle = UI.Toggle
ScrollRect = UI.ScrollRect


--自己写的c#相关
ABMgr = CS.ABMgr.Instance--直接的得到ab包资源管理器的对象