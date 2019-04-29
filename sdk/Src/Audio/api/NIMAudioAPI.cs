/** @file NIMAudioAPI.cs
  * @brief NIM 提供的语音录制和播放接口
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author Harrison
  * @date 2015/12/8
  */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using NIM;

namespace NIMAudio
{
    /// <summary>
    /// audio模块调用返回错误码
    /// </summary>
    public enum NIMAudioResCode
    {
        /// <summary>
        /// 成功
        /// </summary>
	    kNIMAudioSuccess = 200,
        /// <summary>
        /// 操作失败
        /// </summary>
        kNIMAudioFailed = 100,
        /// <summary>
        /// 未初始化或未成功初始化
        /// </summary>
        kNIMAudioUninitError = 101,
        /// <summary>
        /// 正在播放中，操作失败
        /// </summary>
        kNIMAudioClientPlaying = 102,
        /// <summary>
        /// 正在采集中，操作失败
        /// </summary>
        kNIMAudioClientCapturing = 103,
        /// <summary>
        /// 采集设备初始化失败（e.g. 找不到mic设备）
        /// </summary>
        kNIMAudioCaptureDeviceInitError = 104,
        /// <summary>
        /// 采集或播放对象或操作不存在
        /// </summary>
        kNIMAudioClientNotExist = 105,
        /// <summary>
        /// 线程出错退出，需要重新初始化语音模块
        /// </summary>
        kNIMAudioThreadError = 300,
    }

    /// <summary>
    /// 音频编码方式
    /// </summary>
    public enum NIMAudioType
    {
        /// <summary>
        /// 音频AAC编码
        /// </summary>
	    kNIMAudioAAC = 0,
        /// <summary>
        /// 音频AMR编码（暂不支持）
        /// </summary>
	    kNIMAudioAMR = 1,
    }

    public class AudioAPI
    {
        private static bool _initialized = false;
        private static NIMAudio.ResCodeIdCb _onAudioStartPlaying;
        private static NIMAudio.ResCodeIdCb _onAudioStopped;
        private static NIMAudio.NIMResCodeCb _onAudioStartCapturing;
        private static NIMAudio.NIMStopCaptureCb _onAudioStopCapturing;
        private static NIMAudio.NIMResCodeCb _onCancelAudioCapturing;
        private static NIMAudio.NIMEnumCaptureDevicesCb _onEnumCaptureDevices;

        /// <summary>
        /// NIM SDK 初始化语音模块
        /// </summary>
        /// <param name="userDataParentPath">用户目录</param>
        /// <returns><c>true</c> 调用成功, <c>false</c> 调用失败</returns>
        public static bool InitModule(string userDataParentPath)
        {
            return _initialized = AudioNativeMethods.nim_audio_init_module(userDataParentPath);
        }

        /// <summary>
        /// NIM SDK 卸载语音模块（只有在主程序关闭时才有必要调用此接口）
        /// </summary>
        /// <returns><c>true</c> 调用成功, <c>false</c> 调用失败</returns>
        public static bool UninitModule()
        {
            _initialized = false;
            _onAudioStartPlaying = null;
            _onAudioStopped = null;
            return AudioNativeMethods.nim_audio_uninit_module();
        }

        /// <summary>
        /// NIM SDK 播放,通过回调获取开始播放状态
        /// </summary>
        /// <param name="filePath">播放文件绝对路径</param>
        /// <param name="callerId"></param>
        /// <param name="resId">用以定位资源的二级ID，可选</param>
        /// <param name="audioFormat"></param>
        /// <returns><c>true</c> 调用成功, <c>false</c> 调用失败</returns>
        public static bool PlayAudio(string filePath, string callerId, string resId, NIMAudioType audioFormat)
        {
            if (!_initialized)
                throw new Exception("nim audio moudle uninitialized!");
            return AudioNativeMethods.nim_audio_play_audio(filePath, callerId, resId, (int)audioFormat);
        }

        /// <summary>
        /// NIM SDK 停止播放,通过回调获取停止播放状态
        /// </summary>
        /// <returns><c>true</c> 调用成功, <c>false</c> 调用失败</returns>
        public static bool StopPlayAudio()
        {
            return AudioNativeMethods.nim_audio_stop_play_audio();
        }

        /// <summary>
        /// NIM SDK 注册播放开始事件回调
        /// </summary>
        /// <param name="cb">播放开始事件的回调函数</param>
        /// <returns><c>true</c> 调用成功, <c>false</c> 调用失败</returns>
        public static bool RegStartPlayCb(NIMAudio.ResCodeIdCb cb)
        {
            _onAudioStartPlaying = cb;
            return AudioNativeMethods.nim_audio_reg_start_play_cb(_onAudioStartPlaying);
        }

        /// <summary>
        /// NIM SDK 注册播放结束事件回调
        /// </summary>
        /// <param name="cb">播放结束事件的回调函数</param>
        /// <returns><c>true</c> 调用成功, <c>false</c> 调用失败</returns>
        public static bool RegStopPlayCb(NIMAudio.ResCodeIdCb cb)
        {
            _onAudioStopped = cb;
            return AudioNativeMethods.nim_audio_reg_stop_play_cb(_onAudioStopped);
        }

        /// <summary>
        /// 注册录制语音回调
        /// </summary>
        /// <param name="cb"></param>
        /// <returns></returns>
        public static bool RegStartAudioCaptureCb(NIMAudio.NIMResCodeCb cb)
        {
            _onAudioStartCapturing = cb;
            return AudioNativeMethods.nim_audio_reg_start_capture_cb(_onAudioStartCapturing);
        }

        /// <summary>
        /// 注册录制语音结束回调
        /// </summary>
        /// <param name="cb"></param>
        /// <returns></returns>
        public static bool RegStopAudioCaptureCb(NIMAudio.NIMStopCaptureCb cb)
        {
            _onAudioStopCapturing = cb;
            return AudioNativeMethods.nim_audio_reg_stop_capture_cb(_onAudioStopCapturing);
        }

        /// <summary>
        /// 注册取消录制并删除临时文件事件回调
        /// </summary>
        /// <param name="cb"></param>
        /// <returns></returns>
        public static bool RegCancelAudioCapturingCb(NIMAudio.NIMResCodeCb cb)
        {
            _onCancelAudioCapturing = cb;
            return AudioNativeMethods.nim_audio_reg_cancel_audio_cb(_onCancelAudioCapturing);
        }

        /// <summary>
        /// 录制语音
        /// </summary>
        /// <param name="callID">用以定位资源的一级ID，可选</param>
        /// <param name="resID">用以定位资源的二级ID，可选</param>
        /// <param name="format">音频格式，AAC : 0， AMR : 1</param>
        /// <param name="volume">音量(0 - 255, 默认180) pc有效</param>
        /// <param name="loudness">默认0,低频增益开关,pc有效</param>
        /// <param name="device">录音设备</param>
        /// <returns></returns>
        public static bool StartCapture(string callID, string resID, NIMAudioType format = 0, int volume = 180, string device = null)
        {
#if NIM_WIN_DESKTOP_ONLY_SDK
			return AudioNativeMethods.nim_audio_start_capture(callID, resID, (int)format, volume, device);
#else
			return AudioNativeMethods.nim_audio_start_capture(callID, resID, (int)format, volume,0,device);
#endif
		}

		/// <summary>
		/// 停止录制语音
		/// </summary>
		/// <returns></returns>
		public static bool StopCapture()
        {
            return AudioNativeMethods.nim_audio_stop_capture();
        }

        /// <summary>
        /// 取消录制并删除临时文件
        /// </summary>
        /// <param name="audioPath"></param>
        /// <returns></returns>
        public static bool CancelCapture(string audioPath)
        {
            return AudioNativeMethods.nim_audio_cancel_audio(audioPath);
        }

        /// <summary>
        /// 注册枚举本地录音采集设备回调
        /// </summary>
        /// <param name="cb"></param>
        /// <returns></returns>
        public static bool RegEnumDevicesCb(NIMAudio.NIMEnumCaptureDevicesCb cb)
        {
            _onEnumCaptureDevices = cb;
            return AudioNativeMethods.nim_audio_reg_enum_capture_device_cb(_onEnumCaptureDevices);
        }

        /// <summary>
        /// 枚举本地录音采集设备
        /// </summary>
        /// <returns></returns>
        public static bool EnumCaptureDevices()
        {
            return AudioNativeMethods.nim_audio_enum_capture_device();
        }
    }
}
