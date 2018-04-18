using BolaoPirulito.Pages;
using Xamarin.Forms;

namespace BolaoPirulito
{
    public partial class App : Application
    {
        public static string Token { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage()) { BarTextColor = Color.White };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}