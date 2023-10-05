using LendingSystemUI.Core;
using System;

namespace LendingSystemUI.Services
{
    public interface INavigationService
    {
        BaseViewModel CurrentView { get; }
        void NavigateTo<T>() where T : BaseViewModel;
    }

    public class NavigationService : BaseViewModel, INavigationService
    {
        private Func<Type, BaseViewModel> _viewModelFactory;
        public Func<Type, BaseViewModel> ViewModelFactory
        {
            get => _viewModelFactory;
            private set
            {
                _viewModelFactory = value;
                OnPropertyChanged(nameof(ViewModelFactory));
            }
        }

        private BaseViewModel _currentView;
        public BaseViewModel CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public NavigationService(Func<Type, BaseViewModel> viewModelFactory)
        {
            ViewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : BaseViewModel
        {
            BaseViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
        }
    }
}
