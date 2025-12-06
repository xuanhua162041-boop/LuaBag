PlayerData = {}
--我们目前 制作背包 所以只需要他的道具信息即可

PlayerData.equips = {}
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


print(PlayerData.equips[1].id..PlayerData.equips[1].num)