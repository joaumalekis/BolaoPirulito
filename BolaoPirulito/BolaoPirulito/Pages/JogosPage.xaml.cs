using BolaoPirulito.Models;
using BolaoPirulito.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BolaoPirulito.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JogosPage : ContentPage
    {
        public JogosPage(Aposta aposta)
        {
            InitializeComponent();
            BindingContext = new JogosViewModel(Navigation, aposta);
        }
    }
}