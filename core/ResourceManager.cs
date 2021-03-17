using core._V;
using core._VM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace core
{
    /// <summary>
    /// 全局资源管理器
    /// </summary>
    public class ResourceManager
    {
        /// <summary>
        /// 主窗体View Moel
        /// </summary>
        public static MainWindow_VM mainWindow_VM;
        /// <summary>
        ///  主窗体View
        /// </summary>
        public static MainWindow_V mainWindow_V;
        /// <summary>
        /// UPPAAL模板所在的根目录
        /// </summary>
        public static readonly string UppaalTemplatePath = Path.Combine(".", "Templates", "UPPAAL");
        /// <summary>
        /// SPIN模板所在的根目录
        /// </summary>
        public static readonly string SpinTemplatePath = Path.Combine(".", "Templates", "SPIN");
        /// <summary>
        /// TrueTime模板所在的根目录
        /// </summary>
        public static readonly string TrueTimeTemplatePath = Path.Combine(".", "Templates", "TrueTime");
        /// <summary>
        /// UPPAAL生成品所在的根目录
        /// </summary>
        public static readonly string UppaalProductPath = Path.Combine(".", "Products", "UPPAAL");
        /// <summary>
        /// SPIN生成品所在的根目录
        /// </summary>
        public static readonly string SpinProductPath = Path.Combine(".", "Products", "SPIN");
        /// <summary>
        /// TrueTime生成品所在的根目录
        /// </summary>
        public static readonly string TrueTimeProductPath = Path.Combine(".", "Products", "TrueTime");
    }
}
