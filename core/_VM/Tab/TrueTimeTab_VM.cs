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
            this.commonTab_VMs = Tools.BuildCommonTabVMList(_M.ModelType.TrueTime);
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
