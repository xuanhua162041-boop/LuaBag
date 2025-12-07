--一个面板对应一个表
BagPanel = {}

--"成员变量"
--面板对象
BagPanel.panelObj =nil
--各个控件
BagPanel.btnClose =nil
BagPanel.togEquip =nil
BagPanel.togItem =nil
BagPanel.togGem =nil
BagPanel.SvBag =nil
BagPanel.Content=nil

--用来存储当前显示的格子
BagPanel.items = nil
--"成员方法"
--初始化方法
function BagPanel:Init()
    if self.panelObj == nil then
    --1.实例化面板对象
        self.panelObj = ABMgr:LoadRes("ui","BagPanel",typeof(GameObject))
        self.panelObj.transform:SetParent(Canvas,false)
    --2.查找控件
        --关闭按钮
        self.btnClose = self.panelObj.transform:Find('ButtonClose'):GetComponent(typeof(Button))
        --toggle组件
        local group = self.panelObj.transform:Find('Buttons')
        self.togEquip = group:Find('Equip'):GetComponent(typeof(Toggle))
        self.togItem = group:Find('Item'):GetComponent(typeof(Toggle))
        self.togGem = group:Find('Gem'):GetComponent(typeof(Toggle))
        --sv相关
        self.SvBag = self.panelObj.transform:Find('SvBag'):GetComponent(typeof(ScrollRect))
        self.Content = self.SvBag.transform:Find('Viewport'):Find('Content')
    --3.加事件
        --关闭按钮事件
        self.btnClose.onClick:AddListener(function()
            self:HideMe()
        end)
        --单选框事件
        --切页签
        --toggle 对应委托时候 是 unityaction<bool>
        self.togEquip.onValueChanged:AddListener(function( value )
            if value == true then
                self:ChangeType(1)
            end
        end)
        self.togItem.onValueChanged:AddListener(function( value )
            if value == true then
                self:ChangeType(2)
            end
        end)
        self.togGem.onValueChanged:AddListener(function( value )
            if value == true then
                self:ChangeType(3)
            end
        end)

    
        
        

        
    end
end

--显示\隐藏
function BagPanel:ShowMe()
    self:Init()--showme作为初始化入口
    self.panelObj:SetActive(true)
end
function BagPanel:HideMe()
    self.panelObj:SetActive(false)
end

--逻辑处理函数 用来切换页签的
--type 1装备 2道具 3宝石
function BagPanel:ChangeType(type)
    print("当前类型为:"..type)
    --切页  根据玩家信息 来进行格子创建  


    --更新之前把老的格子删掉 BagPanel.items


    --再根据当前选择的类型  来创建新的格子 BagPanel.items
    --要根据传入的type123来选择 显示的数据
    local nowItems = {}--玩家背包数据临时变量
    if type == 1 then--根据  toggle 来 存储 队形的背包数据
        nowItems = PlayerData.equips
    elseif type == 2 then
        nowItems = PlayerData.items
    else
        nowItems = PlayerData.gems

    end

    --创建格子
    for i = 1, #nowItems do
        --根据格子资源  加载格子  实例化·改变图片·文本·位置
        local grid = {}
        --用一张新表  代表 各自对象 里面的属性  存储对应的信息
        grid.obj = ABMgr.LoadRes('ui','ItemGrid')
        --设置父对象
        grid.obj.transform:SetParent(self.Content,false)
        --继续设置他的位置
        grid.obj.transform.localPosition = Vector3((i-1)%4*175),math.floor((i-1)/4*175),0)
        --寻找控价
        grid.imgIcon = grid.obj.transform:Find("ImageIcon"):GetComponent(typeof(Image))
        grid.Text = grid.obj.transform:Find("Count"):GetComponent(typeof(Text))
        --设置图标
        --通过道具id 读取道具配置表 得到图标信息
        local data = ItemData[nowItems[i].id]--获取data中的图表信息 (存在了图集中)
        --根据名字  加载图集  再 加载图集中的图表信息 
        local strs = string.split(data.icon,"_")
        --加载图集
        local spriteAtlas = ABMgr:LoadRes("ui",strs[i],typeof(SpriteAtlas))
        --加载图标 
        grid.imgIcon.sprite = spriteAtlas:GetSprite(strs[2])--使用图集中的 getSprite 通过名字获取image
        --设置数量
        grid.Text.text = nowItems[i].num    

        --把他存储起来
        table.insert(self.items,grid) 
    end

end
