using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using Serilog.Events;
using System.Text;

namespace IMASD.Base.Utilities
{
    public static class SeriLogHelper
    {
        private static readonly ILogger Errorlog;
        private static readonly ILogger Warninglog;
        private static readonly ILogger Debuglog;
        private static readonly ILogger Verboselog;
        private static readonly ILogger Fatallog;
        private static readonly ILogger Informationlog;
        

        static SeriLogHelper()
        {
            // 5 MB = 5242880 bytes
            string path=AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("\\", "/");
            path = path.Replace("bin/", "");
            path = path.Replace("Debug", "");
            path=path.Replace("Release", "");
            Errorlog = new LoggerConfiguration()
                .MinimumLevel.Error()
               .WriteTo.File(path: path+"/ErrorLog/Error/log.txt",
                rollingInterval: RollingInterval.Day,
                fileSizeLimitBytes: 5242880,
                rollOnFileSizeLimit: true)
                .CreateLogger();

            Warninglog = new LoggerConfiguration()
                .MinimumLevel.Warning()
              .WriteTo.File(path: path + "/ErrorLog/Warning/log.txt",
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 5242880,
                    rollOnFileSizeLimit: true)
                .CreateLogger();

            Debuglog = new LoggerConfiguration()
                .MinimumLevel.Debug()
              .WriteTo.File(path: path + "/ErrorLog/Debug/log.txt",
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 5242880,
                    rollOnFileSizeLimit: true)
                .CreateLogger();

            Verboselog = new LoggerConfiguration()
                .MinimumLevel.Verbose()
              .WriteTo.File(path: path + "/ErrorLog/Verbose/log.txt",
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 5242880,
                    rollOnFileSizeLimit: true)
                .CreateLogger();

            Fatallog = new LoggerConfiguration()
                .MinimumLevel.Fatal()
              .WriteTo.File(path: path + "/ErrorLog/Fatal/log.txt",
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 5242880,
                    rollOnFileSizeLimit: true)
                .CreateLogger();
            Informationlog = new LoggerConfiguration()
                .MinimumLevel.Information()
              .WriteTo.File(path: path + "/ErrorLog/Information/log.txt",
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 5242880,
                    rollOnFileSizeLimit: true)
                .CreateLogger();

        }

        public static void WriteError(Exception ex, string message)
        {
            //Error - indicating a failure within the application or connected system
            Errorlog.Write(LogEventLevel.Error, ex, message);
        }

        public static void WriteWarning(Exception ex, string message)
        {
            //Warning - indicators of possible issues or service / functionality degradation
            Warninglog.Write(LogEventLevel.Warning, ex, message);
        }

        public static void WriteDebug(Exception ex, string message)
        {
            //Debug - internal control flow and diagnostic state dumps to facilitate 
            //          pinpointing of recognised problems
            Debuglog.Write(LogEventLevel.Debug, ex, message);
        }

        public static void WriteVerbose(Exception ex, string message)
        {
            // Verbose - tracing information and debugging minutiae; 
            //             generally only switched on in unusual situations
            Verboselog.Write(LogEventLevel.Verbose, ex, message);
        }

        public static void WriteFatal(Exception ex, string message)
        {
            //Fatal - critical errors causing complete failure of the application
            Fatallog.Write(LogEventLevel.Fatal, ex, message);
        }

        public static void WriteInformation(Exception ex, string message)
        {
            //Information -events of interest or that have relevance to outside observers; the default enabled minimum logging level
            Informationlog.Write(LogEventLevel.Information, ex, message);
        }

    }
}
