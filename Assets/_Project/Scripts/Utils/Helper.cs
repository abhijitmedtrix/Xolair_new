using UnityEngine;
using System;
using System.IO;
using System.Text;

namespace App.Utils
{
    public static class Helper
    {
        /// <summary>
        /// Formats seconds to mm:ss
        /// </summary>
        /// <returns>The to minutes.</returns>
        /// <param name="seconds">Seconds.</param>
        public static string SecondsToMinutes(float seconds)
        {
            string special = "";
            if (seconds < 0)
            {
                special = "-";
            }

            TimeSpan t = TimeSpan.FromSeconds(Mathf.Abs(seconds));
            return string.Format("{0}{1:D2}:{2:D2}", special, t.Minutes, t.Seconds);
        }

        public static string GetFileNameWithExtension(string path)
        {
            return Path.GetFileName(path);
        }

        public static string FormatLocalPathToUrl(string path)
        {
#if UNITY_EDITOR
            path = "file://" + path;
#endif
            /*
            #elif UNITY_ANDROID
            path = "jar:file://"+ path;
            #elif UNITY_IOS
            path = "file:" + path;
            #else
            // Desktop (Mac OS or Windows)
            path = "file:"+ path;
            #endif
            */
            return path;
        }

        public static string FormatToUrl(string path)
        {
            StringBuilder sb = new StringBuilder(path);
            
            if (!path.Contains("http"))
            {
                int index = path.IndexOf("//");
                if (index == -1)
                {
                    index = path.IndexOf("www");
                    if (index == -1)
                    {
                        index = 0;
                    }
                }
                else
                {
                    sb.Remove(index, 2);
                }
                
                // insert http://
                sb.Insert(0, "http://");
            }
            return sb.ToString();
        }

        public static string GetDataPath()
        {
#if UNITY_EDITOR
            return Application.streamingAssetsPath;
#else
            return Application.persistentDataPath;
#endif
        }

        public static string StreamingAssetPath()
        {
            string path;
#if UNITY_EDITOR
            path = Application.dataPath + "/StreamingAssets";
#elif UNITY_ANDROID
            path = Application.dataPath + "!/assets/";
#elif UNITY_IOS
            path = Application.dataPath + "/Raw";
#else
            // Desktop (Mac OS or Windows)
            path = Application.dataPath + "/StreamingAssets";
#endif

            return path;
        }

        public static string StreamingAssetPathAsURL()
        {
            string path;
#if UNITY_EDITOR
            path = "file://" + Application.dataPath + "/StreamingAssets";
#elif UNITY_ANDROID
            path = "jar:file://"+ Application.dataPath + "!/assets/";
#elif UNITY_IOS
            path = "file:" + Application.dataPath + "/Raw";
#else
            // Desktop (Mac OS or Windows)
            path = "file:"+ Application.dataPath + "/StreamingAssets";
#endif

            return path;
        }

        public static string Md5Sum(byte[] bytes)
        {
            // encrypt bytes
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(bytes);

            // Convert the encrypted bytes back to a string (base 16)
            string hashString = "";

            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
            }

            return hashString.PadLeft(32, '0');
        }
    }
}
