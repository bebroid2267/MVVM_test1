using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVM_test1.View
{
    /// <summary>
    /// Логика взаимодействия для SwitcherMenu.xaml
    /// </summary>
    public partial class SwitcherMenu : UserControl
    {
        public SwitcherMenu()
        {
            InitializeComponent();
        }

        private void NavigationToHome(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new HomeView());
        }
        private void NavigationToAllSoft(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new AllSoft());
        }
    }
}
