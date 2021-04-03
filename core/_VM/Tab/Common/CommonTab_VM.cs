using Avalonia.Controls;
using core._M;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace core._VM
{
    /// <summary>
    /// 通用的Tab选项卡，是对最内层的各个Tab的VM的抽象
    /// </summary>
    public class CommonTab_VM : Tab_VM
    {
        private List<CommonItem_VM> commonItem_VMs;
        private Func<bool> genModel_Func; // 点击生成模型时委托调用的函数对象

        /// <summary>
        /// 所有选项组成的列表
        /// </summary>
        public List<CommonItem_VM> CommonItem_VMs { get => commonItem_VMs; set => this.RaiseAndSetIfChanged(ref commonItem_VMs, value); }


        public CommonTab_VM(string _name, List<CommonItem_VM> _commonItem_VMs, Func<bool> _genModel_Func)
            : base(_name)
        {
            // 传入的列表一定不是空的，否则就没有需要配置的参数了
            System.Diagnostics.Debug.Assert(!(_commonItem_VMs is null));
            // 把这个列表传进去，自动刷新前端
            this.CommonItem_VMs = _commonItem_VMs;
            // 保存生成模型时要调用的函数对象
            this.genModel_Func = _genModel_Func;
        }

        /// <summary>
        /// 点击【生成模型】按钮
        /// </summary>
        public void GenModel()
        {
            // 不同的模型在构造时传入的生成方法是不一样的，这里直接调用传入的生成方法
            genModel_Func();
        }

        /// <summary>
        /// 点击【打开】图标，打开配置文件，将配置导入到当前页
        /// </summary>
        private async void OnOpenSetting()
        {
            // 调用打开文件的窗体，获取配置文件
            string openFileName = await GetOpenFileName();
            if (string.IsNullOrEmpty(openFileName))
            {
                Tools.FlushTip("取消打开项目文件");
                return;
            }
            // 检查文件存在性
            if (!File.Exists(openFileName))
            {
                Tools.FlushTip($"未找到{openFileName}");
                return;
            }
            // 读取配置文件到字典
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            string[] texts = File.ReadAllLines(openFileName);
            foreach (string line in texts)
            {
                string[] pair = line.Split(' ');
                if (pair.Length != 2)
                {
                    continue;
                }
                keyValuePairs[pair[0]] = pair[1];
            }
            // 将配置写入当前页面中
            foreach (CommonItem_VM commonItem_VM in commonItem_VMs)
            {
                if (commonItem_VM is CheckItem_VM)
                {
                    CheckItem_VM checkItem_VM = commonItem_VM as CheckItem_VM;
                    string key = checkItem_VM.CheckItem.Key;
                    if (keyValuePairs.ContainsKey(key))
                    {

                        checkItem_VM.CheckItem.Val = (keyValuePairs[key] == "true");
                    }
                }
                else if (commonItem_VM is TextItem_VM)
                {
                    TextItem_VM textItem_VM = commonItem_VM as TextItem_VM;
                    string key = textItem_VM.TextItem.Key;
                    if (keyValuePairs.ContainsKey(key))
                    {
                        textItem_VM.TextItem.Val = keyValuePairs[key];
                    }
                }
            }
            Tools.FlushTip($"读取了配置文件{openFileName}");
        }

        /// <summary>
        /// 点击【保存】图标，保存配置文件
        /// </summary>
        private async void OnSaveSetting()
        {
            Tools.FlushTip("save");
        }

        #region 私有

        // 预打开文件：返回文件路径
        private async Task<string> GetOpenFileName()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "配置文件", Extensions = { "gafdx.setting" } });
            string[] result = await dialog.ShowAsync(ResourceManager.mainWindow_V);
            return result == null ? "" : string.Join(" ", result); // Linux bugfix:直接关闭时不能返回null
        }

        // 预保存文件：返回文件路径
        private async Task<string> GetSaveFileName()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "配置文件", Extensions = { "gafdx.setting" } });
            string result = await dialog.ShowAsync(ResourceManager.mainWindow_V);
            // Linux bugfix:某些平台输入文件名不会自动补全.gafdx.setting后缀名,这里判断一下手动补上
            if (string.IsNullOrEmpty(result) || result.EndsWith(".gafdx.setting"))
                return result;
            return result + ".gafdx.setting";
        }


        #endregion
    }
}
