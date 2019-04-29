using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace NimUtility
{
    public enum LogLevel
    {
        Verbose,
        Info,
        Error
    }

    public static class Log
    {
        private static string _filePath = null;
        private const string LogFileName = "nim_cs.log";
        private const int MaxLogFileLegth = 2 * 1024 * 1024; //2M

#if UNITY_STANDALONE || NIMAPI_UNDER_WIN_DESKTOP_ONLY
        private static Mutex _mutex = new Mutex(false,"NimLogFileMutex");
#else
         private static Mutex _mutex = new Mutex(false);
#endif
        static Log()
        {
            CreateLogFile();
        }

        static void AppendLogHeader()
        {
            var head = string.Format("{0} {1}:Nim Started {2}{3}", new string('-', 40), DateTime.Now.ToString("MM-dd hh:mm:ss"), new string('-', 40), System.Environment.NewLine);
            head = System.Environment.NewLine + head;
            OutPutLog(head);
        }

        //[Conditional("DEBUG")]
        static void CreateLogFile()
        {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_IOS || UNITY_ANDROID
            string targetDir = UnityEngine.Application.persistentDataPath;
            if (UnityEngine.Application.platform == UnityEngine.RuntimePlatform.Android)
            {
                if (Directory.Exists("/sdcard"))
                    targetDir = "/sdcard";
            }
            if(UnityEngine.Application.platform == UnityEngine.RuntimePlatform.WindowsPlayer)
            {
                targetDir = System.Environment.CurrentDirectory;
            }

            _filePath = Path.Combine(targetDir, LogFileName);
#else
            _filePath = Path.Combine(System.Environment.CurrentDirectory, LogFileName);
#endif
            _mutex.WaitOne();
            var stream = File.Open(_filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            if (stream.Length > MaxLogFileLegth)
            {
                if (stream.CanWrite && stream.CanSeek)
                    stream.SetLength(0);
                else
                    stream = File.Open(_filePath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
            }
            stream.Close();
            AppendLogHeader();
            _mutex.ReleaseMutex();
        }

        static void WriteFile(string log)
        {
            _mutex.WaitOne();
            {
                if (!string.IsNullOrEmpty(log))
                    File.AppendAllText(_filePath, log);
            }
            _mutex.ReleaseMutex();
        }

        static string FormatLog(LogLevel level, string log)
        {

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("[{0}:{1}] {2}:{3} ", DateTime.Now.ToString("MM-dd hh:mm:ss"), Thread.CurrentThread.ManagedThreadId,
                level.ToString(), log);
            builder.AppendLine();
            return builder.ToString();
        }

        static void OutPutLog(string log)
        {
            if (!log.EndsWith(System.Environment.NewLine))
                log += System.Environment.NewLine;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_IOS || UNITY_ANDROID
            UnityEngine.Debug.Log(log);
#else
            System.Diagnostics.Debug.WriteLine(log);
#endif
            WriteFile(log);
        }

        //[Conditional("DEBUG")]
        public static void Info(string log)
        {
            OutPutLog(log);
        }

        //[Conditional("DEBUG")]
        public static void Error(string logTxt)
        {
            StringBuilder builder = new StringBuilder();
            StackTrace st = new StackTrace(true);
            var frames = st.GetFrames();
            builder.AppendLine();
            builder.AppendLine(new string('*', 20) + "Error:");
            foreach (var sf in frames)
            {
                builder.AppendFormat("File: {0} Line:{1}", sf.GetFileName(), sf.GetFileLineNumber());
                builder.AppendLine();
                builder.AppendFormat("Method:{0}", sf.GetMethod().Name);
                builder.AppendLine();
            }
            var trace = builder.ToString();
            OutPutLog(logTxt + System.Environment.NewLine + trace);
        }
    }
}
