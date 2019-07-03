/** @file LSGlobalDef.cs
  * @brief 直播 SDK提供的一些全局定义
  * @copyright (c) 2016, NetEase Inc. All rights reserved
  * @author leewp
  * @date 2016/8/2
  */

using System;
using System.Runtime.InteropServices;

namespace NIMDemo.LivingStreamSDK
{
	internal class LSGlobal
	{
		/// <summary>
		///     The living stream native DLL
		/// </summary>
#if DEBUG
		public const string LSNativeDLL = @"livingStream\LSMediaCapture.dll";
#else
        public const string LSNativeDLL = @"livingStream\LSMediaCapture.dll";
#endif

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void JsonTransportCb(string jsonParams, IntPtr userData);
	}
}