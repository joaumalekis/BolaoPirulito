using Acr.UserDialogs;
using BolaoPirulito.Interfaces;
using BolaoPirulito.Models;
using BolaoPirulito.Pages;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BolaoPirulito.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel(INavigation navigation) : base(navigation)
        {
        }

        private ObservableCollection<Rodada> _itens = new ObservableCollection<Rodada>();

        public ObservableCollection<Rodada> Itens
        {
            get => _itens;
            set
            {
                _itens = value;
                OnPropertyChanged();
            }
        }

        private Rodada _selectedItem;

        public Rodada SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;

                if (_selectedItem == null)
                    return;

                SelectItemCommand.Execute(_selectedItem);

                SelectedItem = null;
            }
        }


        private Command _acessarTabelaCommand;
        public Command AcessarTabelaCommand => _acessarTabelaCommand ?? (_acessarTabelaCommand = new Command(async (f) => await AcessarTabelaCommandExecute()));

        private async Task AcessarTabelaCommandExecute()
        {
            await Navigation.PushAsync(new TabelaPage());
        }


        private Command _selectItemCommand;
        public Command SelectItemCommand => _selectItemCommand ?? (_selectItemCommand = new Command(async (f) => await SelectItemCommandExecute((Rodada)f)));

        private async Task SelectItemCommandExecute(Rodada rodada)
        {
            IsBusy = true;
            var firebase = new FirebaseClient(App.BaseUrl, new FirebaseOptions { AuthTokenAsyncFactory = () => Task.Delay(100).ContinueWith(t => App.Token) });
            var apostas = await firebase
                .Child("Apostas")
                .OrderByKey()
                .OnceAsync<Aposta>();

            var aposta = apostas.ToList().FirstOrDefault(p =>
                p.Object?.Rodada?.Id == rodada?.Id && p.Object?.Apostador?.Id == App.ApostadorLogado?.Id)?.Object;
            IsBusy = false;
            await Navigation.PushAsync(new JogosPage(aposta ?? new Aposta { Id = Guid.NewGuid(), Rodada = rodada, Apostador = App.ApostadorLogado}));
        }

        private Command _refreshCommand;
        public Command RefreshCommand => _refreshCommand ?? (_refreshCommand = new Command(async (f) => await RefreshCommandExecute()));

        private async Task RefreshCommandExecute()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;

                Itens = await GetRodadas();
                IsBusy = false;
            }
            catch
            {
                IsBusy = false;
                await UserDialogs.Instance.AlertAsync("Verifique sua conexão com a internet ou tente mais tarde.");
            }
        }

        public async Task<ObservableCollection<Rodada>> GetRodadas()
        {
            try
            {
                var firebase = new FirebaseClient(App.BaseUrl, new FirebaseOptions { AuthTokenAsyncFactory = () => Task.Delay(100).ContinueWith(t => App.Token) });
                var rodadas = await firebase
                    .Child("Jogos")
                    .OrderByKey()
                    .OnceAsync<Rodada>();
                return new ObservableCollection<Rodada>(rodadas.Select(p => p.Object).ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task GetRodadas1()
        {
            try
            {
                var firebase = new FirebaseClient(App.BaseUrl);
                var rodadas = await firebase
                    .Child("Jogos")
                    .OrderByKey()
                    .OnceAsync<object>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}