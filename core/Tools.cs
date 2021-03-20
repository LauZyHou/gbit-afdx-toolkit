using core._M;
using core._VM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace core
{
    public class Tools
    {
        /// <summary>
        /// 构建适用于某个工具模型的所有CommonTab_VM
        /// </summary>
        /// <param name="modelType">工具模型类型</param>
        /// <returns>构建好的CommonTab_VM的列表</returns>
        public static List<CommonTab_VM> BuildCommonTabVMList(ModelType modelType)
        {
            string templatePath = modelType switch
            {
                ModelType.UPPAAL => ResourceManager.UppaalTemplatePath,
                ModelType.SPIN => ResourceManager.SpinTemplatePath,
                ModelType.TrueTime => ResourceManager.TrueTimeTemplatePath,
                _ => "",
            };
            // 待返回的结果列表
            List<CommonTab_VM> res = new List<CommonTab_VM>();
            // 获取待搜索目录下的所有.gafdx.metainf文件
            string[] metaInfFiles = Directory.GetFiles(templatePath, "*.gafdx.metainf", SearchOption.TopDirectoryOnly);
            // 遍历每个.gafdx.metainf文件，生成一个CommonTab_VM并写入
            foreach (string file in metaInfFiles)
            {
                // 截取文件名的非后缀部分
                string smallName = GetSmallName(file);
                // 当前Tab页的所有选项列表
                List<CommonItem_VM> commonItem_VMs = new List<CommonItem_VM>();
                // 读取文件的每一行，以构造出每一项
                string[] lines = File.ReadAllLines(file);
                foreach (string line in lines)
                {
                    // 如果是空行直接跳过
                    if (line.Length == 0) continue;
                    // 如果不是空行就按空白符切分成三个部分
                    string[] records = Regex.Split(line, "\\s+", RegexOptions.Singleline);
                    // 一定是三元组<key, bool/string, label>
                    if (records.Length != 3) continue;
                    // 取出来
                    string key = records[0], itemType = records[1], label = records[2];
                    // 添加到选项列表里
                    if (itemType == "string")
                        commonItem_VMs.Add(new TextItem_VM(key, label, ""));
                    else if (itemType == "bool")
                        commonItem_VMs.Add(new CheckItem_VM(key, label, false));
                    else
                    {
                        FlushTip($"解析{file}时出错，无法识别的项类型{itemType}");
                        return res;
                    }
                }
                // 构造点击生成按钮时调用的委托 fixme
                Func<bool> genFunc = delegate ()
                {
                    // 对所有的项生成规则列表
                    LinkedList<Tuple<string, string>> rules = new LinkedList<Tuple<string, string>>();
                    foreach (CommonItem_VM commonItem_VM in commonItem_VMs)
                    {
                        if (commonItem_VM is TextItem_VM)
                        {
                            TextItem_VM textItem_VM = commonItem_VM as TextItem_VM;
                            string key = textItem_VM.TextItem.Key;
                            string val = textItem_VM.TextItem.Val;
                            rules.AddLast(new Tuple<string, string>(key, val));
                        }
                        else if (commonItem_VM is CheckItem_VM)
                        {
                            CheckItem_VM checkItem_VM = commonItem_VM as CheckItem_VM;
                            string key = checkItem_VM.CheckItem.Key;
                            string val = checkItem_VM.CheckItem.Val ? "1" : "0";
                            rules.AddLast(new Tuple<string, string>(key, val));
                        }
                    }
                    // 根据工具模型类型，特定模型名，规则列表来生成模型
                    GenerateModel(modelType, smallName, rules);
                    return true;
                };
                // 构造当前的Tab页VM，传入从文件名解析出的模型名，从文件内容解析出的项列表
                CommonTab_VM commonTab_VM = new CommonTab_VM(smallName, commonItem_VMs, genFunc);
                // 加入到当前Tab页里
                res.Add(commonTab_VM);
            }
            return res;
        }

        /// <summary>
        /// 目录拷贝时替换文件内容
        /// 先拷贝sourceDir下的所有文件到destDir下，然后遍历rules做替换
        /// </summary>
        /// <param name="sourceDir">源目录</param>
        /// <param name="destDir">目标目录</param>
        /// <param name="rules">替换规则</param>
        /// <param name="filter">文件匹配规则</param>
        /// <returns>是否成功</returns>
        public static bool ReplaceOnCopyDir(string sourceDir, string destDir, LinkedList<Tuple<string, string>> rules, string filter = "*")
        {
            // 获取源目录，模板不存在时提示
            DirectoryInfo sourceInfo = new DirectoryInfo(sourceDir);
            if (!sourceInfo.Exists)
            {
                FlushTip("模板" + sourceInfo.FullName + "不存在！");
                return false;
            }
            // 如果目标目录已经存在，就把它删除
            DirectoryInfo destInfo = new DirectoryInfo(destDir);
            if (destInfo.Exists)
            {
                Directory.Delete(destDir, true);
            }
            // 将源目录拷贝一份放置到目标目录位置
            CopyAll(sourceInfo, destInfo);
            // 获取目标目录的所有子文件
            string[] allSubFiles = Directory.GetFiles(destDir, filter, SearchOption.AllDirectories);
            // 对所有的子文件做字符串替换
            foreach (string fpath in allSubFiles)
            {
                ReplaceFileContent(fpath, rules);
            }
            return true;
        }

        /// <summary>
        /// 打日志，同时能解决图形界面难以通过命令行调试的问题
        /// </summary>
        /// <param name="content">日志内容</param>
        /// <param name="logTime">是否在写入前先写入时间</param>
        public static void Log(string content, bool logTime = true)
        {
            string outDir = "./Log";
            // 没有该目录时创建一下
            if (!Directory.Exists(outDir))
            {
                Directory.CreateDirectory(outDir);
            }
            // 日志将记录在以当前日期为文件名的txt文件中
            TextWriter textWriter = new StreamWriter(Path.Combine(outDir, $"{DateTime.Now:yyyy-MM-dd}.txt"), true);
            // 如果需要记录时间，先写入当前时间
            if (logTime)
            {
                textWriter.WriteLine($"-------{DateTime.Now:yyyy-MM-dd HH:mm:ss}-------");
            }
            // 写入日志内容
            textWriter.WriteLine(content);
            // 关闭日志文件
            textWriter.Close();
        }

        /// <summary>
        /// 更新提示信息
        /// </summary>
        /// <param name="tip">新的提示信息</param>
        public static void FlushTip(string tip)
        {
            ResourceManager.mainWindow_VM.Tip = tip;
        }

        /// <summary>
        /// 根据模型类型和具体模型名获取模板路径和产品路径
        /// </summary>
        /// <param name="modelType">模型类型</param>
        /// <param name="specType">具体模型名称</param>
        /// <param name="source">模板路径</param>
        /// <param name="dest">产品路径</param>
        public static void GetSoruceAndDest(ModelType modelType, string specType, out string source, out string dest)
        {
            string templatePath, productPath;
            switch (modelType)
            {
                case ModelType.UPPAAL:
                    templatePath = ResourceManager.UppaalTemplatePath;
                    productPath = ResourceManager.UppaalProductPath;
                    break;
                case ModelType.SPIN:
                    templatePath = ResourceManager.SpinTemplatePath;
                    productPath = ResourceManager.SpinProductPath;
                    break;
                case ModelType.TrueTime:
                    templatePath = ResourceManager.TrueTimeTemplatePath;
                    productPath = ResourceManager.TrueTimeProductPath;
                    break;
                default:
                    source = dest = "";
                    return;
            }
            source = Path.Combine(templatePath, specType);
            dest = Path.Combine(productPath, specType);
        }

        /// <summary>
        /// 生成指定工具框架的具体模型
        /// 先获取到模板目录和生成目录，然后将模板复制过去，然后做规则替换
        /// </summary>
        /// <param name="modelType">工具框架</param>
        /// <param name="specName">具体模型</param>
        /// <param name="rules">替换规则</param>
        public static void GenerateModel(ModelType modelType, string specName, LinkedList<Tuple<string, string>> rules)
        {
            // 获取模型模板目录和模型的生成模板
            string sourcePath, destPath;
            GetSoruceAndDest(modelType, specName, out sourcePath, out destPath);
            // 使用替换规则，将源目录的模型模板拷贝到生成目录并替换
            bool res = ReplaceOnCopyDir(sourcePath, destPath, rules);
            if (!res) return;
            // 刷新用户提示
            FlushTip($"成功生成{modelType}的{specName}模型");
        }

        #region 私有

        /// <summary>
        /// 将源目录拷贝为目标目录
        /// </summary>
        /// <param name="source">源目录</param>
        /// <param name="target">目标目录</param>
        private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            // 创建目标目录
            Directory.CreateDirectory(target.FullName);
            // 拷贝所有子文件
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }
            // 递归拷贝所有子目录
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        /// <summary>
        /// 异步替换文件内容
        /// </summary>
        /// <param name="filePath">待替换的文件</param>
        /// <param name="rules">替换规则</param>
        private static async void ReplaceFileContent(string filePath, LinkedList<Tuple<string, string>> rules)
        {
            // 读取文件的所有内容当成一个字符串
            string text = File.ReadAllText(filePath);
            // 用每个规则做替换
            foreach (var rule in rules)
            {
                text = text.Replace(rule.Item1, rule.Item2);
            }
            // 异步写回文件
            await File.WriteAllTextAsync(filePath, text);
        }


        /// <summary>
        /// 截取文件名的非后缀部分
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string GetSmallName(string s)
        {
            // 先按".gafdx"划分，然后取最前面
            string res = s.Split(".gafdx")[0];
            // 再从后往前扫到第一个有'/'或者'\'的位置，取最后面
            string[] tmp = res.Split('/');
            res = tmp[tmp.Length - 1];
            tmp = res.Split('\\');
            res = tmp[tmp.Length - 1];
            return res;
        }

        #endregion
    }
}
