using NIM.Messagelog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using NIMDemo.Helper;

namespace NIMDemo
{
    public partial class MessageExportForm : Form
    {
        public MessageExportForm()
        {
            InitializeComponent();
        }
        
        private string ImportBackupFromRemoteUnPackageCallback(string file_path)
        {
            //解压
            DirectoryInfo directory_info = Directory.GetParent(file_path);
            string unpackage_path = "";
            try
            {
                string out_folder = directory_info.FullName;
                List<string> files;
                string[] args = { file_path, out_folder };
                ZipHelper.UnZip(args, out files) ;
                if(files.Count>0)
                {
                    unpackage_path = files[0];
                }
            }
            catch
            {
                unpackage_path = null;
            }
           
            return unpackage_path;
        }
        private string ImportBackupFromRemoteDecryptCallback(string filePath, string encryptKey)
        {
            //注意 目前 加密解密仅提供C#端demo示例,与其他端demo暂时还未互通
            //对导入文件进行解密
            DirectoryInfo directory_info = Directory.GetParent(filePath);
            string decrypt_path = directory_info.FullName + @"\temp_import_decrypt.zip";
            CryptoHelper.DecryptFile(filePath, decrypt_path, encryptKey);
            return decrypt_path;
        }

        private void LogsBackupCompleteCallback(LogsBackupRemoteOperate operate, LogsBackupRemoteState state)
        {
            DemoTrace.WriteLine(string.Format("operate :{0} progress{1}", operate.ToString(), state));
        }

        private void LogsBackupProgressCallback(LogsBackupRemoteOperate operate, float progress)
        {
            DemoTrace.WriteLine(string.Format("operate :{0} progress{1}", operate.ToString(), progress));
        }

        private string ExportBackupToRemotePackageCallback(string file_path)
        {
            DirectoryInfo directory_info = Directory.GetParent(file_path);
            string package_path = directory_info.FullName + @"\temp_export_packet.zip";
            try
            {
                ZipHelper.ZipFile(file_path, package_path, 6, 1024);
            }
            catch
            {
                package_path = null;
            }
            return package_path;
        }

        private string ExportBackupToRemoteEncryptCallback(string filePath, string encryptKey)
        {
            //注意 目前 加密解密仅提供C#端示例,与其他端暂时还未互通
            DirectoryInfo directoryInfo = Directory.GetParent(filePath);
            string encryptPath = directoryInfo.FullName + @"\temp_import_encrypt.aes";
            CryptoHelper.EncryptFile(filePath, encryptPath, encryptKey);
            return encryptPath;
        }

        private bool ExportBackupToRemoteLogFiterCallback(string msg)
        {
            bool ret = true;
            return ret;
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            NIMLogsBackupImportInfo import_info = new NIMLogsBackupImportInfo();
            import_info.UnPackageCallback_ = ImportBackupFromRemoteUnPackageCallback;
            import_info.RemoteDecryptCallback_ = ImportBackupFromRemoteDecryptCallback;
            import_info.CompleteCallback_ = LogsBackupCompleteCallback;
            import_info.ProgressCallback_ = LogsBackupProgressCallback;
            MessagelogAPI.ImportBackupFromRemote(import_info);
        }

      

        private void BtnExport_Click(object sender, EventArgs e)
        {
            NIMLogsBackupExportInfo export_info = new NIMLogsBackupExportInfo();
            export_info.ToRemotePackageCallback_ = ExportBackupToRemotePackageCallback;
            export_info.ToRemoteEncryptCallback_ = ExportBackupToRemoteEncryptCallback;
            export_info.CompleteCallback_ = LogsBackupCompleteCallback;
            export_info.ProgressCallback_ = LogsBackupProgressCallback;
            string encrypt_key_ = System.Guid.NewGuid().ToString("N");//32位
            export_info.ToRemoteLogFiter_ = ExportBackupToRemoteLogFiterCallback;

            MessagelogAPI.ExportBackupToRemote(export_info,encrypt_key_);
        }

        
    }



  
}
