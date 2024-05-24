using MVVM_test1.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceProcess;
using System.Reflection;
using System.Configuration.Install;
using MVVM_test1.View;
using MVVM_test1.Utilities;

namespace MVVM_test1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (!CheckPages.IfExistsMainWindow())
                CheckPages.AddMainWindow(this);
            DataContext = new MainVM();
            Switcher.pageSwitcher = this;
            HomeView view = new HomeView();
            CheckPages.AddHomePage(view);
            Switcher.Switch(view);
            Closing += (DataContext as MainVM).ClosingWorksProcesess;
        }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }
        
    }
}