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
            : base(_M.ModelType.TrueTime.ToString())
        {
            this.commonTab_VMs = Tools.BuildCommonTabVMList(_M.ModelType.TrueTime);
        }
    }
}
