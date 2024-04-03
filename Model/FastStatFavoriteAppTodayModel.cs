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
    public class FastStatFavoriteAppTodayModel : BindableBase, INotifyPropertyChanged
    {

        public FastStatFavoriteAppTodayModel()
        {
            FavoriteApp = _FavAppToday;
        }
        public ObservableCollection<ProcessTime> _FavAppToday
        {
            get { return FavoriteApp; }
            set
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                { 
                    FavoriteApp = value;
                    OnPropertyChanged(nameof(FavoriteApp));
                });
            }
        }
        public void GetFavoriteApp()
        {
            ProcessTime process = DateBase.GetMaxSumTimeProcessToday();
            GetProcessModel getProcessModel = new GetProcessModel();
            string icoPath = getProcessModel.GetDirectoryIco(process);

            if (icoPath != null)
                process.IcoPath = icoPath;
            if (_FavAppToday.Count > 0)
                _FavAppToday[0] = process;
            
            _FavAppToday.Add(process);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private ObservableCollection<ProcessTime> FavoriteApp = new ObservableCollection<ProcessTime>();
    }
    
}
