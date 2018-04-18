using System;
using BolaoPirulito.Interfaces;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BolaoPirulito.Models;
using BolaoPirulito.Pages;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BolaoPirulito.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IFirebaseAuthenticator _firebaseAuthenticator;

        public LoginViewModel(INavigation navigation) : base(navigation)
        {
            _firebaseAuthenticator = DependencyService.Get<IFirebaseAuthenticator>();
        }

        private string _email = "joao@joao.com";

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _password = "123456";

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private Command _loginCommand;

        public Command LoginCommand =>
            _loginCommand ?? (_loginCommand = new Command(async () => await LoginCommandExecute()));

        private async Task LoginCommandExecute()
        {
            try
            {
                App.Token = await _firebaseAuthenticator.LoginWithEmailPassword(Email, Password);
                //var firebase = new FirebaseClient("https://bolaopirulito.firebaseio.com/", new FirebaseOptions { AuthTokenAsyncFactory = () => Task.Delay(100).ContinueWith(t => App.Token)});
                //var json = JsonConvert.SerializeObject(new Time { Nome = "Chapecoense" });
                //await firebase.Child("Times").PostAsync(json);
                await Navigation.PushAsync(new HomePage());
            }
            catch (Exception e)
            {
                UserDialogs.Instance.Alert(e.Message);
            }
        }
    }
}