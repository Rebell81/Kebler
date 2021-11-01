﻿using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Kebler.Services.Interfaces;

namespace Kebler.Services
{
    public class Log : ILog
    {
        private static readonly string FileName = $"{nameof(Kebler)}_{DateTime.Now:dd-MM-yyyy}.log";
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Const.ConstStrings.LOG_FOLDER, FileName);
        private static readonly object Lock = new();
        public static readonly FileInfo LogFileInfo = new(FilePath);
        private enum LogType {Info, Warn, Error, ui}
        private static ILog _logger;

        public static ILog Instance => _logger ??= new Log();

        public void Info(string message, [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = "", [CallerFilePath] string sourceFilePath = "")
        {
            var data = Format(message, LogType.Info, lineNumber, caller, GetClassName(sourceFilePath));

            Task.Run(() =>
            {
                WriteToFile(data);
            });
        }

        public void Warn(string message, [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = "", [CallerFilePath] string sourceFilePath = "")
        {
            var data = Format(message, LogType.Warn, lineNumber, caller, GetClassName(sourceFilePath));

            Task.Run(() =>
            {
                WriteToFile(data);
            });
        }

        public void Error(Exception exception, [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = "", [CallerFilePath] string sourceFilePath = "")
        {
            var data = Format(FormatException(exception), LogType.Error, lineNumber, caller, GetClassName(sourceFilePath));

            Task.Run(() =>
            {
                WriteToFile(data);
            });
#if RELEASE
            Microsoft.AppCenter.Crashes.Crashes.TrackError(exception);
#endif
        }

        public void Error(string exception, [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = "", [CallerFilePath] string sourceFilePath = "")
        {
            var data = Format(exception, LogType.Error, lineNumber, caller, GetClassName(sourceFilePath));

            Task.Run(() =>
            {
                WriteToFile(data);
            });
#if RELEASE
            Microsoft.AppCenter.Crashes.Crashes.TrackError(new Exception { Source = exception });
#endif
        }

        public void Ui(string button, [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = "", [CallerFilePath] string sourceFilePath = "")
        {
            var data = Format( $"User clicked '{button}'", LogType.ui, lineNumber, caller, GetClassName(sourceFilePath));

            Task.Run(() =>
            {
                WriteToFile(data);
            });
        }

        public void Ui([CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = "", [CallerFilePath] string sourceFilePath = "")
        {
            var data = Format($"User clicked", LogType.ui, lineNumber, caller, GetClassName(sourceFilePath));

            Task.Run(() =>
            {
                WriteToFile(data);
            });
        }

        private static string Format(string message, LogType type, int lineNumber = 0, string caller = "", string className = "")
        {
            var thread = Dispatcher.CurrentDispatcher.Thread == Thread.CurrentThread ? "Main" : $"{Thread.CurrentThread.ManagedThreadId}-{Thread.CurrentThread.Name}";
            return $"[{DateTime.Now:dd-MM-yyyy HH:mm:ss.fff}] [{thread}] [{type.ToString().ToLower()}] [{className}({caller}):{lineNumber}] {message}{Environment.NewLine}";
        }

        private string FormatException(Exception ex)
        {
            return $"{ex.Message}{Environment.NewLine}{ex.StackTrace}";
        }

        private static string GetClassName(string path)
        {
            return new FileInfo(path).Name;
        }


        private static void WriteToFile(string data)
        {
            lock (Lock)
            {
                try
                {
                    if (LogFileInfo?.DirectoryName != null)
                        Directory.CreateDirectory(LogFileInfo.DirectoryName);

                    File.AppendAllTextAsync(FilePath, data);
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
}
