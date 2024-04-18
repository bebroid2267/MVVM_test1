using MVVM_test1.Utilities;
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
            bool existsHome = CheckPages.IfExistsHomePage();
            if (existsHome)
                HomePage = CheckPages.GetHomePage();
            else if (!existsHome)
            {
                HomePage = new();
                CheckPages.AddHomePage(HomePage);
            }
            Switcher.Switch(HomePage);
        }
        private void NavigationToAllSoft(object sender, RoutedEventArgs e)
        {
            bool existsPages = CheckPages.IfExistsSoftsPage();
            if (existsPages)
                SoftsPage = CheckPages.GetSoftsPage();
            else if (!existsPages)
            {
                CheckPages.AddSoftsPage(SoftsPage = new());
            }
            Switcher.Switch(SoftsPage);
        }
        private HomeView HomePage;
        private AllSoft SoftsPage;
    }
}
