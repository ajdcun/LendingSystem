using LendingSystemUI.Services;

namespace LendingSystemUI.ViewModels
{
    public class MainViewModel : Core.BaseViewModel
    {
        private INavigationService _navigation;
        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged(nameof(Navigation));
            }
        }

        private ILoggerService _logger;
        public ILoggerService Logger
        {
            get => _logger;
            set
            {
                _logger = value;
                OnPropertyChanged(nameof(Logger));
            }
        }

        private IDataService _data;
        public IDataService Data
        {
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged(nameof(Data));
            }
        }

        public MainViewModel(INavigationService navigationService, ILoggerService loggerServie, IDataService dataService)
        {
            Navigation = navigationService;
            Logger = loggerServie;
            Data = dataService;

            Logger.LogInfo("\n" +
                "*********************************************\n" +
                "---------- Start Lending System UI ----------\n" +
                "*********************************************");

            Navigation.NavigateTo<HomeViewModel>();
        }

    }
}
