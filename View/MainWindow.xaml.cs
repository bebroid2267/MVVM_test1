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
            DataContext = new MainVM();
            Switcher.pageSwitcher = this;
            Switcher.Switch(new HomeView());
        }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }
    }
}