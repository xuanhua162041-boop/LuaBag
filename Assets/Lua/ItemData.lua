--首先应该先把json 表 从ab包中加载出来
--加载的json文件textasset对象
local txt = ABMgr:LoadRes('Json','ItemData',typeof(TextAsset))
--获取他的文本信息进行json解析
print(txt.text)
local itemList = Json.decode(txt.text)--对从 ab包 中 获取的json 格式的 textasset文件  进行解析
print("[ID]"..itemList[1].id .."[NAME]".. itemList[1].name .."[TIPS]".. itemList[1].tips)

--加载出来是 数组的形式table 不方便查看...
--而且键 无法自定义,为默认的1,2,3,4,5,6
--  使用一个新表来存储
--而且这张新表能在任何地方使用
--一张用来存储道具信息的表
--键值对形式  键是道具id 值是道具表一行信息
ItemData = {}
for _, value in pairs(itemList) do
    ItemData[value.id] = value-- 物品id 为键 值是value(数组内容)
end

for key, value in ipairs(ItemData) do
    print(key,value)
end

print(ItemData[1].tips)


