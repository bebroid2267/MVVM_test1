﻿using MVVM_test1.DataBase;
using MVVM_test1.ProcessJobs;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_test1.ViewModel
{
    public class DailyProcessJobsModel : BindableBase
    {
        public DailyProcessJobsModel()
        {
            RunningProcesses = _RunningProcesess;
            // создание в конструкторе класса движка питона и процесс экзекуции епта кода питона в обьект scope
            //engine = Python.CreateEngine();
            //scope = engine.CreateScope();
            //engine.ExecuteFile(@"C:\Users\кирилл\source\repos\MVVM_test1\IronPython\sqlite.py", scope);
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

        public async void MonitorProcesess(object sender , EventArgs e)
        {

            ProcessWindow[] applications = await Task.Run(() => ProcessHelper.GetRunningApplications());

            if (applications != null)
            {
                foreach (ProcessWindow process in applications)
                {
                    //dynamic addTask = scope.GetVariable("add_task"); //берем нужную функцию и закидываем в переменную
                    //addTask(process.Process.ProcessName); // вызывает функцию с нужным аргументом
                    if (!NameProcesessDontCheck.Contains(process.Process.ProcessName))
                    {

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
        
        private void UpdateSumTimeProcessEveryTime(string nameProcess, string sumTime, string date)
        {
            bool doesProcessWorks = DateBase.CheckProcessDoesWorks(nameProcess);

            if (doesProcessWorks)
                DateBase.UpdateTimeProcessDaily(nameProcess, date, sumTime);
        }

        

        private void StartProcess(string nameProcess)
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                RunningProcesses.Add(nameProcess);
                OnPropertyChanged(nameof(RunningProcesses));
            });

            DateTime startTimeProcess = DateTime.Now;


            Process[] processes = Process.GetProcessesByName(nameProcess);
            Process process = Process.GetProcessById(processes[0].Id);


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
                    string firstTimeProcess = DateBase.GetSumTimeAppToDailyInfo(nameProcess, DateTime.Now.ToString("d"));

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
                    UpdateSumTimeProcessEveryTime(nameProcess, totalTimeSpend, DateTime.Now.ToString("d"));
                    startTimeProcess = DateTime.Now;
                }

                Thread.Sleep(5000);
            }


        }
        private void StopProcess(DateTime startTimeProcess, string nameProcess)
        {
            string firstTimeProcess = DateBase.GetSumTimeAppToDailyInfo(nameProcess, DateTime.Now.ToString("d"));

            DateTime firstTimePrcs = new DateTime();

            if (DateTime.TryParseExact(firstTimeProcess, "HH:mm:ss", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out firstTimePrcs))
            {
                TimeSpan time = DateTime.Now - startTimeProcess;

                firstTimePrcs = firstTimePrcs.Add(time);
                string totalTimeSpend = firstTimePrcs.ToString("HH:mm:ss");


            }
            else if (firstTimeProcess == null || firstTimeProcess == string.Empty)
            {
                TimeSpan timePassed = DateTime.Now - startTimeProcess;
                DateTime time = DateTime.Today.Add(timePassed);
                DateTime timePassedPrcs = new DateTime();

                DateTime.TryParseExact(time.ToString("HH':'mm':'ss"), "HH:mm:ss", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out timePassedPrcs);


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

        private ObservableCollection<string> RunningProcesses = new ObservableCollection<string>();
        private ObservableCollection<string> CheckTimeProcesess = new ObservableCollection<string>();
        private List<string> NameProcesessDontCheck = new List<string> { "ApplicationFrameHost", "devenv", "SystemSettings", "TextInputHost", };
    }
}
