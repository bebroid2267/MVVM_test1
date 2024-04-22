using MVVM_test1.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_test1.Utilities
{
    public static class CheckPages
    {
        private static HomeView HomePage;
        private static AllSoft SoftsPage;
        private static AnalyticsByDate DashboardPage;
        public static bool IfExistsDashboardPage()
        {
            if (DashboardPage == null)
                return false;
            else
                return true;
        }
        public static void AddDashboardPage(AnalyticsByDate dashboardPage)
        {
            if (DashboardPage == null)
            {
                DashboardPage = dashboardPage;
            }
        }
        public static AnalyticsByDate GetDashboard()
        {
            return DashboardPage;
        }
        public static HomeView GetHomePage()
        {
            return HomePage;
        }
        public static AllSoft GetSoftsPage()
        {
            return SoftsPage;
        }
        public static void AddHomePage(HomeView homePage)
        {
            if (HomePage == null)
            {
                HomePage = homePage;
            }
        }
        public static bool IfExistsHomePage()
        {
            if (HomePage == null)
                return false;
            else
                return true;
        }
        public static void AddSoftsPage(AllSoft softsPage)
        {
            if (SoftsPage == null)
            {
                SoftsPage = softsPage;
            }
        }
        public static bool IfExistsSoftsPage()
        {
            if (SoftsPage == null)
                return false;
            else
                return true;
        }
    }
}
