using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Animation;

namespace SMCL
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private readonly string[] dlls = new string[]
        {
            "Newtonsoft.Json",
            "KMCCC.Pro",
            "MaterialDesignColors",
            "MaterialDesignThemes.Wpf",
        };

        public static Models.Config Config;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DispatcherUnhandledException += App_DispatcherUnhandledException; // 全局异常处理
        }

        public App()
        {
            Config = Models.Config.Load(); // 加载启动器配置
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve; // DLL 内置
            Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata { DefaultValue = 120 }); // 帧率
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("SMCL 发生了未经处理的异常：\n" + e.Exception.ToString(), "Oops!", MessageBoxButton.OK);
            Application.Current.Shutdown();
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string resources = null;
            foreach (var item in dlls)
            {
                if (args.Name.StartsWith(item))
                {
                    resources = item + ".dll";
                    break;
                }
            }
            if (string.IsNullOrEmpty(resources)) return null;

            var assembly = Assembly.GetExecutingAssembly();
            resources = assembly.GetManifestResourceNames().FirstOrDefault(s => s.EndsWith(resources));

            if (string.IsNullOrEmpty(resources)) return null;

            using (Stream stream = assembly.GetManifestResourceStream(resources))
            {
                if (stream == null) return null;
                var block = new byte[stream.Length];
                stream.Read(block, 0, block.Length);
                return Assembly.Load(block);
            }
        }
    }
}