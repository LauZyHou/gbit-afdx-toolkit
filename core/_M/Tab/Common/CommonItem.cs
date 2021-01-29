using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace core._M
{
    /// <summary>
    /// 通用项的抽象基类，所有的项一定有一个无法二次设定的标签
    /// </summary>
    public abstract class CommonItem : ReactiveObject
    {
        private readonly string label;

        public string Label => label;

        public CommonItem(string _label)
        {
            this.label = _label;
        }
    }
}
