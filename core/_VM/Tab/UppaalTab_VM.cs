using System;
using System.Collections.Generic;
using System.Text;

namespace core._VM
{
    public class UppaalTab_VM : Tab_VM
    {
        private readonly List<CommonTab_VM> commonTab_VMs;

        public List<CommonTab_VM> CommonTab_VMs => commonTab_VMs;

        public UppaalTab_VM()
            : base(_M.ModelType.UPPAAL.ToString())
        {
            this.commonTab_VMs = Tools.BuildCommonTabVMList(_M.ModelType.UPPAAL);
        }
    }
}
