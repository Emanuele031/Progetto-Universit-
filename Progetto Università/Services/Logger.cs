using Progetto_Università.Interfaces;
using System;
using System.Collections.Generic;
using Interfaces;

namespace Services
{
    public sealed class Logger : ILogger
    {
        private static Logger _instance = null;
        private List<string> logInMemoria = new List<string>();

        private Logger() { }

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Logger();
                return _instance;
            }
        }

        public void Log(string messaggio)
        {
            string log = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {messaggio}";
            logInMemoria.Add(log);
        }

        public List<string> GetLogs()
        {
            return new List<string>(logInMemoria);
        }

        public void Clear()
        {
            logInMemoria.Clear();
        }
    }
}
