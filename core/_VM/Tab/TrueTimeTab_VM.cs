using System;
using System.Collections.Generic;
using System.Text;

namespace core._VM
{
    public class TrueTimeTab_VM : Tab_VM
    {
        private readonly List<CommonTab_VM> commonTab_VMs;

        public List<CommonTab_VM> CommonTab_VMs => commonTab_VMs;

        public TrueTimeTab_VM()
            : base("TrueTime")
        {
            this.commonTab_VMs = new List<CommonTab_VM>();
            // 单跳模型
            List<CommonItem_VM> commonItem_VMs = new List<CommonItem_VM>();
            commonItem_VMs.Add(new CheckItem_VM("测试3", true));
            commonItem_VMs.Add(new CheckItem_VM("测试4", false));
            commonItem_VMs.Add(new TextItem_VM("测试1", "1"));
            commonItem_VMs.Add(new TextItem_VM("测试1", "1"));
            commonItem_VMs.Add(new TextItem_VM("测试1", "1"));
            commonItem_VMs.Add(new CheckItem_VM("测试3", true));
            commonItem_VMs.Add(new CheckItem_VM("测试4", false));
            commonItem_VMs.Add(new TextItem_VM("测试1", "1"));
            commonItem_VMs.Add(new TextItem_VM("测试1", "1"));
            commonItem_VMs.Add(new TextItem_VM("测试1", "1"));
            commonItem_VMs.Add(new CheckItem_VM("测试3", true));
            commonItem_VMs.Add(new CheckItem_VM("测试4", false));
            commonItem_VMs.Add(new TextItem_VM("测试1", "1"));
            commonItem_VMs.Add(new TextItem_VM("测试1", "1"));
            commonItem_VMs.Add(new TextItem_VM("测试1", "1"));
            commonItem_VMs.Add(new CheckItem_VM("测试3", true));
            commonItem_VMs.Add(new CheckItem_VM("测试4", false));
            commonItem_VMs.Add(new TextItem_VM("测试1", "1"));
            commonItem_VMs.Add(new TextItem_VM("测试1", "1"));
            commonItem_VMs.Add(new TextItem_VM("测试1", "1"));
            this.commonTab_VMs.Add(new CommonTab_VM("单跳模型", commonItem_VMs));
            // 多跳模型
            commonItem_VMs = new List<CommonItem_VM>();
            commonItem_VMs.Add(new CheckItem_VM("测试3", true));
            commonItem_VMs.Add(new CheckItem_VM("测试4", false));
            commonItem_VMs.Add(new TextItem_VM("测试2", "2"));
            commonItem_VMs.Add(new TextItem_VM("测试2", "2"));
            commonItem_VMs.Add(new TextItem_VM("测试2", "2"));
            this.commonTab_VMs.Add(new CommonTab_VM("多跳模型", commonItem_VMs));
        }
    }
}
