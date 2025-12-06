print( '准备就绪哇' )
--初始化 所有准备好的类别名  之后的逻辑可以直接使用
require("InitClass")
--初始化道具表信息
require("ItemData")
--获取玩家信息
--1.从本地获取   本地存储有好几种的方式 playerPrefs  或者json 或 二进制
--2.从网络游戏 服务器 获取
require("ItemData")

require("PlayerData")
PlayerData:Init()

