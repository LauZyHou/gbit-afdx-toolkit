﻿using System;
using System.Collections.Generic;
using System.Text;

namespace core._VM
{
    public class UppaalTab_VM : Tab_VM
    {
        private readonly List<CommonTab_VM> commonTab_VMs;

        public List<CommonTab_VM> CommonTab_VMs => commonTab_VMs;

        public UppaalTab_VM()
            : base("UPPAAL")
        {
            this.commonTab_VMs = new List<CommonTab_VM>();
            // 单跳模型
            List<CommonItem_VM> commonItem_VMs = new List<CommonItem_VM>();
            commonItem_VMs.Add(new TextItem_VM("测试1", "1"));
            commonItem_VMs.Add(new CheckItem_VM("测试3", true));
            commonItem_VMs.Add(new CheckItem_VM("测试4", false));
            commonItem_VMs.Add(new TextItem_VM("测试1", "1"));
            commonItem_VMs.Add(new TextItem_VM("测试1", "1"));
            this.commonTab_VMs.Add(new CommonTab_VM("单跳模型", commonItem_VMs, OneStepModel));
            // 多跳模型
            commonItem_VMs = new List<CommonItem_VM>();
            commonItem_VMs.Add(new TextItem_VM("测试2", "2"));
            commonItem_VMs.Add(new TextItem_VM("测试2", "2"));
            commonItem_VMs.Add(new TextItem_VM("测试2", "2"));
            commonItem_VMs.Add(new CheckItem_VM("测试3", true));
            commonItem_VMs.Add(new CheckItem_VM("测试4", false));
            this.commonTab_VMs.Add(new CommonTab_VM("多跳模型", commonItem_VMs, MultiStepModel));
        }

        #region 不同模型生成时调用的函数对象

        private string OneStepModel()
        {
            return "成功生成up单跳模型";
        }

        private string MultiStepModel()
        {
            return "成功生成up多条模型";
        }

        #endregion
    }
}
