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
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();
		    BindingContext = new HomeViewModel(Navigation);
        }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        if (((HomeViewModel)BindingContext).Itens.Count == 0)
	            ((HomeViewModel)BindingContext).RefreshCommand.Execute(false);
	    }
    }
}