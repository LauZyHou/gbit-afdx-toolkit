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

        public List<CommonItem_VM> CommonItem_VMs { get => commonItem_VMs; set => this.RaiseAndSetIfChanged(ref commonItem_VMs, value); }

        public CommonTab_VM(string _name, List<CommonItem_VM> _commonItem_VMs)
            : base(_name)
        {
            // 传入的列表一定不是空的，否则就没有需要配置的参数了
            System.Diagnostics.Debug.Assert(!(_commonItem_VMs is null));
            // 把这个列表传进去，自动刷新前端
            this.CommonItem_VMs = _commonItem_VMs;
        }
    }
}
