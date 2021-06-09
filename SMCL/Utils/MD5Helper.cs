using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SMCL.Utils
{
    public static class MD5Helper
    {
        private static MD5 md5 = MD5.Create();

        public static string GetMD5String(byte[] bytes)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                stringBuilder.Append($"{bytes[i]:X2}");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 计算文件的 MD5 值
        /// </summary>
        /// <param name="path"> 路径 </param>
        /// <returns> MD5 </returns>
        public static string GetMD5String(string path)
        {
            var bytes = md5.ComputeHash(new FileStream(path, FileMode.Open));
            return GetMD5String(bytes);
        }
    }
}