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
        }

        public ObservableCollection<Tab_VM> TabVMs => tabVMs;
    }
}
