using core._M;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace core._VM
{
    /// <summary>
    /// 通用的Tab选项卡，是对最内层的各个Tab的VM的抽象
    /// </summary>
    public class CommonTab_VM : Tab_VM
    {
        private List<CommonItem_VM> commonItem_VMs;
        private Func<string> genModel_Func; // 点击生成模型时委托调用的函数对象

        /// <summary>
        /// 所有选项组成的列表
        /// </summary>
        public List<CommonItem_VM> CommonItem_VMs { get => commonItem_VMs; set => this.RaiseAndSetIfChanged(ref commonItem_VMs, value); }


        public CommonTab_VM(string _name, List<CommonItem_VM> _commonItem_VMs, Func<string> _genModel_Func)
            : base(_name)
        {
            // 传入的列表一定不是空的，否则就没有需要配置的参数了
            System.Diagnostics.Debug.Assert(!(_commonItem_VMs is null));
            // 把这个列表传进去，自动刷新前端
            this.CommonItem_VMs = _commonItem_VMs;
            // 保存生成模型时要调用的函数对象
            this.genModel_Func = _genModel_Func;
        }

        /// <summary>
        /// 点击【生成模型】按钮
        /// </summary>
        public void GenModel()
        {
            // 不同的模型在构造时传入的生成方法是不一样的，这里直接调用传入的生成方法
            // 返回值是要提示给用户的信息，所以直接刷新Tips
            ResourceManager.mainWindow_VM.Tip = genModel_Func();
        }
    }
}
