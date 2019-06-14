using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using log4net;
using log4net.Core;

namespace Tool
{
    class FTBAutoTestLoger : ILog
    {
        public delegate string GetLogFileNameHandler();
        private ILog log4netLoger;
        private string previousLogName;
        private GetLogFileNameHandler getFileNameHandler;
        private Mutex logMutex;
        public FTBAutoTestLoger(ILog ilog, GetLogFileNameHandler handler)
        {
            if (null == ilog && null == getFileNameHandler)
                throw new FTBAutoTestException("Create FTBAutoTestLoger failed by invalid param.");
            log4netLoger = ilog;
            getFileNameHandler = handler;
            previousLogName = "";
            logMutex = new Mutex();
        }

        public bool IsDebugEnabled
        {
            get
            {
                return log4netLoger.IsDebugEnabled;
            }
        }
        public bool IsErrorEnabled
        {
            get
            {
                return log4netLoger.IsErrorEnabled;
            }
        }
        public bool IsFatalEnabled
        {
            get
            {
                return log4netLoger.IsFatalEnabled;
            }
        }
        public bool IsInfoEnabled
        {
            get
            {
                return log4netLoger.IsInfoEnabled;
            }
        }
        public bool IsWarnEnabled
        {
            get
            {
                return log4netLoger.IsWarnEnabled;
            }
        }

        public ILogger Logger
        {
            get
            {
                return log4netLoger.Logger;
            }
        }

        public void Debug(object message)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.Debug(message);
            logMutex.ReleaseMutex();
        }
        public void Debug(object message, Exception exception)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.Debug(message,exception);
            logMutex.ReleaseMutex();
        }
        public void DebugFormat(string format, object arg0)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.DebugFormat(format, arg0);
            logMutex.ReleaseMutex();
        }
        public void DebugFormat(string format, params object[] args)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.DebugFormat(format, args);
            logMutex.ReleaseMutex();
        }
        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.DebugFormat(provider, format, args);
            logMutex.ReleaseMutex();
        }
        public void DebugFormat(string format, object arg0, object arg1)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.DebugFormat(format, arg0, arg1);
            logMutex.ReleaseMutex();
        }
        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.DebugFormat(format, arg0, arg1, arg2);
            logMutex.ReleaseMutex();
        }

        public void Error(object message)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.Error(message);
            logMutex.ReleaseMutex();
        }
        public void Error(object message, Exception exception)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.Error(message, exception);
            logMutex.ReleaseMutex();
        }
        public void ErrorFormat(string format, object arg0)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.ErrorFormat(format, arg0);
            logMutex.ReleaseMutex();
        }
        public void ErrorFormat(string format, params object[] args)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.ErrorFormat(format, args);
            logMutex.ReleaseMutex();
        }
        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.ErrorFormat(provider, format, args);
            logMutex.ReleaseMutex();
        }
        public void ErrorFormat(string format, object arg0, object arg1)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.ErrorFormat(format, arg0, arg1);
            logMutex.ReleaseMutex();
        }
        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.ErrorFormat(format, arg0, arg1, arg2);
            logMutex.ReleaseMutex();
        }

        public void Fatal(object message)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.Fatal(message);
            logMutex.ReleaseMutex();
        }
        public void Fatal(object message, Exception exception)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.Fatal(message, exception);
            logMutex.ReleaseMutex();
        }
        public void FatalFormat(string format, object arg0)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.FatalFormat(format, arg0);
            logMutex.ReleaseMutex();
        }
        public void FatalFormat(string format, params object[] args)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.FatalFormat(format, args);
            logMutex.ReleaseMutex();
        }
        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.FatalFormat(provider, format, args);
            logMutex.ReleaseMutex();
        }
        public void FatalFormat(string format, object arg0, object arg1)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.FatalFormat(format, arg0, arg1);
            logMutex.ReleaseMutex();
        }
        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.FatalFormat(format, arg0, arg1, arg2);
            logMutex.ReleaseMutex();
        }

        public void Info(object message)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.Info(message);
            logMutex.ReleaseMutex();
        }
        public void Info(object message, Exception exception)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.Info(message, exception);
            logMutex.ReleaseMutex();
        }
        public void InfoFormat(string format, object arg0)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.InfoFormat(format, arg0);
            logMutex.ReleaseMutex();
        }
        public void InfoFormat(string format, params object[] args)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.InfoFormat(format, args);
            logMutex.ReleaseMutex();
        }
        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.InfoFormat(provider, format, args);
            logMutex.ReleaseMutex();
        }
        public void InfoFormat(string format, object arg0, object arg1)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.InfoFormat(format, arg0, arg1);
            logMutex.ReleaseMutex();
        }
        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.InfoFormat(format, arg0, arg1, arg2);
            logMutex.ReleaseMutex();
        }

        public void Warn(object message)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.Warn(message);
            logMutex.ReleaseMutex();
        }
        public void Warn(object message, Exception exception)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.Warn(message, exception);
            logMutex.ReleaseMutex();
        }
        public void WarnFormat(string format, object arg0)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.WarnFormat(format, arg0);
            logMutex.ReleaseMutex();
        }
        public void WarnFormat(string format, params object[] args)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.WarnFormat(format, args);
            logMutex.ReleaseMutex();
        }
        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.WarnFormat(provider, format, args);
            logMutex.ReleaseMutex();
        }
        public void WarnFormat(string format, object arg0, object arg1)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.WarnFormat(format, arg0, arg1);
            logMutex.ReleaseMutex();
        }
        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            logMutex.WaitOne();
            CheckLogFileName();
            log4netLoger.WarnFormat(format, arg0, arg1, arg2);
            logMutex.ReleaseMutex();
        }

        private void CheckLogFileName()
        {
            string currentFileName = getFileNameHandler();
            if (previousLogName != currentFileName)
            {
                previousLogName = currentFileName;
                LogImpl logImpl = log4netLoger as LogImpl;
                if (logImpl != null)
                {
                    log4net.Appender.AppenderCollection ac = ((log4net.Repository.Hierarchy.Logger)logImpl.Logger).Appenders;
                    for (int i = 0; i < ac.Count; i++)
                    {
                        log4net.Appender.FileAppender fa = ac[i] as log4net.Appender.FileAppender;
                        if (fa != null)
                        {
                            string pathName = Path.GetDirectoryName(currentFileName);
                            if (!Directory.Exists(pathName))
                                Directory.CreateDirectory(pathName);
                            fa.File = currentFileName;
                            fa.ActivateOptions();
                        }
                    }
                }
            }
        }
    }
    static class StaticLog4NetLogger
    {
        public static ILog mfcLogger,commandExecutorLogger,reportLogger;
        static StaticLog4NetLogger()
        {
            var logCfg = new FileInfo(StaticEnvironInfo.getLog4NetConfigFileName());
            log4net.Config.XmlConfigurator.ConfigureAndWatch(logCfg);
            mfcLogger = new FTBAutoTestLoger(LogManager.GetLogger("MFCLogger"), 
                                             StaticEnvironInfo.getMachineLogFileName);
            commandExecutorLogger = new FTBAutoTestLoger(LogManager.GetLogger("CommandExecutorLogger"),
                                                         StaticEnvironInfo.getCommandLogFileName);
            reportLogger = new FTBAutoTestLoger(LogManager.GetLogger("ReportLogger"),
                                                StaticEnvironInfo.getReportLogFileName);
        }
    }
}
