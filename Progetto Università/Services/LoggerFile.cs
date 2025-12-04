using System;
using System.Collections.Generic;
using System.IO;

namespace Services
{
    public sealed class LoggerFile
    {
        private static LoggerFile _instance = null;
        private List<string> logInMemoria = new List<string>();
        private string percorsoFile = "log.txt";

        private LoggerFile() { }

        public static LoggerFile Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LoggerFile();
                return _instance;
            }
        }

        public void SetFilePath(string path)
        {
            percorsoFile = path;
        }

        public void Log(string messaggio)
        {
            string log = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {messaggio}";

            
            logInMemoria.Add(log);

            
            try
            {
                File.AppendAllText(percorsoFile, log + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore scrittura file log: {ex.Message}");
            }
        }

        
        public List<string> GetLogs()
        {
            try
            {
                if (File.Exists(percorsoFile))
                    return new List<string>(File.ReadAllLines(percorsoFile));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore lettura file log: {ex.Message}");
            }
            return new List<string>();
        }

        
        public List<string> GetLogsInMemory()
        {
            return new List<string>(logInMemoria);
        }

        
        public void Clear()
        {
            logInMemoria.Clear();
            try
            {
                if (File.Exists(percorsoFile))
                    File.WriteAllText(percorsoFile, string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore pulizia file log: {ex.Message}");
            }
        }
    }
}
