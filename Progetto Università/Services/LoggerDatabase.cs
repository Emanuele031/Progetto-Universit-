using Interfaces;
using Progetto_Università.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Services
{
    public sealed class LoggerDatabase : ILogger
    {
        private static LoggerDatabase _instance = null;
        private string connectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=Universita;Integrated Security=True;TrustServerCertificate=True;";



        private LoggerDatabase() { }

        public static LoggerDatabase Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LoggerDatabase();
                return _instance;
            }
        }


        public void SetConnectionString(string conn)
        {
            connectionString = conn;
        }


        public void Log(string messaggio)
        {
            string sql = "INSERT INTO Logs (Timestamp, Messaggio) VALUES (@ts, @msg)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ts", DateTime.Now);
                cmd.Parameters.AddWithValue("@msg", messaggio);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public List<string> GetLogs()
        {
            List<string> lista = new List<string>();

            string sql = "SELECT Timestamp, Messaggio FROM Logs ORDER BY Id DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add($"[{reader.GetDateTime(0)}] {reader.GetString(1)}");
                    }
                }
            }
            return lista;
        }


        public void Clear()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Logs", conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
