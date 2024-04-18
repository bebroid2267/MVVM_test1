using Microsoft.Data.Sqlite;
using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_test1.DataBase
{
    public static class DateBase
    {
        //private static readonly string connectionString = @"Data Source = C:\Users\кирилл\Desktop\testFirstWPF.db";
        private static readonly string connectionString = @"Data Source = C:\Users\porka\OneDrive\Рабочий стол\testFirstWPF.db";


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
        public static string GetTimeStartTodaySession(string nameProcess)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                string todaySession = string.Empty;
                connection.Open();
                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT start_today_session FROM Process WHERE name = '{nameProcess}'";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        todaySession = reader.GetString(0);
                    }    
                }
                connection.Close();
                return todaySession;
            }

        }
        public static void AddTodaySession(string nameProcess, string dateTime)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"UPDATE Process SET start_today_session = '{dateTime}' WHERE name LIKE '{nameProcess}'";
                
                command.ExecuteNonQuery();
                connection.Close();
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
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                List<ProcessTime> infoProcesess = new List<ProcessTime>();

                connection.Open();
                var command = new SqliteCommand();
                command.Connection = connection;

                if (whatTypeProcess == "all")
                    command.CommandText = $"SELECT name, sum_time, global_start_time, end_session, start_today_session FROM Process";
                else
                command.CommandText = $"SELECT name, sum_time, global_start_time, end_session, start_today_session FROM Process WHERE status LIKE '{whatTypeProcess}'";
            
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ProcessTime info = new ProcessTime();
                    info.NameProcess = reader.GetString(0);
                    info.SumTimeProcess = reader.GetString(1);
                    info.GlobalStartTime = reader.GetString(2);
                    if (!reader.IsDBNull(3))
                        info.EndSession = reader.GetString(3);
                    if (!reader.IsDBNull(4))
                        info.StartTodaySession = reader.GetString(4);
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

                string todaySession = GetTimeStartTodaySession(name);
                DateTime timeSession;
                //string startTodaySession = DateTime.UtcNow.AddHours(3).ToString();
                //command.Parameters.AddWithValue("@StartTodaySession", startTodaySession);

                if (todaySession != string.Empty)
                {
                    timeSession = Convert.ToDateTime(todaySession);
                    if (timeSession.ToString("d") != DateTime.Now.ToString("d"))
                    {
                        command.CommandText = $"UPDATE Process SET start_session = '{dateTime}'," +
                            $" status = 'works', start_today_session = '{dateTime}' " +
                            $"WHERE name LIKE '{name}'";
                    }
                    else
                    {
                        command.CommandText = $"UPDATE Process SET status = 'works' WHERE name LIKE '{name}'";
                    }
                }
                else
                {
                    command.CommandText = $"UPDATE Process SET start_session = '{dateTime.ToString()}', " +
                        $"status = 'works', start_today_session = '{dateTime}'" +
                        $"WHERE name LIKE '{name}'";
                }
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
        public static void AddIcoPath(string nameProcess, string icoPath)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"UPDATE Process SET ico_path = {icoPath} WHERE name LIKE '{nameProcess}'";

                command.ExecuteNonQuery();
                connection.Close();
            }

        }
        public static void StopWorksProcesess()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"UPDATE Process SET status = 'stoped', end_session = '{DateTime.Now}' WHERE status LIKE 'works'";

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
        
        
        public static ProcessTime GetMaxSumTimeProcessToday()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                Dictionary<ProcessTime,DateTime> sumTimeApps = new Dictionary<ProcessTime, DateTime>();
                List<ProcessTime> procesess = new List<ProcessTime>();

                connection.Open();
                var command = new SqliteCommand();
                command.Connection = connection;
                string todayDate = DateTime.UtcNow.AddHours(3).ToString().Substring(0, 10);

                command.CommandText = $"SELECT sum_time, name FROM Process " +
                $"WHERE start_today_session LIKE '{todayDate}%'";
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ProcessTime processTime = new ProcessTime();
                    processTime.SumTimeProcess = reader.GetString(0);
                    processTime.NameProcess = reader.GetString(1);
                    procesess.Add(processTime);
                }

                
                foreach (var app in procesess)
                {
                    DateTime sumTime;
                    DateTime.TryParseExact(app.SumTimeProcess, "HH:mm:ss", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out sumTime);
                    sumTimeApps.Add(app,sumTime);
                }
                
                connection.Close();
                ProcessTime processTime1;
                if (procesess.Count > 0)
                    processTime1 = procesess.OrderByDescending(x => TimeSpan.Parse(x.SumTimeProcess)).First();
                else
                {
                    processTime1 = new ProcessTime();
                    processTime1.NameProcess = "Собираем статистику";
                }
                return processTime1;
                
            }
        }
        private static bool IfAppCountStartsExists(string name)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand();
                command.CommandText = $"SELECT COUNT(*) FROM DailyCountStartsApp WHERE name_app LIKE '{name}'";

                int result = (int)command.ExecuteScalar();

                if (result > 0)
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


        public static void StartNewDay()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT name_app, today_count FROM DailyCountStartsApp WHERE today_date NOT LIKE '{DateTime.Now.ToString("d")}'";

                var reader = command.ExecuteReader();
                List<ProcessTime>  process = new List<ProcessTime>();
                while (reader.Read())
                {
                    ProcessTime app = new ProcessTime();
                    app.NameProcess = reader.GetString(0);
                    app.TodayCountStarts = reader.GetInt32(1);
                    process.Add(app);
                }
                reader
                    .Close();
                if (process.Count > 0)
                {
                    foreach (ProcessTime item in process)
                    {
                        command.CommandText = $"UPDATE DailyCountStartsApp SET yesterday_count = '{item.TodayCountStarts}' " +
                            $"WHERE name_app LIKE '{item.NameProcess}'";
                        command.ExecuteNonQuery();
                    }

                    command.CommandText = $"UPDATE DailyCountStartsApp SET today_date = '{DateTime.Now.ToString("d")}'";
                    command.ExecuteNonQuery();

                    command.CommandText = $"UPDATE DailyCountStartsApp SET today_count = 0";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        private static void AddCountStartsApp(string name)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand();
                command.Connection = connection;
                command.Parameters.AddWithValue("@name", name);
                command.CommandText = $"INSERT INTO DailyCountStartsApp (name_app, today_count, today_date) values (@name, {1}, '{DateTime.Now.ToString("d")}')";
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        private static int GetDayCountStartsApp(string name)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                int countStarts = -1;
                connection.Open();
                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT today_count FROM DailyCountStartsApp WHERE name_app LIKE '{name}'";

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    countStarts = reader.GetInt32(0);
                }
                
                connection.Close();
                return countStarts;
            }
        }
        public static void UpdateCountStartsApp(string name, string whatDayCount)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                int countStarts = GetDayCountStartsApp(name);
                connection.Open();
                var command = new SqliteCommand();
                command.Connection = connection;

                if (countStarts != -1)
                {
                    countStarts++;
                    command.CommandText = $"UPDATE DailyCountStartsApp SET {whatDayCount} = '{countStarts}' WHERE name_app LIKE '{name}'";
                    command.ExecuteNonQuery();
                }
                else
                    AddCountStartsApp(name);

            }
        }
        public static ProcessTime GetRandomApp(string numberApp, string nameApp)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                ProcessTime process = new ProcessTime();

                connection.Open();
                var command = new SqliteCommand();
                command.Connection = connection;
                if (numberApp == "one")
                {
                    command.CommandText = $"SELECT name_app, today_count FROM DailyCountStartsApp WHERE today_date LIKE '{DateTime.Now.ToString("d")}'" +
                    $"ORDER BY RANDOM() LIMIT 1";
                }
                else
                {
                    command.CommandText = $"SELECT name_app, today_count FROM DailyCountStartsApp WHERE today_date LIKE '{DateTime.Now.ToString("d")}'" +
                    $"AND name_app NOT LIKE '{nameApp}'  ORDER BY RANDOM() LIMIT 1";
                }
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    process.NameProcess = reader.GetString(0);
                    process.TodayCountStarts = reader.GetInt32(1);
                }
                connection.Close();
                return process;

            }
        }
        public static bool IfExistsTodayStartUsingPc(string date)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT COUNT(*) FROM DurationUsingPc WHERE date LIKE '{date}'";
                
                long exists = (long)command.ExecuteScalar();
                if (exists > 0)
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
        
        public static void UpdateSumTimeUsingPc(string date, string sumTime)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"UPDATE DurationUsingPc SET sum_time = '{sumTime}' WHERE date LIKE '{date}'";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public static void StartTodayUsingPc(string date)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand();
                command.Connection = connection;
                if (!IfExistsTodayStartUsingPc(date))
                {
                    command.Parameters.AddWithValue("@start_time",$"{DateTime.Now}");
                    command.Parameters.AddWithValue("@date", $"{DateTime.Now.ToString("d")}");
                    command.CommandText = $"INSERT INTO DurationUsingPc (start_time, date) values (@start_time, @date)";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        public static void EndTodayUsingPc(string date)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"UPDATE DurationUsingPc SET end_time = '{DateTime.Now}'" +
                    $" WHERE date LIKE '{date}'";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static string GetSumTimeUsingPcToday(string date)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                string time = string.Empty;
                connection.Open();

                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT sum_time FROM DurationUsingPc WHERE date LIKE '{date}' ";

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
        public static string GetFirstStartsUsingPcToday(string date)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT start_time FROM DurationUsingPc WHERE date LIKE '{date}'";
                string startTime = string.Empty;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    startTime = reader.GetString(0);
                }
                connection.Close();
                return startTime;
            }
        }


    }
}
