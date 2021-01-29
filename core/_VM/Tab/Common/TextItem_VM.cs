using core._M;
using System;
using System.Collections.Generic;
using System.Text;

namespace core._VM
{
    /// <summary>
    /// 文本项的VM，维护一个只读的文本项，改项的时候是get出来再改里面的M
    /// </summary>
    public class TextItem_VM : CommonItem_VM
    {
        private readonly TextItem textItem;

        public TextItem TextItem => textItem;

        public TextItem_VM(string _label, string _text)
        {
            this.textItem = new TextItem(_label, _text);
        }
    }
}
