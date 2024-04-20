using GalaSoft.MvvmLight.Command;
using MaterialDesignThemes.Wpf;
using MVVM_test1.Model;
using MVVM_test1.Utilities;
using MVVM_test1.View;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;



namespace MVVM_test1.ViewModel
{
    public class AllSoftVM : Utilities.ViewModelBase
    {
        public ProcessTime _SelectedApp
        {
            get { return SelectedApp; }
            set
            {
                SelectedApp = value;
                OnPropertyChanged(nameof(SelectedApp));
            }
        }
        public ObservableCollection<ProcessTime> _Apps
        {
            get { return Apps._Apps; }
            set
            {
                Apps._Apps = value;
                OnPropertyChanged(nameof(_Apps));
            }
        }
        private void OpenPopUp(object parameter)
        {
            if (parameter is ProcessTime selectedApp)
            {

                ApplyEffect(Application.Current.MainWindow);
                PopUpVm = new PopUpAppInfoVM(selectedApp.NameProcess);

                // Создаем новое окно и помещаем в него UserControl
                PopUpView = new PopUpAppInfoView();
                PopUpView.DataContext = PopUpVm;

                Window popUpWindow = new Window
                {
                    Content = PopUpView,
                    Width = 500,
                    Height = 400,
                    WindowStyle = WindowStyle.None,
                    Background = System.Windows.Media.Brushes.Transparent,
                    AllowsTransparency = true,
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                
                popUpWindow.Deactivated += PopUpWindow_Deactivated;

                popUpWindow.Loaded += (sender, e) =>
                {
                    // Подписываемся на событие MouseDown основного окна при загрузке PopUp
                    popUpWindow.Owner.MouseDown += OwnerWindow_MouseDown;
                };

                popUpWindow.Closed += (sender, e) =>
                {
                    ClearEffect(Application.Current.MainWindow);
                    // Отписываемся от события MouseDown при закрытии PopUp
                    popUpWindow.Owner.MouseDown -= OwnerWindow_MouseDown;
                };

                ((PopUpAppInfoView)popUpWindow.Content).DataContext = PopUpVm;
                // Устанавливаем владельца окна, чтобы обеспечить правильную модальность и поведение Deactivated

                

                popUpWindow.ShowDialog();
            }
        }
        private void ApplyEffect(ContentControl win)
        {
            System.Windows.Media.Effects.BlurEffect objBlur = new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = 11;
            win.Effect = objBlur;
        }

        private void ClearEffect(ContentControl win)
        {
            win.Effect = null;
        }

        private void OwnerWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Проверяем хит-тест, чтобы увидеть, находится ли курсор над PopUpWindow
            if (!PopUpView.IsMouseOver)
            {
                // Если курсор не над PopUp, закрываем PopUpWindow
                PopUpView.IsEnabled = false ;
            }
        }

        private void PopUpWindow_Deactivated(object sender, EventArgs e)
        {
            Window popUpWindow = sender as Window;
            if (popUpWindow != null)
            {
                // Закрываем окно при потере фокуса
                popUpWindow.Close();
            }
        }




        public AllSoftVM() 
        {
            ShowMiniPageApp = new RelayCommand<object>(OpenPopUp);
            Apps = new AllSoftModel();

            System.Timers.Timer timerCheckProcess = new System.Timers.Timer(3000);
            timerCheckProcess.Elapsed += Apps.GetApps;
            timerCheckProcess.Start();
        }
        public ICommand ShowMiniPageApp { get;  set; }
        private ProcessTime SelectedApp;
        public AllSoftModel Apps;
        private PopUpAppInfoVM PopUpVm;
        private PopUpAppInfoView PopUpView;
        
    }
}
