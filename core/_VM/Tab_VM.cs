using System;
using System.Collections.Generic;
using System.Text;

namespace core._VM
{
    public class Tab_VM : ViewModelBase
    {
        private readonly string name;

        public Tab_VM()
        {
            this.name = "无名称";
        }

        public Tab_VM(string name)
        {
            this.name = name;
        }

        public string Name => name;
    }
}
