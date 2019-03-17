using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BolaoPirulito.Models;
using BolaoPirulito.Pages;
using Firebase.Database;
using Firebase.Database.Query;
using Xamarin.Forms;

namespace BolaoPirulito.ViewModels
{
    public class JogosViewModel : BaseViewModel
    {
        public JogosViewModel(INavigation navigation) : base(navigation)
        {
        }
        public JogosViewModel(INavigation navigation, Aposta aposta) : base(navigation)
        {
            _aposta = aposta;
            _itens = new ObservableCollection<Jogo>(_aposta.Rodada.Jogos);
            //_pontos = _aposta.Rodada.Jogos.Sum(p => p.Pontos);
        }

        private Aposta _aposta;
        public Aposta Aposta
        {
            get => _aposta;
            set
            {
                _aposta = value;
                OnPropertyChanged();
            }
        }

        private int _pontos;
        public int Pontos
        {
            get => _pontos;
            set
            {
                _pontos = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Jogo> _itens = new ObservableCollection<Jogo>();
        public ObservableCollection<Jogo> Itens
        {
            get => _itens;
            set
            {
                _itens = value;
                OnPropertyChanged();
            }
        }

        private Command _salvarCommand;
        public Command SalvarCommand => _salvarCommand ?? (_salvarCommand = new Command(async () => await SalvarCommandExecute()));

        private async Task SalvarCommandExecute()
        {
            IsBusy = true;
            var firebase = new FirebaseClient(App.BaseUrl, new FirebaseOptions { AuthTokenAsyncFactory = () => Task.Delay(100).ContinueWith(t => App.Token) });
            _aposta.Rodada.Jogos = _itens.ToArray();
            await firebase.Child("Apostas").Child(_aposta.Id.ToString).PutAsync(_aposta);
            IsBusy = false;
            await Navigation.PopAsync();
        }

        private Command _refreshCommand;
        public Command RefreshCommand => _refreshCommand ?? (_refreshCommand = new Command(async () => await RefreshCommandExecute()));

        private async Task RefreshCommandExecute()
        {
            IsBusy = true;
            var firebase = new FirebaseClient(App.BaseUrl, new FirebaseOptions { AuthTokenAsyncFactory = () => Task.Delay(100).ContinueWith(t => App.Token) });
            var apostas = await firebase
                .Child("Apostas")
                .OrderByKey()
                .OnceAsync<Aposta>();

            var aposta = apostas.ToList().FirstOrDefault(p =>
                p.Object?.Rodada?.Id == _aposta.Rodada?.Id && p.Object?.Apostador?.Id == App.ApostadorLogado?.Id)?.Object;
            if (aposta?.Rodada?.Jogos != null)
            {
                Itens = new ObservableCollection<Jogo>(aposta.Rodada.Jogos);
                //Pontos = aposta.Rodada.Jogos.Sum(p => p.Pontos);
            }

            IsBusy = false;
        }
    }
}