---@meta

-- ===============================================
-- xLua 核心与全局定义
-- ===============================================

-- 声明 CS 全局变量 (C# 命名空间根目录)
---@class CS
CS = {}

-- 声明 xLua 注入的全局函数 typeof
---@param type any C# 类型，如 CS.UnityEngine.GameObject
---@return table
function typeof(type) end

-- ===============================================
-- 声明 Unity C# 命名空间
-- ===============================================

---@class UnityEngine : table
CS.UnityEngine = {}

---@class UnityEngine.UI : table
CS.UnityEngine.UI = {}

-- ===============================================
-- 声明 InitClass.lua 中使用的 C# 类别名
-- ===============================================

-- Unity 常用类型 (CS.UnityEngine.XXX)
---@class UnityEngine.GameObject : table
---@class UnityEngine.Resources : table
---@class UnityEngine.Transform : table
---@class UnityEngine.RectTransform : table
---@class UnityEngine.SpriteAtlas : table
---@class UnityEngine.Vector3 : table
---@class UnityEngine.Vector2 : table
---@class UnityEngine.TextAsset : table

-- UI 常用类型 (CS.UnityEngine.UI.XXX)
---@class UnityEngine.UI.Image : table
---@class UnityEngine.UI.Text : table
---@class UnityEngine.UI.Button : table
---@class UnityEngine.UI.Toggle : table
---@class UnityEngine.UI.ScrollRect : table

-- ===============================================
-- 声明您自定义的 C# 类 (CS.ABMgr)
-- ===============================================

-- 1. 声明 ABMgr 命名空间/类
---@class ABMgr : table
CS.ABMgr = {}

-- 2. 声明 ABMgr.Instance 静态属性的类型
-- 这样编辑器就知道 ABMgr.Instance 也是 ABMgr 类型
---@field Instance ABMgr
CS.ABMgr.Instance = nil