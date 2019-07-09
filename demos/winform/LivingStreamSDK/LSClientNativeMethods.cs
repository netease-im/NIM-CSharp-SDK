using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NIMDemo.LivingStreamSDK
{
	class LSClientNativeMethods
	{
		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_GetFreeDevicesNum", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Nlss_GetFreeDevicesNum(out IntPtr iVideoDeviceNum, out IntPtr iAudioDeviceNum);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_GetFreeDeviceInf", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Nlss_GetFreeDeviceInf(out IntPtr pstVideoDevices, out IntPtr pstAudioDevices);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_Create", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int Nlss_Create(
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string paLogpath, 
			out IntPtr phNLSService);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_GetVersionNo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Nlss_GetVersionNo(out IntPtr ppaVersion);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_Destroy", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Nlss_Destroy(IntPtr hNLSService);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_GetDefaultParam", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int Nlss_GetDefaultParam(IntPtr hNLSService,IntPtr pstParam);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_InitParam", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int Nlss_InitParam(IntPtr hNLSService, IntPtr pstParam);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_SetVideoDisplayRatio", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Nlss_SetVideoDisplayRatio(IntPtr hNLSService, int iWideUnit, int iHeightUnit);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_SetVideoSamplerCB", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Nlss_SetVideoSamplerCB(IntPtr hNLSService, PFN_NLSS_VIDEOSAMPLER_CB pFunVideoSamplerCB);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_SetStatusCB", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Nlss_SetStatusCB(IntPtr hNLSService, PFN_NLSS_STATUS_NTY pFunStatusNty);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_UninitParam", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Nlss_UninitParam(IntPtr hNLSService);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_StartVideoCapture", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Nlss_StartVideoCapture(IntPtr hNLSService);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_StopVideoCapture", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Nlss_StopVideoCapture(IntPtr hNLSService);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_StartVideoPreview", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int Nlss_StartVideoPreview(IntPtr hNLSService);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_PauseVideoPreview", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Nlss_PauseVideoPreview(IntPtr hNLSService);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_StopVideoPreview", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Nlss_StopVideoPreview(IntPtr hNLSService);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_StartLiveStream", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int Nlss_StartLiveStream(IntPtr hNLSService);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_StopLiveStream", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Nlss_StopLiveStream(IntPtr hNLSService);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_PauseVideoLiveStream", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Nlss_PauseVideoLiveStream(IntPtr hNLSService);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_ResumeVideoLiveStream", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Nlss_ResumeVideoLiveStream(IntPtr hNLSService);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_PauseAudioLiveStream", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Nlss_PauseAudioLiveStream(IntPtr hNLSService);


		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_ResumeAudioLiveStream", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Nlss_ResumeAudioLiveStream(IntPtr hNLSService);


		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_SendCustomVideoData", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int Nlss_SendCustomVideoData(IntPtr hNLSService,
			IntPtr pcVideoData,
			int iLen,ref int penErrCode);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_SendCustomAudioData", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int Nlss_SendCustomAudioData(IntPtr hNLSService,
		IntPtr pcAudioData,
		int iLen, 
		int iSampleRate,
		ref int penErrCode);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "Nlss_GetStaticInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Nlss_GetStaticInfo(IntPtr hNLSService,out IntPtr penErrCode);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "GetAvailableAppWindNum", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void GetAvailableAppWindNum(IntPtr iAppWindNum);

		[DllImport(LSGlobal.LSNativeDLL, EntryPoint = "GetAvailableAppWind", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void GetAvailableAppWind(IntPtr pLSAppWindTitles);
	}
}
