using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace core._M
{
    public class TextItem : CommonItem
    {
        private string val;

        public string Val { get => val; set => this.RaiseAndSetIfChanged(ref val, value); }

        /// <summary>
        /// string设定项
        /// </summary>
        /// <param name="_key">替换键</param>
        /// <param name="_label">用户提示标签</param>
        /// <param name="_val">存储值</param>
        public TextItem(string _key, string _label, string _val)
            : base(_key, _label)
        {
            this.Val = _val;
        }
    }
}
