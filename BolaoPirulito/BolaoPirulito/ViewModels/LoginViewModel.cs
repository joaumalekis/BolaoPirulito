using Acr.UserDialogs;
using BolaoPirulito.Interfaces;
using BolaoPirulito.Models;
using BolaoPirulito.Pages;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                await InserirApostadores();
                await InserirRodadas();
                await InserirTimesSerieA2019();
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

        private async Task InserirTimesSerieA2018()
        {
            var times = new[]
            {
                "São Paulo",
                "Internacional",
                "Flamengo",
                "Palmeiras",
                "Grêmio",
                "Atlético-MG",
                "Cruzeiro",
                "Corinthians",
                "América-MG",
                "Fluminense",
                "Bahia",
                "Botafogo",
                "Atlético-PR",
                "Santos",
                "Vasco",
                "Vitória",
                "Chapecoense",
                "Sport",
                "Ceará",
                "Paraná"
            };

            foreach (var time in times)
            {
                var firebase = new FirebaseClient(App.BaseUrl, new FirebaseOptions { AuthTokenAsyncFactory = () => Task.Delay(100).ContinueWith(t => App.Token) });
                var id = Guid.NewGuid();
                await firebase.Child("Times").Child(id.ToString()).PutAsync(new Time { Id = id, Nome = time });
            }
        }

        private async Task InserirTimesSerieA2019()
        {
            var times = new[]
            {
                "Athletico-PR",
                "Atlético-MG",
                "Avaí",
                "Bahia",
                "Botafogo",
                "CSA",
                "Ceará",
                "Chapecoense",
                "Corinthians",
                "Cruzeiro",
                "Flamengo",
                "Fluminense",
                "Fortaleza",
                "Goiás",
                "Grêmio",
                "Internacional",
                "Palmeiras",
                "Santos",
                "São Paulo",
                "Vasco"
            };

            foreach (var time in times)
            {
                var firebase = new FirebaseClient(App.BaseUrl, new FirebaseOptions { AuthTokenAsyncFactory = () => Task.Delay(100).ContinueWith(t => App.Token) });
                var id = Guid.NewGuid();
                await firebase.Child("Times").Child(id.ToString()).PutAsync(new Time { Id = id, Nome = time });
            }
        }

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

        private async Task InserirRodadas()
        {
            var firebase = new FirebaseClient(App.BaseUrl, new FirebaseOptions { AuthTokenAsyncFactory = () => Task.Delay(100).ContinueWith(t => App.Token) });
            var id = Guid.NewGuid();
            await firebase.Child("Rodadas").Child(id.ToString()).PutAsync(new Rodada
            {
                Id = id,
                Nome = $"Rodada {1}",
                NumeroRodada = 1,
                Jogos = new List<Jogo>
                    {
                        new Jogo
                        {
                            Id = Guid.NewGuid(),
                            TimeA = new Time{Id = Guid.Parse("5a52bb3f-e53e-42ab-bea4-6296aad5b10a"), Nome = "Ceará"},
                            TimeB = new Time{Id = Guid.Parse("eebd6b4c-299b-4c29-8ccc-f6053210125a"), Nome = "Santos"},
                            GolsTimeA = 0,
                            GolsTimeB = 0,
                        },
                        new Jogo
                        {
                            Id = Guid.NewGuid(),
                            TimeA = new Time{Id = Guid.Parse("dab5dac6-8b3a-4725-a61e-d50c9b8d3bda"), Nome = "São Paulo"},
                            TimeB = new Time{Id = Guid.Parse("7d17bdd6-9fac-4a7a-81c3-7e3d13a9db19"), Nome = "Paraná"},
                            GolsTimeA = 0,
                            GolsTimeB = 0
                        },
                        new Jogo
                        {
                            Id = Guid.NewGuid(),
                            TimeA = new Time{Id = Guid.Parse("b10537a5-3633-4901-b090-fffee655dfd3"), Nome = "Bahia"},
                            TimeB = new Time{Id = Guid.Parse("31416414-d584-44cc-979f-bec596e27182"), Nome = "Internacional"},
                            GolsTimeA = 0,
                            GolsTimeB = 0
                        },
                        new Jogo
                        {
                            Id = Guid.NewGuid(),
                            TimeA = new Time{Id = Guid.Parse("537351be-b54a-4753-a413-0a628ea5dd79"), Nome = "Sport"},
                            TimeB = new Time{Id = Guid.Parse("f909c6b8-27bb-4d4d-9af2-705374f1bdb0"), Nome = "América-MG"},
                            GolsTimeA = 0,
                            GolsTimeB = 0
                        },
                        new Jogo
                        {
                            Id = Guid.NewGuid(),
                            TimeA = new Time{Id = Guid.Parse("29f92bb7-4ea0-4a59-941e-12222479098d"), Nome = "Palmeiras"},
                            TimeB = new Time{Id = Guid.Parse("d4a44afa-00b3-444c-9d57-dff8c25e946d"), Nome = "Botafogo"},
                            GolsTimeA = 0,
                            GolsTimeB = 0
                        },
                        new Jogo
                        {
                            Id = Guid.NewGuid(),
                            TimeA = new Time{Id = Guid.Parse("c613d563-443a-4668-8675-4cd4e27a30d6"), Nome = "Fluminense"},
                            TimeB = new Time{Id = Guid.Parse("0bcaa928-92b9-4bf3-9bd4-90486c28a961"), Nome = "Corinthians"},
                            GolsTimeA = 0,
                            GolsTimeB = 0
                        },
                        new Jogo
                        {
                            Id = Guid.NewGuid(),
                            TimeA = new Time{Id = Guid.Parse("c39672c9-7959-4489-b8c3-324c54382dd8"), Nome = "Grêmio"},
                            TimeB = new Time{Id = Guid.Parse("690a77e2-3557-4f5c-8db0-78e8129b5ef5"), Nome = "Cruzeiro"},
                            GolsTimeA = 0,
                            GolsTimeB = 0
                        },
                        new Jogo
                        {
                            Id = Guid.NewGuid(),
                            TimeA = new Time{Id = Guid.Parse("b7bc9c81-4c8c-4569-b807-6b2a53ffc85f"), Nome = "Flamengo"},
                            TimeB = new Time{Id = Guid.Parse("a0d32011-7ae9-4701-96cb-5252557347bb"), Nome = "Vitória"},
                            GolsTimeA = 0,
                            GolsTimeB = 0
                        },
                        new Jogo
                        {
                            Id = Guid.NewGuid(),
                            TimeA = new Time{Id = Guid.Parse("f909c6b8-27bb-4d4d-9af2-705374f1bdb0"), Nome = "Atlético-MG"},
                            TimeB = new Time{Id = Guid.Parse("9c315f25-1136-4c2d-8259-1a8aa02f4c71"), Nome = "Vasco"},
                            GolsTimeA = 0,
                            GolsTimeB = 0
                        },
                        new Jogo
                        {
                            Id = Guid.NewGuid(),
                            TimeA = new Time{Id = Guid.Parse("f909c6b8-27bb-4d4d-9af2-705374f1bdb0"), Nome = "Atlético-MG"},
                            TimeB = new Time{Id = Guid.Parse("9c315f25-1136-4c2d-8259-1a8aa02f4c71"), Nome = "Vasco"},
                            GolsTimeA = 0,
                            GolsTimeB = 0
                        },
                        new Jogo
                        {
                            Id = Guid.NewGuid(),
                            TimeA = new Time{Id = Guid.Parse("a75ccfb1-acb6-4ba3-8a63-964694ed771f"), Nome = "Chapecoense"},
                            TimeB = new Time{Id = Guid.Parse("90467803-dbf8-4ce1-ab3b-1d0f035a8e66"), Nome = "Atlético-PR"},
                            GolsTimeA = 0,
                            GolsTimeB = 0
                        }
                    }
            });
        }
    }
}