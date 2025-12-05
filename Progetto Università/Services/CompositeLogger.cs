using Interfaces;
using Progetto_Università.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public sealed class CompositeLogger : ILogger
    {
        private static CompositeLogger _instance = null;

       
        private Dictionary<ILogger, bool> loggers = new Dictionary<ILogger, bool>();

        private CompositeLogger()
        {
            
            loggers.Add(Logger.Instance, true);         
            loggers.Add(LoggerFile.Instance, true);     
            loggers.Add(LoggerDatabase.Instance, true); 
        }

        public static CompositeLogger Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CompositeLogger();
                return _instance;
            }
        }

       
        public void Log(string messaggio)
        {
            foreach (var kvp in loggers.Where(l => l.Value == true))
            {
                kvp.Key.Log(messaggio);
            }
        }

        
        public void SetLoggerStatus(ILogger logger, bool isEnabled)
        {
            if (loggers.ContainsKey(logger))
            {
                loggers[logger] = isEnabled;
            }
        }

        
        public bool GetLoggerStatus(ILogger logger)
        {
            return loggers.ContainsKey(logger) && loggers[logger];
        }

        
        public Dictionary<ILogger, bool> GetAllLoggers()
        {
            return loggers;
        }
    }
}