using BolaoPirulito.Pages;
using Xamarin.Forms;

namespace BolaoPirulito
{
    public partial class App : Application
    {
        public App()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            NavigationPage.SetBackButtonTitle(this, "Login");

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