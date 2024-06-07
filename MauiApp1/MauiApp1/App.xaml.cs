using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using Application = Microsoft.Maui.Controls.Application;

namespace MauiApp1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var dbService = new DbService(); 
            var startPage = new Views.StartPage(dbService);
            MainPage = new NavigationPage(startPage); 
        }
    }
}
