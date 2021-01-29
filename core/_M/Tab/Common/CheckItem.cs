using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace core._M
{
    public class CheckItem : CommonItem
    {
        private bool @checked;

        public bool Checked { get => @checked; set => this.RaiseAndSetIfChanged(ref @checked, value); }

        public CheckItem(string _label, bool @checked)
            : base(_label)
        {
            this.Checked = @checked;
        }
    }
}
