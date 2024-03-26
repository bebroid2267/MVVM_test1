using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_test1.DataBase
{
    public static class DateBase
    {
        private static readonly string connectionString = @"Data Source = C:\Users\кирилл\Desktop\testFirstWPF.db";

        private static bool IfProcessExists(string name)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT COUNT(*) FROM Process WHERE name LIKE '{name}'";

                long existsProcess = (long)command.ExecuteScalar();

                if (existsProcess > 0)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }


            }

        }

        public static void AddProcess(string name, string startDateUsing)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = new SqliteCommand();
                command.Connection = connection;

                if (!IfProcessExists(name))
                {
                    command.CommandText = $"INSERT INTO Process (name, global_start_time) values (@Name, @GlobalStart)";
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@GlobalStart", startDateUsing);

                    command.ExecuteNonQuery();

                    connection.Close();
                }
                else
                {
                    connection.Close();
                }

            }

        }

        public static string GetTimeProcess(string name)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                string time = string.Empty;
                connection.Open();

                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT sum_time FROM Process WHERE name LIKE '{name}'";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        time = reader.GetString(0);
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }
                }

                connection.Close();
                return time;

            }

        }
        public static List<ProcessTime> GetInfoProcess(string whatTypeProcess)
        {
            using (SqliteConnection connection = new SqliteConnection())
            {
                List<ProcessTime> infoProcesess = new List<ProcessTime>();

                connection.Open();
                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT name, sum_time, global_start_time FROM Process WHERE status LIKE '{whatTypeProcess}'";
            
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ProcessTime info = new ProcessTime();
                    info.NameProcess = reader.GetString(0);
                    info.SumTimeProcess = reader.GetString(1);
                    info.GlobalStartTime = reader.GetString(2);
                    infoProcesess.Add(info);
                }

                connection.Close();
                return infoProcesess;
            }

        }
        public static void StartSession(string name, string dateTime)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"UPDATE Process SET start_session = '{dateTime}', status = 'works' WHERE name LIKE '{name}'";

                command.ExecuteNonQuery();
                connection.Close();
            }

        }
        public static void StopSession(string name, string dateTime, string sumTime)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"UPDATE Process SET end_session = '{dateTime}', status = 'stoped' , sum_time = '{sumTime}' WHERE name LIKE '{name}'";

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void UpdateTimeProcess(string name, string time)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"UPDATE Process SET sum_time = '{time}' WHERE name LIKE '{name}'";

                command.ExecuteNonQuery();

                connection.Close();

            }

        }
        public static bool CheckProcessDoesWorks(string nameProcess)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                string status = string.Empty;
                connection.Open();

                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT status FROM Process WHERE name LIKE '{nameProcess}'";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    status = reader.GetString(0);
                }
                connection.Close();
                if(status == "works") 
                    return true;
                else
                    return false;
            }

        }
    }
}
