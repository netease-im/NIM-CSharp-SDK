using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NIM
{
    /// <summary>
    /// native dll 信息
    /// </summary>
    public class NativeConfig
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_IOS || UNITY_ANDROID
#if DEBUGVERSION
#if UNITY_IOS
            public const string NIMNativeDLL = "__Internal";
            public const string NIMAudioNativeDLL = "__Internal";
            public const string NIMHttpNativeDLL = "__Internal";
            public const string ChatRoomNativeDll = "__Internal";
#elif UNITY_ANDROID
            public const string NIMNativeDLL = "nim";
            public const string NIMAudioNativeDLL = "nim_audio";
            public const string NIMHttpNativeDLL = "nim_tools_http";
            public const string ChatRoomNativeDll = "nim_chatroom";
#elif UNITY_STANDALONE_LINUX
            public const string NIMNativeDLL = "nim_linux";
            public const string NIMAudioNativeDLL = "nim_audio";
            public const string NIMHttpNativeDLL = "nim_tools_http";
            public const string ChatRoomNativeDll = "nim_chatroom";
#elif UNITY_STANDALONE_WIN
        public const string NIMNativeDLL = "nim";
        public const string NIMAudioNativeDLL = "nim_audio";
        public const string NIMHttpNativeDLL = "nim_tools_http";
        public const string ChatRoomNativeDll = "nim_chatroom";
#elif UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
			public const string NIMNativeDLL = "nim_sdk";
			public const string NIMAudioNativeDLL = "nim_audio_sdk";
			public const string NIMHttpNativeDLL = "nim_tools_http";
			public const string ChatRoomNativeDll = "nim_chatroom_sdk";
#endif
#else
#if UNITY_IOS
            public const string NIMNativeDLL = "__Internal";
            public const string NIMAudioNativeDLL = "__Internal";
            public const string NIMHttpNativeDLL = "__Internal";
            public const string ChatRoomNativeDll = "__Internal";
#elif UNITY_ANDROID
        public const string NIMNativeDLL = "nim";
            public const string NIMAudioNativeDLL = "nim_audio";
            public const string NIMHttpNativeDLL = "nim_tools_http";
            public const string ChatRoomNativeDll = "nim_chatroom";
#elif UNITY_STANDALONE_LINUX
            public const string NIMNativeDLL = "nim_linux";
            public const string NIMAudioNativeDLL = "nim_audio";
            public const string NIMHttpNativeDLL = "nim_tools_http";
            public const string ChatRoomNativeDll = "nim_chatroom";
#elif UNITY_STANDALONE_WIN
            public const string NIMNativeDLL = "nim";
            public const string NIMAudioNativeDLL = "nim_audio";
            public const string NIMHttpNativeDLL = "nim_tools_http";
            public const string ChatRoomNativeDll = "nim_chatroom";
#elif UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
			public const string NIMNativeDLL = "nim_sdk";
			public const string NIMAudioNativeDLL = "nim_audio_sdk";
			public const string NIMHttpNativeDLL = "nim_tools_http";
			public const string ChatRoomNativeDll = "nim_chatroom_sdk";
#endif
#endif
#else
#if DEBUGVERSION1
        public const string NIMNativeDLL = "nim_d.dll";
        public const string NIMAudioNativeDLL = "nim_audio_d.dll";
        public const string NIMHttpNativeDLL = "nim_tools_http_d.dll";
        public const string ChatRoomNativeDll = "nim_chatroom_d.dll";
#else
        public const string NIMNativeDLL = "nim.dll";
        public const string NIMAudioNativeDLL = "nim_audio.dll";
        public const string NIMHttpNativeDLL = "nim_tools_http.dll";
        public const string ChatRoomNativeDll = "nim_chatroom.dll";
#endif
#endif
    }
}
