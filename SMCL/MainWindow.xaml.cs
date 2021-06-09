using SMCL.Models;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SMCL
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModels.MainViewModel mainViewModel = new ViewModels.MainViewModel();

        public MainWindow()
        {
            InitializeComponent();

            // 网络操作避免卡线程
            Task.Run(() =>
            {
                var hitokoto = Utils.Others.GetHitokoto();
                this.Dispatcher.Invoke(() =>
                {
                    this.HitokotoContent.Text = hitokoto.Content;
                    this.HitokotoFrom.Content = hitokoto.From;
                });
            });

            Utils.LaunchHelper.GameWindowShownEvent += GameWindowShown;

            this.DataContext = mainViewModel;
            this.StartInfo.DataContext = mainViewModel;
        }

        private void GameWindowShown()
        {
            this.Dispatcher.Invoke(() =>
            {
                this.StartGameButton.Content = "开 始 游 戏";
                this.Hide();
            });
        }

        private void HideWindow_Click(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;

        private void Exit_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void Window_Drag(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                //  mainViewModel.SnackbarMessageQueue.Enqueue("t"); // 这东西坏了，等以后看看 MD 会不会修
                if (!Utils.LaunchHelper.IsGameStarted)
                {
                    if (App.Config.UpdateSource != null)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            this.mainViewModel.ProgressBarVisibility = Visibility.Visible;
                            this.mainViewModel.StartInfo = "获取更新中...";
                        });

                        Manifest manifest = new Manifest(App.Config.UpdateSource);
                        if (manifest.List == null)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                this.mainViewModel.ProgressBarVisibility = Visibility.Collapsed;
                                this.mainViewModel.StartInfo = "下载失败 ＞﹏＜";
                                //   mainViewModel.SnackbarMessageQueue.Enqueue("下载失败 ＞﹏＜");
                                return;
                            });
                        }
                        else
                        {
                            WebClient webClient = new WebClient();
                            for (int i = 0; i < manifest.List.Count; i++)
                            {
                                this.Dispatcher.Invoke(() => this.mainViewModel.StartInfo = $"更新中 {i}/{manifest.List.Count}");
                                var item = manifest.List[i];
                                if (item.Level == ManifestLevel.Banned)
                                {
                                    if (File.Exists(item.FilePath))
                                    {
                                        if (!File.Exists(item.FilePath + ".disabled"))
                                        {
                                            File.Move(item.FilePath, item.FilePath + ".disabled");
                                        }
                                        else
                                        {
                                            File.Delete(item.FilePath);
                                        }
                                    }
                                }
                                else if (item.Level == ManifestLevel.Required)
                                {
                                    if (!File.Exists(item.FilePath))
                                    {
                                        try
                                        {
                                            if (!Directory.Exists(Path.GetDirectoryName(item.FilePath)))
                                            {
                                                Directory.CreateDirectory(Path.GetDirectoryName(item.FilePath));
                                            }

                                            webClient.DownloadFile(item.Url, item.FilePath);
                                        }
                                        catch (Exception e1)
                                        {
                                            this.Dispatcher.Invoke(() =>
                                            {
                                                this.mainViewModel.ProgressBarVisibility = Visibility.Collapsed;
                                                this.mainViewModel.StartInfo = "下载失败 ＞﹏＜";
                                                //   mainViewModel.SnackbarMessageQueue.Enqueue("下载失败 ＞﹏＜");
                                                return;
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }

                    this.Dispatcher.Invoke(() =>
                    {
                        this.mainViewModel.ProgressBarVisibility = Visibility.Visible;
                        this.mainViewModel.StartInfo = "启动中...";
                    });

                    Utils.LaunchHelper.LaunchGame();
                }
            });
        }

        private void SaveUsernameButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainViewModel.Username != null)
            {
                App.Config.Save();
                this.mainViewModel.IsUsernameNull = false;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            App.Config.Save();
        }
    }
}