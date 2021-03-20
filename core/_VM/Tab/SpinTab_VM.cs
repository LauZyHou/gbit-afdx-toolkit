using System;
using System.Collections.Generic;
using System.Text;

namespace core._VM
{
    public class SpinTab_VM : Tab_VM
    {
        private readonly List<CommonTab_VM> commonTab_VMs;

        public List<CommonTab_VM> CommonTab_VMs => commonTab_VMs;

        public SpinTab_VM()
            : base(_M.ModelType.SPIN.ToString())
        {
            this.commonTab_VMs = Tools.BuildCommonTabVMList(_M.ModelType.SPIN);
        }
    }
}
