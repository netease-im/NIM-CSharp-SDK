using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace NIMDemo
{

    public class Account
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }
    }

    public class AccountCollection
    {
        [JsonProperty("LastIndex")]
        public int LastIndex { get; set; }

        [JsonProperty("List")]
        public List<Account> List { get; set; }

        public AccountCollection()
        {
            LastIndex = -1;
            List = new List<Account>();
        }

        public int IndexOf(string name)
        {
            if (List == null || !List.Any())
                return -1;
            for (int i = 0; i < List.Count; i++)
            {
                if (List[i].Name == name)
                    return i;
            }
            return -1;
        }

        public Account Find(string name)
        {
            if (List == null || !List.Any())
                return null;
            return List.FirstOrDefault(c => c.Name == name);
        }
    }

    class AccountManager
    {
        private const string SettingFilePath = "Accounts.json";
        private const string DESKey = "nim.2016";
        private const string DESIV = "2016.nim";

        public static AccountCollection GetAccountList()
        {
            var path = System.IO.Path.Combine(System.Environment.CurrentDirectory, SettingFilePath);
            if (!File.Exists(path))
                return null;
            var obj = LoadData(path);
            if (obj != null && obj.List != null)
            {
                foreach (var item in obj.List)
                {
                    item.Password = DESDecrypt(item.Password, DESKey, DESIV);
                }
            }
            return obj;
        }

        public static void SaveLoginAccounts(AccountCollection collection)
        {
            var path = System.IO.Path.Combine(System.Environment.CurrentDirectory, SettingFilePath);
            foreach (var item in collection.List)
                item.Password = DESEncrypt(item.Password, DESKey, DESIV);
            SaveData(path, collection);
        }

        private static void SaveData(string filePath, AccountCollection sourceObj)
        {
            if (!string.IsNullOrEmpty(filePath) && sourceObj != null)
            {
                var json = NimUtility.Json.JsonParser.SerializeWithIndented(sourceObj);
                File.WriteAllText(filePath, json);
            }
        }

        private static AccountCollection LoadData(string filePath)
        {
            AccountCollection result = null;
            if (File.Exists(filePath))
            {
                var content = File.ReadAllText(filePath);
                result = NimUtility.Json.JsonParser.Deserialize<AccountCollection>(content);
            }

            return result;
        }

        #region DES加密解密
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="data">加密数据</param>
        /// <param name="key">8位字符的密钥字符串</param>
        /// <param name="iv">8位字符的初始化向量字符串</param>
        /// <returns></returns>
        public static string DESEncrypt(string data, string key, string iv)
        {
            byte[] byKey = Encoding.ASCII.GetBytes(key);
            byte[] byIV = Encoding.ASCII.GetBytes(iv);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="data">解密数据</param>
        /// <param name="key">8位字符的密钥字符串(需要和加密时相同)</param>
        /// <param name="iv">8位字符的初始化向量字符串(需要和加密时相同)</param>
        /// <returns></returns>
        public static string DESDecrypt(string data, string key, string iv)
        {
            byte[] byKey = Encoding.ASCII.GetBytes(key);
            byte[] byIV = Encoding.ASCII.GetBytes(iv);

            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }
        #endregion

    }
}
