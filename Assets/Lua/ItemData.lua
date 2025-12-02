--首先应该先把json 表 从ab包中加载出来
--加载的json文件textasset对象
local txt = ABMgr:LoadRes('Json','ItemData',typeof(TextAsset))
--获取他的文本信息进行json解析
print(txt.text)
local itemList = Json.decode(txt.text)
