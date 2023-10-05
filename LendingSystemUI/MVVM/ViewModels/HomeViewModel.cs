using Haley.Models;
using LendingSystemUI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LendingSystemUI.ViewModels
{
    public class HomeViewModel : Core.BaseViewModel
    {
        private LendingViewModel _lendingViewModel;

        private ObservableCollection<string> _formatedLendings;
        public ObservableCollection<string> FormatedLendings
        {
            get { return _formatedLendings; }
            set { _formatedLendings = value; OnPropertyChanged(nameof(FormatedLendings)); }
        }

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

        public HomeViewModel(INavigationService navigationService, ILoggerService loggerServie, IDataService dataService,
            LendingViewModel lendingViewModel)
        {
            Navigation = navigationService;
            Logger = loggerServie;
            Data = dataService;

            Logger.LogInfo("Initialize Home View");

            _lendingViewModel = lendingViewModel;
            FormatedLendings = _lendingViewModel.FormatedItems;
        }

        public ICommand CMDNavigateToAuthor => new DelegateCommand<object>(NavigateToAuthor);
        public ICommand CMDNavigateToStudent => new DelegateCommand<object>(NavigateToStudent);
        public ICommand CMDNavigateToBook => new DelegateCommand<object>(NavigateToBook);
        public ICommand CMDNavigateToLending => new DelegateCommand<object>(NavigateToLending);

        private void NavigateToAuthor(object parameter)
        {
            Logger.LogInfo("Navigate to Author View");
            Navigation.NavigateTo<AuthorViewModel>();
        }
        private void NavigateToStudent(object parameter)
        {
            Logger.LogInfo("Navigate to Student View");
            Navigation.NavigateTo<StudentViewModel>();
        }
        private void NavigateToBook(object parameter)
        {
            Logger.LogInfo("Navigate to Book View");
            Navigation.NavigateTo<BookViewModel>();
        }
        private void NavigateToLending(object parameter)
        {
            Logger.LogInfo("Navigate to Lending View");
            Navigation.NavigateTo<LendingViewModel>();
        }
    }
}
