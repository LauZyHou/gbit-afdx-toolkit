﻿using System;
using System.Collections.Generic;
using System.Text;

namespace core._VM
{
    public class SpinTab_VM : Tab_VM
    {
        private readonly List<CommonTab_VM> commonTab_VMs;

        public List<CommonTab_VM> CommonTab_VMs => commonTab_VMs;

        public SpinTab_VM()
            : base("SPIN")
        {
            this.commonTab_VMs = new List<CommonTab_VM>();
            // 单跳模型
            List<CommonItem_VM> commonItem_VMs = new List<CommonItem_VM>();
            commonItem_VMs.Add(new CheckItem_VM("【key】", "测试3", true));
            this.commonTab_VMs.Add(new CommonTab_VM("单跳模型", commonItem_VMs, OneStepModel));
            // 多跳模型
            commonItem_VMs = new List<CommonItem_VM>();
            commonItem_VMs.Add(new TextItem_VM("【key】", "测试2", "2"));
            this.commonTab_VMs.Add(new CommonTab_VM("多跳模型", commonItem_VMs, MultiStepModel));
        }

        #region 不同模型生成时调用的函数对象

        private bool OneStepModel()
        {
            return true;
        }

        private bool MultiStepModel()
        {
            return true;
        }

        #endregion
    }
}
