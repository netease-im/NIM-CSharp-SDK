using NIM.Nos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NIMDemo.Http
{
    public partial class HttpForm : Form
    {
        public UploadData NosUploadData { get; set; }
        InvokeActionWrapper _invokeWrapper;
        public HttpForm()
        {
            InitializeComponent();
            SetDataBindings();
            _invokeWrapper = new InvokeActionWrapper(this);
        }

        void SetDataBindings()
        {
            NosUploadData = new UploadData();
            this.uploadedFilePathTxt.DataBindings.Add("Text", NosUploadData, "FilePath");
            this.uploadPrgLabel.DataBindings.Add("Text", NosUploadData, "UploadedSize");
            this.uploadRetLabel.DataBindings.Add("Text", NosUploadData, "ResultCode");
            this.uploadSpeedLabel.DataBindings.Add("Text", NosUploadData, "Speed");
            this.totalSizeLabel.DataBindings.Add("Text", NosUploadData, "TotalSize");
            this.avgSpeedLabel.DataBindings.Add("Text",NosUploadData, "AvgSpeed");
            this.actualSizeLabel.DataBindings.Add("Text", NosUploadData, "ActualSize");
            this.downloadUrlTxt.DataBindings.Add("Text",NosUploadData, "Url");
        }

        private void selectFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if(!string.IsNullOrEmpty(dialog.FileName))
            {
                NosUploadData.FilePath = dialog.FileName;
            }
        }

        private void uploadBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(NosUploadData.FilePath))
            {
                NosUploadData.ActualSize = 0;
                NosUploadData.AvgSpeed = 0;
                NosUploadData.ResultCode = 0;
                NosUploadData.Speed = 0;
                NosUploadData.TotalSize = 0;
                NosUploadData.UploadedSize = 0;
                NosUploadData.Url = "";
                NIM.Nos.NosAPI.UploadEx(NosUploadData.FilePath,null, ResultCb, Marshal.StringToHGlobalAnsi("UploadResult"),
               ReportPrg, Marshal.StringToHGlobalAnsi("UploadProgress"),
               ReportSpeed, Marshal.StringToHGlobalAnsi("UploadSpeed"),
               ReportInfo, Marshal.StringToHGlobalAnsi("UploadInfo"));
            }

        }

        private void ReportInfo(long actual_upload_size, long upload_speed, string json_extension, IntPtr user_data)
        {
            _invokeWrapper.InvokeAction(() => 
            {
                NosUploadData.ActualSize = actual_upload_size;
                NosUploadData.AvgSpeed = upload_speed;
            });
            
        }

        private void ReportPrg(long uploaded_size, long file_size, string json_extension, IntPtr user_data)
        {
            _invokeWrapper.InvokeAction(() => 
            {
                NosUploadData.UploadedSize = uploaded_size;
                NosUploadData.TotalSize = file_size;
            });
           
        }

        private void ReportSpeed(long upload_speed, string json_extension, IntPtr user_data)
        {
            _invokeWrapper.InvokeAction(() =>
            {
                if (upload_speed > 0)
                    NosUploadData.Speed = upload_speed;
            });
            
        }

        private void ResultCb(int rescode, string url, string json_extension, IntPtr user_data)
        {
            _invokeWrapper.InvokeAction(() =>
            {
                NosUploadData.ResultCode = rescode;
                NosUploadData.Url = url;
                downloadBtn.Enabled = (rescode == 200 && !string.IsNullOrEmpty(url));
            });
            
        }

        private void downloadBtn_Click(object sender, EventArgs e)
        {
            NosAPI.DownloadEx(NosUploadData.Url,null, ReportDownloadResult, Marshal.StringToCoTaskMemAnsi(""),
                ReportDownloadPrg, Marshal.StringToCoTaskMemAnsi(""),
                ReportDownloadSpeed, Marshal.StringToCoTaskMemAnsi(""),
                ReportDownloadInfo, Marshal.StringToCoTaskMemAnsi(""));
        }

        private void ReportDownloadInfo(long actual_download_size, long download_speed, string json_extension, IntPtr user_data)
        {
            
        }

        private void ReportDownloadSpeed(long download_speed, string json_extension, IntPtr user_data)
        {
            _invokeWrapper.InvokeAction(() =>
            {
                if (download_speed > 0)
                    downloadSpeedLabel.Text = download_speed.ToString();
            });
        }

        private void ReportDownloadPrg(long downloaded_size, long file_size, string json_extension, IntPtr user_data)
        {
            _invokeWrapper.InvokeAction(() =>
            {
                downloadPrgLabel.Text = string.Format("{0}/{1}", downloaded_size, file_size);
            });
        }

        private void ReportDownloadResult(int rescode, string file_path, string call_id, string res_id, string json_extension, IntPtr user_data)
        {
            _invokeWrapper.InvokeAction(() => 
            {
                downloadResultLabel.Text = string.Format("{0} {1}", rescode, file_path);
            });
        }
    }

    public class UploadData:INotifyPropertyChanged
    {
        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if(_filePath != value)
                {
                    _filePath = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("FilePath"));
                }
            }
        }

        private string _url;
        public string Url
        {
            get { return _url; }
            set
            {
                if(_url != value)
                {
                    _url = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Url"));
                }
            }
        }

        private int _retCode;
        public int ResultCode
        {
            get { return _retCode; }
            set
            {
                if(_retCode != value)
                {
                    _retCode = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("ResultCode"));
                }
            }
        }

        private long _uploadedSize;
        public long UploadedSize
        {
            get { return _uploadedSize; }
            set
            {
                if(_uploadedSize !=value)
                {
                    _uploadedSize = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("UploadedSize"));
                }
            }
        }

        private long _totalSize;
        public long TotalSize
        {
            get { return _totalSize; }
            set
            {
                if(_totalSize != value)
                {
                    _totalSize = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("TotalSize"));
                }
            }
        }

        private long _speed;
        public long Speed
        {
            get { return _speed; }
            set
            {
                if (_speed != value)
                {
                    _speed = value;

                    PropertyChanged(this, new PropertyChangedEventArgs("Speed"));
                }
            }
        }

        private long _actualSize;
        public long ActualSize
        {
            get { return _actualSize; }
            set
            {
                if (_actualSize != value)
                {
                    _actualSize = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("ActualSize"));
                }
            }
        }

        private long _avgSpeed;
        public long AvgSpeed
        {
            get { return _avgSpeed; }
            set
            {
                if (_avgSpeed != value)
                {
                    _avgSpeed = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("ActualSize"));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
