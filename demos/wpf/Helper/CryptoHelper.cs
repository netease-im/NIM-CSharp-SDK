using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NIMDemo.Helper
{
    class CryptoHelper
    {
        private const long EnBufferLen = 1024;
        private const long DeBufferLen = 1024 + 16;
        private const string iv = "0123456789012345";

        public static byte[] EncryptByteArray(byte[] src, string encrypt_key)
        {
            byte[] key = Encoding.ASCII.GetBytes(encrypt_key);
            byte[] IV = Encoding.ASCII.GetBytes(iv);
            AesEngine aesEngine = new AesEngine();
            CbcBlockCipher cbc = new CbcBlockCipher(aesEngine);
            PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(cbc);
            KeyParameter keySpec = new KeyParameter(key);
            ICipherParameters parameters = new ParametersWithIV(keySpec, IV);
            cipher.Init(true, parameters);
            // process ciphering
            byte[] output = new byte[cipher.GetOutputSize(src.Length)];
            int bytesProcessed1 = cipher.ProcessBytes(src, 0, src.Length, output, 0);
            int bytesProcessed2 = cipher.DoFinal(output, bytesProcessed1);
            byte[] result = new byte[bytesProcessed1 + bytesProcessed2];
            Array.Copy(output, result, result.Length);
            return result;
        }

        public static byte[] DecryptByteArray(byte[] src, string encrypt_key)
        {
            byte[] key = Encoding.ASCII.GetBytes(encrypt_key);
            byte[] IV = Encoding.ASCII.GetBytes(iv);
            AesEngine aesEngine = new AesEngine();
            CbcBlockCipher cbc = new CbcBlockCipher(aesEngine);
            PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(cbc);
            KeyParameter keySpec = new KeyParameter(key);
            ICipherParameters parameters = new ParametersWithIV(keySpec, IV);
            cipher.Init(false, parameters);
            // process ciphering
            byte[] output = new byte[cipher.GetOutputSize(src.Length)];
            int bytesProcessed1 = cipher.ProcessBytes(src, 0, src.Length, output, 0);
            int bytesProcessed2 = cipher.DoFinal(output, bytesProcessed1);
            byte[] result = new byte[bytesProcessed1 + bytesProcessed2];
            Array.Copy(output, result, result.Length);
            return result;
        }


        public static void EncryptFile(string inFile, string outFile, string in_key)
        {
            byte[] key = Encoding.ASCII.GetBytes(in_key);
            byte[] IV = Encoding.ASCII.GetBytes(iv);
            AesEngine aesEngine = new AesEngine();
            CbcBlockCipher cbc = new CbcBlockCipher(aesEngine);
            PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(cbc);
            KeyParameter keySpec = new KeyParameter(key);
            ICipherParameters parameters = new ParametersWithIV(keySpec, IV);
            cipher.Init(true, parameters);

            using (FileStream fin = File.OpenRead(inFile), fout = File.Open(outFile, FileMode.Create))
            {
                BinaryReader br = new BinaryReader(fin);
                BinaryWriter bw = new BinaryWriter(fout);
                int block = 0;
                //初始化缓冲区
                byte[] decryptor_data = null;
                if (fin.Length > EnBufferLen)
                {
                    decryptor_data = new byte[EnBufferLen];
                }
                else
                {
                    decryptor_data = new byte[fin.Length];
                }

                while ((block = br.Read(decryptor_data, 0, decryptor_data.Length)) > 0)
                {
                    if (decryptor_data.Length != block)
                    {
                        byte[] temp = new byte[block];
                        Buffer.BlockCopy(decryptor_data, 0, temp, 0, block);
                        decryptor_data = temp;
                    }

                    // 开始处理编码
                    byte[]  encrypted = new byte[cipher.GetOutputSize(decryptor_data.Length)];
                    int bytesProcessed1 = cipher.ProcessBytes(decryptor_data, 0, decryptor_data.Length, encrypted, 0);
                        
                    //保存进文件
                    int write_len = encrypted.Length;
                    bw.Write(encrypted, 0, bytesProcessed1);

                }
                byte[] output = new byte[cipher.GetOutputSize(decryptor_data.Length)];
                int bytesProcessed2 = cipher.DoFinal(output, 0);
                if(bytesProcessed2>0)
                {
                    bw.Write(output, 0, bytesProcessed2);
                }

                br.Close();
                bw.Close();

            }
        }


        public static void DecryptFile(string inFile, string outFile, string in_key)
        {
            byte[] key = Encoding.ASCII.GetBytes(in_key);
            byte[] IV = Encoding.ASCII.GetBytes(iv);
            AesEngine aesEngine = new AesEngine();
            CbcBlockCipher cbc = new CbcBlockCipher(aesEngine);
            PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(cbc);
            KeyParameter keySpec = new KeyParameter(key);
            ICipherParameters parameters = new ParametersWithIV(keySpec, IV);
            cipher.Init(false, parameters);

            using (FileStream fin = File.OpenRead(inFile), fout = File.Open(outFile, FileMode.Create))
            {
                BinaryReader br = new BinaryReader(fin);
                BinaryWriter bw = new BinaryWriter(fout);
                int block = 0;
                //初始化缓冲区
                byte[] encrypted = null;
                if (fin.Length > (DeBufferLen))
                {
                    encrypted = new byte[DeBufferLen];
                }
                else
                {
                    encrypted = new byte[fin.Length];
                }

                //解密
                int total_processed = 0;
                while ((block = br.Read(encrypted, 0, encrypted.Length)) > 0)
                {
                    try
                    {
                        if (encrypted.Length != block)
                        {
                            byte[] temp = new byte[block];
                            Buffer.BlockCopy(encrypted, 0, temp, 0, block);
                            encrypted = temp;
                        }

                        // 开始处理编码
                        byte[] de_data = new byte[cipher.GetOutputSize(encrypted.Length)];
                        int bytesProcessed1 = cipher.ProcessBytes(encrypted, 0, encrypted.Length, de_data, 0);
                        total_processed += bytesProcessed1;
                        //保存进文件
                        if (bytesProcessed1 > 0)
                            bw.Write(de_data, 0, bytesProcessed1);

                        //byte[] de_data = DecryptByteArrayEx(encrypted, encrypt);
                        //bw.Write(de_data, 0, de_data.Length);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                    }
                }

                 byte[] output = new byte[DeBufferLen];
                 int bytesProcessed2 = cipher.DoFinal(output, 0);
                 if (bytesProcessed2 > 0)
                 {
                     bw.Write(output, 0, bytesProcessed2);
                 }
                bw.Close();
                br.Close();
            }
        }


    }
}

