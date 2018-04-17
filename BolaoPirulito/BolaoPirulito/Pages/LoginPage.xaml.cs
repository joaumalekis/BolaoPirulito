using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BolaoPirulito.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BolaoPirulito.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
		    NavigationPage.SetHasNavigationBar(this, false);

		    NavigationPage.SetBackButtonTitle(this, "Login");

            InitializeComponent ();

		    BindingContext = new LoginViewModel(Navigation);
        }
	}
}