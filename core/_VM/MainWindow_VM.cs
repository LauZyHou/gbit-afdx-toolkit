using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace core._VM
{
    public class MainWindow_VM : ViewModelBase
    {
        private readonly ObservableCollection<Tab_VM> tabVMs = new ObservableCollection<Tab_VM>();

        public MainWindow_VM()
        {
            tabVMs.Add(new TrueTimeTab_VM());
            tabVMs.Add(new UppaalTab_VM());
            tabVMs.Add(new SpinTab_VM());
            // text
            List<CommonItem_VM> commonItem_VMs = new List<CommonItem_VM>();
            commonItem_VMs.Add(new TextItem_VM("测试1", "1"));
            commonItem_VMs.Add(new TextItem_VM("测试2", "2"));
            CommonTab_VM commonTab_VM = new CommonTab_VM("测试", commonItem_VMs);
            tabVMs.Add(commonTab_VM);
        }

        public ObservableCollection<Tab_VM> TabVMs => tabVMs;
    }
}
