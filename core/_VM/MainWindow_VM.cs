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
            tabVMs.Add(new Tab_VM("共享配置"));
            tabVMs.Add(new Tab_VM("TrueTime仿真模型配置"));
            tabVMs.Add(new Tab_VM("UPPAAL形式化模型配置"));
            tabVMs.Add(new Tab_VM("SPIN形式化模型配置"));
        }

        public ObservableCollection<Tab_VM> TabVMs => tabVMs;
    }
}
