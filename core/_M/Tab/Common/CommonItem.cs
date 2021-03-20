using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace core._M
{
    /// <summary>
    /// 通用项的抽象基类，具有标签和替换键
    /// </summary>
    public abstract class CommonItem : ReactiveObject
    {
        private readonly string key;
        private readonly string label;

        public string Key => key;
        public string Label => label;

        /// <summary>
        /// 通用项构造
        /// </summary>
        /// <param name="_key">替换键，形如【key】</param>
        /// <param name="_label">标签，用于给用户做语义说明</param>
        public CommonItem(string _key, string _label)
        {
            this.key = _key;
            this.label = _label;
        }
    }
}
