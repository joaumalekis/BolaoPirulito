using Acr.UserDialogs;
using BolaoPirulito.Interfaces;
using BolaoPirulito.Models;
using BolaoPirulito.Pages;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
                //await InserirApostadores();
                //await InsertAllRodadas();
                App.ApostadorLogado = await GetApostadorLogado(Email);
                await Navigation.PushAsync(new HomePage());
            }
            catch (Exception e)
            {
                UserDialogs.Instance.Alert(e.Message);
            }
        }

        private async Task<Apostador> GetApostadorLogado(string email)
        {
            var firebase = new FirebaseClient(App.BaseUrl, new FirebaseOptions { AuthTokenAsyncFactory = () => Task.Delay(100).ContinueWith(t => App.Token) });
            var apostadores = await firebase
                .Child("Apostadores")
                .OrderByKey()
                .OnceAsync<Apostador>();
            return apostadores.ToList().FirstOrDefault(p => p.Object.Email.Equals(email))?.Object;
        }

        public async Task<Jogo[]> GetAllRodadas(int rodada)
        {
            var url = $"https://api.globoesporte.globo.com/tabela/d1a37fa4-e948-43a6-ba53-ab24ab3a45b1/fase/fase-unica-seriea-2019/rodada/{rodada}/jogos";
            var uri = new Uri(url);

            var _client = new HttpClient();

            using (var response = await _client.GetAsync(uri))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Jogo[]>(content);
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Forbidden:
                    case HttpStatusCode.Conflict:
                    case HttpStatusCode.NotFound:
                        throw new ArgumentException(response.Content.ReadAsStringAsync().Result);
                    case HttpStatusCode.Unauthorized:
                        throw new Exception("Token expirado");
                    default:
                        throw new ArgumentException(response.ReasonPhrase);
                }
            }
        }

        public async Task InsertAllRodadas()
        {
            for (int i = 1; i <= 38; i++)
            {
                var jogos = await GetAllRodadas(i);

                var rodada = new Rodada
                {
                    //Id = Guid.NewGuid(),
                    NumeroRodada = i,
                    Jogos = jogos
                };

                var firebase = new FirebaseClient(App.BaseUrl, new FirebaseOptions { AuthTokenAsyncFactory = () => Task.Delay(100).ContinueWith(t => App.Token) });

                await firebase.Child("Jogos").Child(rodada.Id.ToString).PutAsync(rodada);

            }
        }

        //private async Task InserirTimesSerieA2018()
        //{
        //    var times = new[]
        //    {
        //        "São Paulo",
        //        "Internacional",
        //        "Flamengo",
        //        "Palmeiras",
        //        "Grêmio",
        //        "Atlético-MG",
        //        "Cruzeiro",
        //        "Corinthians",
        //        "América-MG",
        //        "Fluminense",
        //        "Bahia",
        //        "Botafogo",
        //        "Atlético-PR",
        //        "Santos",
        //        "Vasco",
        //        "Vitória",
        //        "Chapecoense",
        //        "Sport",
        //        "Ceará",
        //        "Paraná"
        //    };

        //    foreach (var time in times)
        //    {
        //        var firebase = new FirebaseClient(App.BaseUrl, new FirebaseOptions { AuthTokenAsyncFactory = () => Task.Delay(100).ContinueWith(t => App.Token) });
        //        var id = Guid.NewGuid();
        //        await firebase.Child("Times").Child(id.ToString()).PutAsync(new Time { Id = id, Nome = time });
        //    }
        //}

        //private async Task InserirTimesSerieA2019()
        //{
        //    var times = new[]
        //    {
        //        "Athletico-PR",
        //        "Atlético-MG",
        //        "Avaí",
        //        "Bahia",
        //        "Botafogo",
        //        "CSA",
        //        "Ceará",
        //        "Chapecoense",
        //        "Corinthians",
        //        "Cruzeiro",
        //        "Flamengo",
        //        "Fluminense",
        //        "Fortaleza",
        //        "Goiás",
        //        "Grêmio",
        //        "Internacional",
        //        "Palmeiras",
        //        "Santos",
        //        "São Paulo",
        //        "Vasco"
        //    };

        //    foreach (var time in times)
        //    {
        //        var firebase = new FirebaseClient(App.BaseUrl, new FirebaseOptions { AuthTokenAsyncFactory = () => Task.Delay(100).ContinueWith(t => App.Token) });
        //        var id = Guid.NewGuid();
        //        await firebase.Child("Times").Child(id.ToString()).PutAsync(new Time { Id = id, Nome = time });
        //    }
        //}

        private async Task InserirApostadores()
        {
            var apostadores = new List<Apostador>
            {
                new Apostador { Id = Guid.NewGuid(), Nome = "João", Email = "joao@joao.com" },
                new Apostador { Id = Guid.NewGuid(), Nome = "Leonardo", Email = "leonardo@leonardo.com" },
                new Apostador { Id = Guid.NewGuid(), Nome = "Celso", Email = "celso@celso.com" },
                new Apostador { Id = Guid.NewGuid(), Nome = "Juliano", Email = "juliano@juliano.com" },
                new Apostador { Id = Guid.NewGuid(), Nome = "amarildo", Email = "amarildo@amarildo.com" },
                new Apostador { Id = Guid.NewGuid(), Nome = "Ozório", Email = "ozorio@ozorio.com" },
                new Apostador { Id = Guid.NewGuid(), Nome = "Ruan", Email = "ruan@ruan.com" },
                new Apostador { Id = Guid.NewGuid(), Nome = "Matheus", Email = "matheus@matheus.com" },
                new Apostador { Id = Guid.NewGuid(), Nome = "Elaudir", Email = "elaudir@elaudir.com" },
                new Apostador { Id = Guid.NewGuid(), Nome = "Douglas", Email = "douglas@douglas.com" },
                new Apostador { Id = Guid.NewGuid(), Nome = "Fábio", Email = "fabio@fabio.com" },
            };

            foreach (var apostador in apostadores)
            {
                var firebase = new FirebaseClient(App.BaseUrl, new FirebaseOptions { AuthTokenAsyncFactory = () => Task.Delay(100).ContinueWith(t => App.Token) });

                await firebase.Child("Apostadores").Child(apostador.Id.ToString).PutAsync(apostador);
            }
        }
    }
}