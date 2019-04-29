using System;
using System.IO;
using System.Windows.Forms;

namespace NIMDemo.Audio
{
    public partial class AudioForm : Form
    {
        Tools.OutputInfoText _outputTools;
        NIMAudio.NIMAudioType _audioType;
        public AudioForm()
        {
            InitializeComponent();
            this.Load += AudioForm_Load;
            _outputTools = new Tools.OutputInfoText(richTextBox1);
            comboBox1.SelectedIndex = 0;
        }

        private void AudioForm_Load(object sender, EventArgs e)
        {
            if(!NIMAudio.AudioAPI.InitModule(""))
            {
                MessageBox.Show("语音模块初始化错误");
            }
        }

        private void getDevicesBtn_Click(object sender, EventArgs e)
        {
            NIMAudio.AudioAPI.RegEnumDevicesCb(OnEnumDevices);
            NIMAudio.AudioAPI.EnumCaptureDevices();
        }

        private void OnEnumDevices(int resCode, string deviecs)
        {
            _outputTools.ShowInfo("获取设备:\r\nresCode:{0}\r\ndevices:{1}", resCode, deviecs);
        }

        private void captureBtn_Click(object sender, EventArgs e)
        {
            NIMAudio.AudioAPI.StartCapture("", "", _audioType);
            NIMAudio.AudioAPI.RegStartAudioCaptureCb(OnCaptureStarted);
        }

        private void OnCaptureStarted(int resCode)
        {
            _outputTools.ShowInfo("开始录制:{0}", resCode); ;
        }

        private void stopCaptureBtn_Click(object sender, EventArgs e)
        {
            NIMAudio.AudioAPI.RegStopAudioCaptureCb(OnCaptureStopped);
            NIMAudio.AudioAPI.StopCapture();
        }

        private void OnCaptureStopped(int resCode, string call_id, string res_id, string file_path, string file_ext, int file_size, int audio_duration)
        {
            _outputTools.ShowInfo("录制结束:{0}\r\n file_path:{1}\r\n ext:{2}\r\n size:{3}\r\n duration:{4}",
                resCode, file_path, file_ext, file_size, audio_duration);
        }

        private void selectFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            string ext = "*.*";
            if (_audioType == NIMAudio.NIMAudioType.kNIMAudioAAC)
                ext = "*.aac";
            if (_audioType == NIMAudio.NIMAudioType.kNIMAudioAMR)
                ext = "*.amr";
            dialog.Filter = "音频|" + ext;
            dialog.ShowDialog();
            if(dialog.FileName != null)
            {
                filePathTxt.Text = dialog.FileName;
            }
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(filePathTxt.Text) && File.Exists(filePathTxt.Text))
            {
                NIMAudio.AudioAPI.RegStartPlayCb(OnPlayStarted);
                NIMAudio.AudioAPI.PlayAudio(filePathTxt.Text, "", "", _audioType);
            }
        }

        private void OnPlayStarted(int resCode, string filePath, string callId, string resId)
        {
            _outputTools.ShowInfo("播放音频:{0}\r\n path:{1}", resCode, filePath);
        }

        private void stopPlayBtn_Click(object sender, EventArgs e)
        {
            NIMAudio.AudioAPI.RegStopPlayCb(OnPlayStopped);
            NIMAudio.AudioAPI.StopPlayAudio();
        }

        private void OnPlayStopped(int resCode, string filePath, string callId, string resId)
        {
            _outputTools.ShowInfo("停止播放:{0}\r\n path:{1}", resCode, filePath);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _audioType = (NIMAudio.NIMAudioType)comboBox1.SelectedIndex;
        }
    }
}
