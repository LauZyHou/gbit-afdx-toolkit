using core._M;
using System;
using System.Collections.Generic;
using System.Text;

namespace core._VM
{
    /// <summary>
    /// 对勾项的VM，维护一个只读的对勾项，改项的时候是get出来再改里面的M
    /// </summary>
    public class CheckItem_VM : CommonItem_VM
    {
        private readonly CheckItem checkItem;

        public CheckItem CheckItem => checkItem;

        public CheckItem_VM(string _label, bool @checked)
        {
            this.checkItem = new CheckItem(_label, @checked);
        }
    }
}
