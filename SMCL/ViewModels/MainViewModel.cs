using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SMCL.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private BitmapImage backgroudImage;
        private bool isUsernameNull;
        private string startInfo = "开 始 游 戏";
        private bool canSave = false;
        private Visibility progressBarVisibility = Visibility.Collapsed;

        public Visibility ProgressBarVisibility
        {
            get { return progressBarVisibility; }
            set
            {
                progressBarVisibility = value;
                OnPropertyChanged("ProgressBarVisibility");
            }
        }

        public bool CanSave
        {
            get => canSave;
            set
            {
                canSave = value;
                OnPropertyChanged("CanSave");
            }
        }

        public string StartInfo
        {
            get { return startInfo; }
            set
            {
                startInfo = value;
                OnPropertyChanged("StartInfo");
            }
        }

        public BitmapImage BackgroudImage
        {
            get { return backgroudImage; }
            set
            {
                backgroudImage = value;
                OnPropertyChanged("BackgroudImage");
            }
        }

        public bool IsUsernameNull
        {
            get { return isUsernameNull; }
            set
            {
                isUsernameNull = value;
                OnPropertyChanged("IsUsernameNull");
            }
        }

        public string Username
        {
            get { return App.Config.Username; }
            set
            {
                App.Config.Username = value;
                OnPropertyChanged("Username");

                if (!string.IsNullOrWhiteSpace(value))
                {
                    CanSave = true;
                }
            }
        }

        public string Memory
        {
            get { return App.Config.Memory; }
            set
            {
                App.Config.Memory = value;
                OnPropertyChanged("Memory");
            }
        }

        public string JavaPath
        {
            get { return App.Config.JavaPath; }
            set
            {
                App.Config.JavaPath = value;
                OnPropertyChanged("Memory");
            }
        }

        public string Version
        {
            get { return App.Config.Version; }
            set
            {
                App.Config.Version = value;
                OnPropertyChanged("Version");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            this.BackgroudImage = Utils.Others.GetRandomImage();
            this.IsUsernameNull = App.Config.Username == null;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}