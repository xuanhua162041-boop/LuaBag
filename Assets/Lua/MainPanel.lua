MainPanel = {}--只要是一个新的面板 我们就新建一个表
--需要做 实例化面板对象 
--为这个面板处理对应的逻辑  比如按钮点击登

--用来存储面板的对象
MainPanel.panelObj = nil --不是必须写的  因为lua没有声明变量 的概念

MainPanel.btnRole = nil
MainPanel.btnSkill = nil

--初始化改=该面板   控件事件的监听
function MainPanel:Init()
    if self.panelObj == nil then--面板没有实例化过 才去实例化
    --1.实例化面板对象  注意此处用的是冒号调用, 
        self.panelObj = ABMgr:LoadRes("ui","MainPanel",typeof(GameObject))
        self.panelObj.transform:SetParent(Canvas,false)
    --2.找到面板控件
        self.btnRole = self.panelObj.transform:Find('BtnRole'):GetComponent(typeof(Button))--通过父物体找到子对象 的transform再找到   Button类型的组件
        self.btnSkill = self.panelObj.transform:Find('BtnSkill'):GetComponent(typeof(Button))
    --3.为面板加上事件监听 运行点击等等的逻辑
        --如果直接点 传入自己的函数, 那么在函数内部 无法用self获取内容
        --self.btnRole.onClick:AddListener(self.ButtonRoleClick)
        self.btnRole.onClick:AddListener(function ()--这里点击按钮 其实是执行的该匿名函数, 但是匿名函数执行了真正的点击方法
        self:ButtonRoleClick()
    end)
end

    
end

function MainPanel:ShowMe()
    self:Init()--showme作为初始化入口
    self.panelObj:SetActive(true)
end

function MainPanel:HideMe()
    self.panelObj:SetActive(false)
end

function MainPanel:ButtonRoleClick()
    print("点击了按钮BUTTON Role")
    print(self.panelObj)
    BagPanel:ShowMe()
end