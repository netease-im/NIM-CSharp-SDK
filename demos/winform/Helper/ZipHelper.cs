using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NIMDemo.Helper
{
    public class ZipHelper
    {
        /// <summary>
        /// 压缩单个文件
        /// </summary>
        /// <param name="FileToZip">需要压缩的文件</param>
        /// <param name="ZipedFile">压缩后的文件(初始时不存在)</param>
        /// <param name="CompressionLevel">压缩级别</param>
        /// <param name="BlockSize">每次写入的内存大小</param>
        public static  void ZipFile(string FileToZip, string ZipedFile, int CompressionLevel, int BlockSize)
        {
            // 如果文件没有找到，则报错
            if (!File.Exists(FileToZip))
            {
                throw new FileNotFoundException("The specified file" + FileToZip + "could not be found. Zipping aborderd.");
            }

            string fileName = Path.GetFileName(FileToZip);      // 文件名
            // 防止中文名乱码
            Encoding gbk = Encoding.GetEncoding("gbk");      
            ZipConstants.DefaultCodePage = gbk.CodePage;
            
            FileStream streamToZip = new FileStream(FileToZip, FileMode.Open, FileAccess.Read); // 读取需压缩的文件
            // 创建压缩文件
            FileStream ZipFile = File.Create(ZipedFile);             
            ZipOutputStream ZipStream = new ZipOutputStream(ZipFile);
            //这里必须将zip64开关关掉，不然PC demo将会解压缩失败
            ZipStream.UseZip64 = UseZip64.Off;
            ZipEntry ZipEntry = new ZipEntry(fileName);
            ZipStream.PutNextEntry(ZipEntry);
            ZipStream.SetLevel(CompressionLevel);             // 设置压缩级别

            byte[] buffer = new byte[BlockSize];
            int size = streamToZip.Read(buffer, 0, buffer.Length);    // 每次读入指定大小
            ZipStream.Write(buffer, 0, size);

            try
            {
                while (size < streamToZip.Length)       // 保证文件被全部写入
                {
                    int sizeRead = streamToZip.Read(buffer, 0, buffer.Length);
                    ZipStream.Write(buffer, 0, sizeRead);
                    size += sizeRead;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            ZipStream.Finish();
            ZipStream.Close();
            streamToZip.Close();
        }


        /// <summary>
        /// 解压ZIP
        /// </summary>
        /// <param name="args">要解压的ZIP文件和解压后保存到的文件夹</param>
        /// <param name="files">zip文件下的文件路径集合</param>
        public static void UnZip(string[] args,out List<string> files)
        {
            ZipInputStream s = new ZipInputStream(File.OpenRead(args[0]));  // 获取要解压的ZIP文件
            files = new List<string>();
            Encoding gbk = Encoding.GetEncoding("gbk");      // 防止中文名乱码
            ZipConstants.DefaultCodePage = gbk.CodePage;

            ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                string directoryName = args[1];   // 获取解压后保存到的文件夹
                string fileName = Path.GetFileName(theEntry.Name);

                //生成解压目录
                Directory.CreateDirectory(directoryName);
                
                if (fileName != String.Empty)
                {
                    //解压文件到指定的目录
                    string tempFileName = args[1] + theEntry.Name;
                    FileStream streamWriter = File.Create(tempFileName);
                    files.Add(tempFileName);
                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)       //  循环写入单个文件
                    {
                        size = s.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);     // 每次写入的文件大小
                        }
                        else
                        {
                            break;
                        }
                    }

                    streamWriter.Close();
                }
            }
            s.Close();
        }
    }
}
