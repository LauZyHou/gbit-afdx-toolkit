using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace core._VM
{
    public class MainWindow_VM : ViewModelBase
    {
        private readonly ObservableCollection<Tab_VM> tabVMs = new ObservableCollection<Tab_VM>();
        private string tip = "GAFDX已根据*.gafdx.metainf构建配置项";

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

        /// <summary>
        /// 点击右下角的问号图标，打开Wiki页面
        /// </summary>
        private static void OnAskUse()
        {
            string url = "https://github.com/LauZyHou/gbit-afdx-toolkit/wiki/GAFDX";
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
