using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using GameFramework;
using GameFramework.DataNode;
using StarForce;

namespace Xiu
{
    public static class EncryptionUtility
    { 
        // public static byte[] key = Encoding.UTF8.GetBytes("your-secret-key-here"); // 密钥，必须是 16/24/32 字节长度
        // public static byte[] iv = EncryptionUtility.GenerateRandomIV(); // 初始化向量，必须是 16 字节长度
        public static Aes aesAlg = Aes.Create();
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="numBytes"></param>
        /// <returns></returns>
        public static byte[] GenerateRandomBytes(int numBytes)
        {
            var randomBytes = new byte[numBytes];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            return randomBytes;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="dataToEncrypt">加密的字节流</param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] dataToEncrypt)
        {  
            IDataNode passNode = GameEntry.DataNode.GetNode("Game.SecretKey");
            if (passNode == null)
            {
                return null;
            }
            UnityGameFramework.Runtime.VarString password = passNode.GetData<UnityGameFramework.Runtime.VarString>();
            aesAlg.Key = Encoding.UTF8.GetBytes((string)password.GetValue());
            aesAlg.IV = Encoding.UTF8.GetBytes((string)password.GetValue());
            // string key = "e0j|r+g*F@e5\"r`B";
            // aesAlg.Key = Encoding.UTF8.GetBytes((string)key);
            // aesAlg.IV = Encoding.UTF8.GetBytes((string)key);
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.ISO10126;
            
            byte[] encryptedData = null;

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cs.FlushFinalBlock();
                    encryptedData = ms.ToArray();
                }
            }

            return encryptedData;
        }
 
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="dataToDecrypt">需要解密的字节流</param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] dataToDecrypt)
        {
            // byte[] decryptedData = null;

            // using (MemoryStream ms = new MemoryStream())
            // {
            //     try
            //     {
            //         using (CryptoStream cs = new CryptoStream(ms, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
            //         {
            //             cs.Write(dataToDecrypt, 0, dataToDecrypt.Length);
            //             cs.FlushFinalBlock();               
            //         }
            //     }
            //     catch (Exception ex)
            //     {
            //         Console.WriteLine("Error: " + ex.Message);
            //         // 可以在这里添加更多的错误处理逻辑
            //     }
            //     // 此时 MemoryStream 中已经包含了解密后的数据
            //     // 将 MemoryStream 的位置重置为开始，准备读取全部数据
            //     ms.Position = 0;
            //     decryptedData = ms.ToArray();
            // }
            // return decryptedData;
            try
            {
                ICryptoTransform cTransform = aesAlg.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
                return resultArray;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // 可以在这里添加更多的错误处理逻辑
            }
            return null;
        }
    }
    
 
}
