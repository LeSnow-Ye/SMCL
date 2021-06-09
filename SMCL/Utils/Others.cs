using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Media.Imaging;

namespace SMCL.Utils
{
    public static class Others
    {
        /// <summary>
        /// 获取 一言
        /// </summary>
        /// <returns>(内容,来源)</returns>
        public static (string Content, string From) GetHitokoto()
        {
            WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 };
            var jsonContent = webClient.DownloadString("https://v1.hitokoto.cn/?c=a&c=c");
            var json = JsonConvert.DeserializeObject<dynamic>(jsonContent);
            return (json.hitokoto, "—— " + json.from);
        }

        public static BitmapImage GetRandomImage()
        {
            BitmapImage image = new BitmapImage();

            if (Directory.Exists("bg"))
            {
                var files = Directory.GetFiles("bg");
                if (files.Length > 0)
                {
                    try
                    {
                        var path = Environment.CurrentDirectory + "\\" + files[new Random().Next(files.Length)];
                        image = new BitmapImage(new Uri(path));
                        return image;
                    }
                    catch { }
                }
            }

            using (MemoryStream stream = new MemoryStream())
            {
                Properties.Resources.bg.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                image = bitmapImage;
            }
            return image;
        }
    }
}