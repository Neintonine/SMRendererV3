using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL4;
using SM.OGL;

namespace SM.Base
{
    /// <summary>
    /// Specifies the target.
    /// </summary>
    [Flags]
    public enum LogTarget
    {
        /// <summary>
        /// No target, will not draw.
        /// </summary>
        None = 0,
        /// <summary>
        /// Takes the <see cref="Log.DefaultTarget"/>.
        /// </summary>
        Default = 1,
        /// <summary>
        /// Writes the log to the console.
        /// </summary>
        Console = 2,
        /// <summary>
        /// Writes the log to the debugger at <see cref="Debug"/>.
        /// </summary>
        Debugger = 4,
        /// <summary>
        /// Writes the log to the specific file.
        /// </summary>
        File = 8,
        /// <summary>
        /// Writes the log to every target.
        /// </summary>
        All = Console | Debugger | File
    }

    /// <summary>
    /// Preset log types.
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// Informations. Console Color: Green
        /// </summary>
        Info,
        /// <summary>
        /// Warnings. Console Color: Yellow
        /// </summary>
        Warning,
        /// <summary>
        /// Error. Console Color: Red
        /// </summary>
        Error
    }

    /// <summary>
    /// Contains the system for logging.
    /// </summary>
    public class Log
    {
        private static StreamWriter _logStream;
        private static bool _init = false;

        /// <summary>
        /// Presets for the log targets.
        /// </summary>
        public static Dictionary<LogTarget, string> Preset = new Dictionary<LogTarget, string>()
        {
            {LogTarget.Console, "[%type%] %msg%"},
            {LogTarget.Debugger, "[%type%] %msg%"},
            {LogTarget.File, "<%date%, %time%> [%type%] %msg%"}
        };

        private static readonly Dictionary<LogType, ConsoleColor> Colors = new Dictionary<LogType, ConsoleColor>()
        {
            {LogType.Info, ConsoleColor.Green},
            {LogType.Warning, ConsoleColor.Yellow},
            {LogType.Error, ConsoleColor.Red},
        };

        /// <summary>
        /// Specified the default target.
        /// </summary>
        public static LogTarget DefaultTarget = LogTarget.All;

        /// <summary>
        /// Sets the log file. At wish compresses the old file to a zip file.
        /// </summary>
        /// <param name="path">The path to the log file.</param>
        /// <param name="compressionFolder">Path for the compression, if desired.</param>
        public static void SetLogFile(string path = "sm.log", string compressionFolder = "")
        {
            _logStream?.Close();

            if (!_init) Init();

            if (File.Exists(path))
            {
                if (compressionFolder != "")
                {
                    DateTime creation = File.GetLastWriteTime(path);
                    try
                    {
                        using ZipArchive archive =
                            ZipFile.Open(
                                $"{compressionFolder}{Path.DirectorySeparatorChar}{Path.GetFileName(path)}_{creation.Year.ToString() + creation.Month + creation.Day}_{creation.Hour.ToString() + creation.Minute + creation.Second + creation.Millisecond}.zip",
                                ZipArchiveMode.Create);
                        archive.CreateEntryFromFile(path, Path.GetFileName(path));
                    }
                    catch
                    {
                        // ignore
                    }
                }

                File.Delete(path);
            }

            _logStream = new StreamWriter(path) {AutoFlush = true};

            Write(LogType.Info, $"Activated new log file. ['{path}']");

        }

        static void Init()
        {
            AppDomain.CurrentDomain.UnhandledException += ExceptionHandler;
            AppDomain.CurrentDomain.DomainUnload += (sender, args) =>
            {
                _logStream.WriteLine("Unload application");
                _logStream.Close();
            };

            GLDebugging.DebugAction = GLDebugAction;
            GLDebugging.GlErrorAction = code =>
            {
                Write(LogType.Warning, $"A '{code}' GL error occured.");
            };
            _init = true;
        }

        private static void GLDebugAction(DebugSource source, DebugType type, DebugSeverity severity, string msg)
        {
            if (type.HasFlag(DebugType.DebugTypeError))
            {
                throw new Exception("[GLError] "+msg);
            }
            Write(type != DebugType.DontCare ? type.ToString().Substring(9) : "DontCare", ConsoleColor.Gray, msg);
        }

        [DebuggerStepThrough]
        private static void ExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Write(e.IsTerminating ? "Terminating Error" : LogType.Error.ToString(), e.IsTerminating ? ConsoleColor.DarkRed : ConsoleColor.Red, e.ExceptionObject);

            if (e.IsTerminating)
            {
                MessageBox.Show($"Critical error occured.\n\n{e.ExceptionObject}",
                    $"Terminating Error: {e.ExceptionObject.GetType().Name}");
                _logStream?.Close();
            }
        }

        /// <summary>
        /// Writes multiple lines of the same type to the log.
        /// </summary>
        public static void Write<T>(LogType type, params T[] values) => Write<T>(type.ToString(), Colors[type], values);

        /// <summary>
        /// Writes multiple lines of the same type to the log.
        /// </summary>
        public static void Write<T>(string type, ConsoleColor color, params T[] values)
        {
            for (var i = 0; i < values.Length; i++)
            {
                Write(type, color, values[i], DefaultTarget);
            }
        }

        /// <summary>
        /// Writes one line to the log.
        /// </summary>
        public static void Write<T>(LogType type, T value, LogTarget target = LogTarget.Default) =>
            Write<T>(type.ToString(), Colors[type], value, target);

        /// <summary>
        /// Writes one line to the log.
        /// 
        /// </summary>
        public static void Write<T>(string type, ConsoleColor color, T value, LogTarget target = LogTarget.Default)
        {
            if (target == LogTarget.Default)
                target = DefaultTarget;

            if (target.HasFlag(LogTarget.Console))
                ColorfulWriteLine(color, ProcessPreset(LogTarget.Console, type, value.ToString()));
            if (target.HasFlag(LogTarget.Debugger))
                Debug.WriteLine(ProcessPreset(LogTarget.Debugger, type, value.ToString()));
            if (target.HasFlag(LogTarget.File))
                _logStream?.WriteLine(ProcessPreset(LogTarget.File, type, value.ToString()));
        }

        /// <summary>
        /// Writes a text with a different color.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="value"></param>
        public static void ColorfulWriteLine(ConsoleColor color, string value)
        {
            ConsoleColor before = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ForegroundColor = before;
        }

        static string ProcessPreset(LogTarget target, string type, string msg)
        {
            string preset = Preset[target];
            DateTime now = DateTime.Now;

            return preset.Replace("%date%", now.ToShortDateString())
                .Replace("%time%", now.ToShortTimeString())
                .Replace("%type%", type)
                .Replace("%msg%", msg);
        }
    }
}