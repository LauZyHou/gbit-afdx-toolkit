using core._M;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace core
{
    public class Tools
    {
        /// <summary>
        /// 目录拷贝时替换文件内容
        /// 先遍历sourceDir下的所有文件，对于每个文件内容字符串，遍历rules做替换，然后写入destDir下的相应位置
        /// </summary>
        /// <param name="sourceDir">源目录</param>
        /// <param name="destDir">目标目录</param>
        /// <param name="rules">替换规则</param>
        /// <param name="filter">文件匹配规则</param>
        public static void ReplaceOnCopyDir(string sourceDir, string destDir, LinkedList<Tuple<string, string>> rules, string filter = "*")
        {
            // 获取源目录，模板不存在时提示
            DirectoryInfo sourceInfo = new DirectoryInfo(sourceDir);
            if (!sourceInfo.Exists)
            {
                FlushTip("模板" + sourceInfo.FullName + "不存在！");
                return;
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

        #endregion
    }
}
