using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace core._VM
{
    public class MainWindow_VM : ViewModelBase
    {
        private readonly ObservableCollection<Tab_VM> tabVMs = new ObservableCollection<Tab_VM>();
        private string tip = "在此处获取操作提示";

        public MainWindow_VM()
        {
            // 把自己挂到全局资源上
            ResourceManager.mainWindow_VM = this;
            // 添加各类建模工具的面板
            tabVMs.Add(new TrueTimeTab_VM());
            tabVMs.Add(new UppaalTab_VM());
            tabVMs.Add(new SpinTab_VM());
        }

        /// <summary>
        /// 组织左侧的三种建模工具（TrueTime、UPPAAL、SPIN）的模型面板
        /// </summary>
        public ObservableCollection<Tab_VM> TabVMs => tabVMs;

        /// <summary>
        /// 主窗体下方的提示栏内容
        /// </summary>
        public string Tip { get => tip; set => this.RaiseAndSetIfChanged(ref tip, value); }
    }
}
