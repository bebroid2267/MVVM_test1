using MVVM_test1.View;
using MVVM_test1.ViewModel;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_test1.Utilities
{
    public static class CheckPages
    {
        private static AllTimesDateVM AllTimesPage;
        private static Dictionary<string, List7DaysVM> SevenDaysAppsInfo = new Dictionary<string, List7DaysVM>();
        private static HomeView HomePage;
        private static AllSoft SoftsPage;
        private static AnalyticsByDate DashboardPage;
        public static AllTimesDateVM GetAllTimesPage()
        {
            return AllTimesPage;
        }
        public static void AddAllTimesPage(AllTimesDateVM vm)
        {
            if (AllTimesPage == null)
            {
                AllTimesPage = vm;
            }
        }
        public static bool IfExistsAllTimesPage()
        {
            if (AllTimesPage == null)
                return false;
            else
                return true;
        }
        public static List7DaysVM GetLast7Days(string date)
        {
            return SevenDaysAppsInfo[date];
        }
        
        public static void AddSevenDaysInfo(List7DaysVM vm, string date)
        {
            if (SevenDaysAppsInfo.Count == 0)
            {
                SevenDaysAppsInfo.Add(date, vm);
            }
        }
        public static bool IfExistsSevenDaysAppsInfo(string date)
        {
            if (!SevenDaysAppsInfo.ContainsKey(date))
                return false;
            else
                return true;
        }
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
