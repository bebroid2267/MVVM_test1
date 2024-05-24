using MVVM_test1.DataBase;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_test1.Model
{
    public class FastStatMoreUsingAppEver : BindableBase, INotifyPropertyChanged
    {
        public FastStatMoreUsingAppEver()
        {
            MoreUsingApp = _MoreUsingApp;
        }
        public ObservableCollection<ProcessTime> _MoreUsingApp
        {
            get { return MoreUsingApp; }
            set
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    MoreUsingApp = value;
                    OnPropertyChanged(nameof(MoreUsingApp));
                });
            }
        }
        public void GetMoreUsingApp()
        {
            List<ProcessTime> procesess = DateBase.GetInfoProcess("all", "no");
            ProcessTime moreUseApp = new();
            if (procesess.Count > 0)
            {
                moreUseApp = procesess.OrderByDescending(x =>
                TimeSpan.Parse(x.SumTimeProcess)).First();

                GetProcessModel processModel = new GetProcessModel();
                if (processModel.GetDirectoryIco(moreUseApp) != null)
                    moreUseApp.IcoPath = processModel.GetDirectoryIco(moreUseApp);
            }
            else
                moreUseApp.NameProcess = "Собираем статистику";

            if (MoreUsingApp.Count > 0)
                MoreUsingApp[0] = moreUseApp;
            else
                MoreUsingApp.Add(moreUseApp);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private ObservableCollection<ProcessTime> MoreUsingApp = new ObservableCollection<ProcessTime>();
    }
}
