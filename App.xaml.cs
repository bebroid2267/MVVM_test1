using MVVM_test1.Utilities;
using MVVM_test1.View;
using MVVM_test1.ViewModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using Forms = System.Windows.Forms;

namespace MVVM_test1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public App()
        {
            _notifyIcon = new Forms.NotifyIcon();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            _notifyIcon.Icon = new System.Drawing.Icon("View/icontray.ico");
            _notifyIcon.Visible = true;
            
            _notifyIcon.MouseClick += _notifyIcon_Click;
            _notifyIcon.BalloonTipClicked += BalloonTipClicked;
            NotifyIconMessage.AddNotifyIcon(_notifyIcon);
            _notifyIcon.ContextMenuStrip = new Forms.ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("Открыть", Image.FromFile("View/icontray.ico"), OnStatusClicked);
            _notifyIcon.ContextMenuStrip.Items.Add("Стоп трекинг", 
                Image.FromFile("Utilities/pics/Free_Flat_Exit_Door_Icon.ico"),OnCloseClicked);

            base.OnStartup(e);
        }

        private void BalloonTipClicked(object? sender, EventArgs e)
        {
            MainWindow.Show();
        }

        private void OnStatusClicked(object? sender, EventArgs e)
        {
            if (CheckPages.GetHomePage().DataContext is IClosable closable)
                closable.Start();
            if (CheckPages.GetSoftsPage().DataContext is IClosable _closable)
                _closable.Start();
            MainWindow.Show();
        }
        private void OnCloseClicked(object? sender, EventArgs e)
        {
            MainWindow.Close();
        }

        private void _notifyIcon_Click(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CheckPages.GetHomePage().DataContext is IClosable closable)
                    closable.Start();
                if (CheckPages.GetSoftsPage().DataContext is IClosable _closable)
                    _closable.Start();

                MainWindow.WindowState = WindowState.Normal;
                MainWindow.Activate();

                MainWindow.Show();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose();
            base.OnExit(e);
        }
        private readonly Forms.NotifyIcon _notifyIcon;

    }

}
