using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace BolaoPirulito.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected INavigation Navigation;

        public BaseViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}