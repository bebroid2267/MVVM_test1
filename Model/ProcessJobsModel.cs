using MVVM_test1.DataBase;
using MVVM_test1.ProcessJobs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Prism;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

using IronPython.SQLite;
using System.IO;
using System.Drawing;

namespace MVVM_test1.Model
{
    public class ProcessJobsModel : BindableBase
    {
        public ProcessJobsModel() 
        {
            RunningProcesses = _RunningProcesess;
        }

        public ObservableCollection<string> _RunningProcesess
        {
            
            get { return RunningProcesses; }
            set
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                { 
                    RunningProcesses = value;
                    OnPropertyChanged(nameof(RunningProcesses));
                });
            }
        }

        public async void MonitorProcesess(object sender, EventArgs e)
        {
            
            ProcessWindow[] applications = await Task.Run(() => ProcessHelper.GetRunningApplications());

            if (applications != null)
            { 
                foreach (ProcessWindow process in applications)
                {
                    if (!NameProcesessDontCheck.Contains(process.Process.ProcessName))
                    {
                        DateBase.AddProcess(process.Process.ProcessName, DateTime.Now.ToString());

                        if (!RunningProcesses.Contains(process.Process.ProcessName))
                        {
                            Application.Current.Dispatcher.InvokeAsync(async () =>
                            {
                                await Task.Run(() => StartProcess(process.Process.ProcessName));
                            });
                        }
                    }
                }
            }

        }
        private void UpdateSumTimeProcessEveryTime(string nameProcess, string sumTime)
        {
            bool doesProcessWorks = DateBase.CheckProcessDoesWorks(nameProcess);

            if (doesProcessWorks)
                DateBase.UpdateTimeProcess(nameProcess, sumTime);
        }

        private void GetIcoPic(string filePath, Process process)
        {
            if (!File.Exists(filePath))
            {
                try
                {   
                    string test =  process.MainModule.FileName;
                    Icon icon = Icon.ExtractAssociatedIcon(process.MainModule.FileName);
                    if (icon != null)
                    {
                        using (FileStream fs = new FileStream(filePath, FileMode.Create))
                        {
                            icon.Save(fs);                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void StartProcess(string nameProcess)
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                RunningProcesses.Add(nameProcess);
                OnPropertyChanged(nameof(RunningProcesses));
            });

            DateTime startTimeProcess = DateTime.Now;
            DateBase.StartSession(nameProcess,DateTime.Now.ToString());
            DateBase.UpdateCountStartsApp(nameProcess, "today_count");


            Process[] processes = Process.GetProcessesByName(nameProcess);
            Process process = Process.GetProcessById(processes[0].Id);

            string projectDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(projectDirectory, process.ProcessName + ".ico");

            Task.Run(() => GetIcoPic(filePath, process));

            while (true)
            {
                    processes = Process.GetProcessesByName(nameProcess);

                    if (processes.Length == 0)
                    {
                        StopProcess(startTimeProcess, nameProcess);
                        Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            RunningProcesses.Remove(nameProcess);
                            OnPropertyChanged(nameof(RunningProcesses));
                        });
                        break;
                    }
                    else
                    {
                        string totalTimeSpend = string.Empty;
                        string firstTimeProcess = DateBase.GetTimeProcess(nameProcess);

                        if (firstTimeProcess == null)
                            firstTimeProcess = "00:00:00";

                        DateTime firstTimePrcs = new DateTime();

                        if (DateTime.TryParseExact(firstTimeProcess, "HH:mm:ss", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out firstTimePrcs))
                        {
                            TimeSpan time = DateTime.Now - startTimeProcess;

                            firstTimePrcs = firstTimePrcs.Add(time);
                            totalTimeSpend = firstTimePrcs.ToString("HH:mm:ss");
                        }
                        else
                        {
                            TimeSpan timePassed = DateTime.Now - startTimeProcess;
                            DateTime time = DateTime.Today.Add(timePassed);
                            DateTime timePassedPrcs = new DateTime();

                            DateTime.TryParseExact(time.ToString("HH':'mm':'ss"), "HH:mm:ss", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out timePassedPrcs);
                            totalTimeSpend = timePassedPrcs.ToString("HH:mm:ss");
                        }
                        UpdateSumTimeProcessEveryTime(nameProcess, totalTimeSpend);
                        startTimeProcess = DateTime.Now;
                    }
                
                    Thread.Sleep(3000);
                }
            

        }
        private void StopProcess(DateTime startTimeProcess, string nameProcess)
        {
            string firstTimeProcess = DateBase.GetTimeProcess(nameProcess);

            DateTime firstTimePrcs = new DateTime();

            if (DateTime.TryParseExact(firstTimeProcess, "HH:mm:ss", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out firstTimePrcs))
            {
                TimeSpan time = DateTime.Now - startTimeProcess;

                firstTimePrcs = firstTimePrcs.Add(time);
                string totalTimeSpend = firstTimePrcs.ToString("HH:mm:ss");
                DateBase.StopSession(nameProcess, DateTime.Now.ToString(),totalTimeSpend);
                
                //Console.WriteLine($"Программа: {nameProcess} завершила свою работу. она работает уже в формате часы, минуты, секунды: {totalTimeSpend}");

            }
            else if (firstTimeProcess == null || firstTimeProcess == string.Empty)
            {
                TimeSpan timePassed = DateTime.Now - startTimeProcess;
                DateTime time = DateTime.Today.Add(timePassed);
                DateTime timePassedPrcs = new DateTime();

                DateTime.TryParseExact(time.ToString("HH':'mm':'ss"), "HH:mm:ss", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out timePassedPrcs);

                DateBase.StopSession(nameProcess, DateTime.Now.ToString(), timePassedPrcs.ToString("HH:mm:ss"));

                //DateBase.UpdateTimeProcess(nameProcess, timePassedPrcs.ToString("HH:mm:ss"));

                //Console.WriteLine($"Программа: {nameProcess} завершила свою работу. она проработала: часы {timePassed.Hours} минут {timePassed.Minutes} секунд {timePassed.Seconds}");
            }
            CheckTimeProcesess.Remove(nameProcess);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        
        private string TotalTimeSpendForCheck = string.Empty;

        private ObservableCollection<string> RunningProcesses = new ObservableCollection<string>();
        private ObservableCollection<string> CheckTimeProcesess = new ObservableCollection<string>();
        private List<string> NameProcesessDontCheck = new List<string> { "ApplicationFrameHost", "devenv", "SystemSettings", "TextInputHost", };
    }
}
