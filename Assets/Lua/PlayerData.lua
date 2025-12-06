PlayerData = {}--用于存放玩家数据的全局表

PlayerData.equips = {}--定义了三个表变量
PlayerData.items = {}
PlayerData.gems = {}

--为玩家数据写了一个初始化方法   更改此处即可
function PlayerData:Init()
    --道具信息 不管存本地 还是服务器 都不会吧所有的到九月信息存入  只会存id和数量
    table.insert(self.equips,{id = 1 , num = 1})
    table.insert(self.equips,{id = 2 , num = 1})

    table.insert(self.items,{id = 3 , num = 35})
    table.insert(self.items,{id = 4 , num = 20})

    table.insert(self.gems,{id = 5 , num = 52})
    table.insert(self.gems,{id = 6 , num = 64})
end

--PlayerData:Init()

