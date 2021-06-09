using KMCCC.Authentication;
using KMCCC.Launcher;
using Microsoft.VisualBasic.Devices;
using System;
using System.Diagnostics;
using System.Windows;

namespace SMCL.Utils
{
    public static class LaunchHelper
    {
        public static event Action GameWindowShownEvent;

        public static bool IsGameStarted = false;
        public static bool IsGameWindowShown = false;

        /// <summary>
        /// 不知为何 KMCCC 的 FindJava 有点问题，这里换个方法写下。
        /// 一定要在编译设置里关闭"优化编码"，否则 Process 失效 （所以这是为什么？？？
        /// </summary>
        /// <returns> Java 路径。若为 null 则未找到 </returns>
        public static string FindJava()
        {
            string path = null;
            bool done = false;
            ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe")
            {
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
            };

            Process process = new Process() { StartInfo = processStartInfo, };
            process.ErrorDataReceived += (s, e) =>
            {
                path = null;
                done = true;
            };

            process.OutputDataReceived += (s, e) =>
            {
                if (e.Data.Contains("Opened"))
                {
                    var fullPath = e.Data.Replace("[Opened ", string.Empty).Replace("]", string.Empty);
                    path = fullPath.Split(new string[] { "lib" }, StringSplitOptions.None)[0] + "bin\\javaw.exe";
                    done = true;
                    process.CancelOutputRead();
                }
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.StandardInput.WriteLine("java -verbose");

            while (!done) { }
            return path;
        }

        public static void LaunchGame()
        {
            IsGameStarted = true;

            #region Memory

            int maxMemory; // MB
            try
            {
                maxMemory = Convert.ToInt32(App.Config.Memory);
            }
            catch
            {
                var totalMemory = new ComputerInfo().TotalPhysicalMemory / 1024 / 1024;
                if (totalMemory <= 4096)
                {
                    maxMemory = 2048;
                }
                else
                {
                    maxMemory = ((int)(totalMemory / 2 / 4)) * 4;
                    maxMemory = maxMemory > 4096 ? 4096 : maxMemory;
                }
            }

            #endregion Memory

            LauncherCore core = LauncherCore.Create(new LauncherCoreCreationOption(
#if DEBUG
                gameRootPath: @"E:\Minecraft\寰宇之域客户端V1.4\.minecraft",
#else
                gameRootPath: @".minecraft",
#endif
                javaPath: App.Config.JavaPath == "Auto" ? FindJava() : App.Config.JavaPath
                ));

            core.GameLog += GameLog;
            core.GameExit += GameExit;

            var gameOptions = new LaunchOptions()
            {
                Version = core.GetVersion(App.Config.Version),
                Authenticator = new OfflineAuthenticator(App.Config.Username.Trim()),
                VersionType = "SMCL",
                MaxMemory = maxMemory,
                Size = new WindowSize { Height = 768, Width = 1280 }
            };

            if (App.Config.ServerIp != null)
            {
                gameOptions.Server = new ServerInfo() { Address = App.Config.ServerIp };
            }

            var result = core.Launch(gameOptions, (Action<MinecraftLaunchArguments>)(x => { })); // 可选 ( 启动前修改参数

            if (!result.Success)
            {
                Debug.WriteLine("启动失败：[{0}] {1}", result.ErrorType, result.ErrorMessage);
                if (result.Exception != null)
                {
                    Debug.WriteLine(result.Exception.Message);
                    Debug.WriteLine(result.Exception.Source);
                    Debug.WriteLine(result.Exception.StackTrace);
                    MessageBox.Show($"启动失败：[{result.ErrorType}] {result.ErrorMessage}\n" + result.Exception.ToString());
                }
                else
                {
                    MessageBox.Show($"启动失败：[{result.ErrorType}] {result.ErrorMessage}");
                }
            }
        }

        private static void GameExit(LaunchHandle arg1, int arg2)
        {
            if (arg2 != 0)
            {
                MessageBox.Show("游戏非正常退出。", "Oops!", MessageBoxButton.OK);
            }

            App.Current.Dispatcher.Invoke(() =>
            {
                App.Current.Shutdown();
            });
        }

        private static void GameLog(LaunchHandle arg1, string arg2)
        {
            Debug.WriteLine(arg2);
            if (arg2.Contains("LWJGL") && !IsGameWindowShown)
            {
                GameWindowShownEvent();
                IsGameWindowShown = true;
            }
        }
    }
}