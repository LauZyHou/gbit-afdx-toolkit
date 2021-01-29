using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace core._M
{
    public class TextItem : CommonItem
    {
        private string text;

        public string Text { get => text; set => this.RaiseAndSetIfChanged(ref text, value); }

        public TextItem(string _label, string _text)
            : base(_label)
        {
            this.Text = _text;
        }
    }
}
