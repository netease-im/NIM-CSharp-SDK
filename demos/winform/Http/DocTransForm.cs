using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NIM.DocTransition;

namespace NIMDemo.Http
{
    public partial class DocTransForm : Form
    {
        Tools.OutputInfoText _outputTxt;

        public DocTransForm()
        {
            InitializeComponent();
            _outputTxt = new Tools.OutputInfoText(richTextBox1);
        }

        private void selectFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if(!string.IsNullOrEmpty(dialog.FileName))
            {
                filePathBox.Text = dialog.FileName;
            }
        }

        private void OnUploadProgress(long uploaded_size, long file_size, string json_extension, IntPtr user_data)
        {
            _outputTxt.ShowInfo(string.Format("uploaded:{0} / total:{1}", uploaded_size, file_size));
        }

        private void OnUploadCompleted(int rescode, string url, string json_extension, IntPtr user_data)
        {
            _outputTxt.ShowInfo(string.Format("rescode:{0},url:{1},ext:{2}", rescode, url, json_extension));
            if (rescode != 200)
                _outputTxt.ShowInfo("转换失败");
            else
            {
                if (!string.IsNullOrEmpty(json_extension))
                {
                    var jObj = Newtonsoft.Json.Linq.JObject.Parse(json_extension);
                    var token = jObj.SelectToken("res_id");
                    var resId = token.ToObject<string>();
                    Action action = () => 
                    {
                        transIDtxtbox.Text = resId;
                        textBox1.Text = url;
                    };
                    transIDtxtbox.Invoke(action);
                }
            }
        }

        private void submitFileBtn_Click(object sender, EventArgs e)
        {
            string filePath = filePathBox.Text;
            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
                return;
            NIM.Nos.DocTransitionParams param = new NIM.Nos.DocTransitionParams();
            param.UploadType = NIM.Nos.NIMNosUploadType.kNIMNosUploadTypeDocTrans;
            var index = filePath.LastIndexOf('.');
            if (index < filePath.Length)
            {
                var suffix = filePath.Substring(index + 1);
                if (string.Compare(suffix, "pdf", true) == 0)
                    param.SourceType = NIM.DocTransition.NIMDocTranscodingFileType.kNIMDocTranscodingFileTypePDF;
                else if (string.Compare(suffix, "ppt", true) == 0)
                    param.SourceType = NIM.DocTransition.NIMDocTranscodingFileType.kNIMDocTranscodingFileTypePPT;
                else if (string.Compare(suffix, "pptx", true) == 0)
                    param.SourceType = NIM.DocTransition.NIMDocTranscodingFileType.kNIMDocTranscodingFileTypePPTX;
                else
                {
                    MessageBox.Show("不支持的文件类型,请选择pdf/ppt/pptx 文件");
                    return;
                }
            }
            NIM.DocTransition.DocTransApi.RegisterNotifyCallback(OnTransDocCompleted);
            param.PictureType = NIM.DocTransition.NIMDocTranscodingImageType.kNIMDocTranscodingImageTypeJPG;
            param.TransitionFileName = System.IO.Path.GetFileName(filePath);
            param.DocTransitionExt = "DocTransForm";
            
            param.TaskID = Guid.NewGuid().ToString();
            _outputTxt.ShowInfo(param.Serialize());
            NIM.Nos.NosAPI.UploadEx(filePath, param,
                OnUploadCompleted, IntPtr.Zero,
                OnUploadProgress, IntPtr.Zero,
                null, IntPtr.Zero,
                null, IntPtr.Zero);
        }

        private void OnTransDocCompleted(int code, DocTransInfo info)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NIM.DocTransition.DocTransApi.GetTransitionInfo(transIDtxtbox.Text, OnGetTransInfo);
        }

        private void OnGetTransInfo(int code, DocTransInfo info)
        {
            _outputTxt.ShowInfo(info.Dump());
            var sourceUrl = NIM.DocTransition.DocTransApi.GetSourceFileUrl(info.UrlPrefix, NIMDocTranscodingFileType.kNIMDocTranscodingFileTypePPT);
            var DestUrl = NIM.DocTransition.DocTransApi.GetPageUrl(info.UrlPrefix, info.DestImageType,NIMDocTranscodingQuality.kNIMDocTranscodingQualityHigh, info.PageNum);
            _outputTxt.ShowInfo(string.Format("源文档:{0}\r\n,图片url{1}", sourceUrl, DestUrl));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NIM.DocTransition.DocTransApi.GetTransitionInfoList(transIDtxtbox.Text, 30,OnGetTransList);
        }

        private void OnGetTransList(int code, DocTransInfoList list)
        {
            _outputTxt.ShowInfo(list.Dump());
        }
    }
}
