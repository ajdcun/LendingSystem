using Haley.Models;
using LendingSystemUI.Core;
using LendingSystemUI.Services;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace LendingSystemUI.ViewModels
{
    public abstract class BaseItemViewModel<T> : BaseViewModel
    {
        private T _item;
        public T Item
        {
            get { return _item; }
            set { _item = value; OnPropertyChanged(nameof(Item)); }
        }

        private ObservableCollection<T> _items;
        public ObservableCollection<T> Items
        {
            get { return _items; }
            set { _items = value; OnPropertyChanged(nameof(Item)); }
        }

        private string _formatedItem;
        public string FormatedItem
        {
            get { return _formatedItem; }
            set { _formatedItem = value; OnPropertyChanged(nameof(FormatedItem)); }
        }

        private ObservableCollection<string> _formatedItems;
        public ObservableCollection<string> FormatedItems
        {
            get { return _formatedItems; }
            set { _formatedItems = value; OnPropertyChanged(nameof(FormatedItems)); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; OnPropertyChanged(nameof(FileName)); }
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; OnPropertyChanged(nameof(FilePath)); }
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

        public BaseItemViewModel(INavigationService navigationService, ILoggerService loggerServie, IDataService dataService)
        {
            Navigation = navigationService;
            Logger = loggerServie;
            Data = dataService;

            GetName();
            Logger.LogInfo("Initialize " + Name + " View");

            CreateNewItem();
            Items = new ObservableCollection<T>();
            FormatedItem = String.Empty;
            FormatedItems = new ObservableCollection<string>();

            Logger.LogInfo("Load " + Name);
            GetFileName();
            FilePath = Data.GetFilePath(FileName);
            Logger.LogInfo("\n" +
                FilePath);

            if (Path.Exists(FilePath))
            {
                Items = Data.LoadData<T>(FilePath);
                FormatItems();
                Logger.LogInfo($"\n" +
                    String.Join("\n", FormatedItems));
            }
        }

        public abstract void FormatItems();
        public abstract void FormatItem();
        public abstract void MergeItems();
        public abstract void CreateNewItem();
        public abstract void GetName();
        public abstract void GetFileName();

        public ICommand CMDNavigateToHome => new DelegateCommand<object>(NavigateToHome);
        public ICommand CMDAddItem => new DelegateCommand(AddItem);
        public ICommand CMDDeleteItem => new DelegateCommand<T>(DeleteItem);

        public void NavigateToHome(object parameter)
        {
            Logger.LogInfo("Navigate to Home View");

            Navigation.NavigateTo<HomeViewModel>();
        }
        public void AddItem()
        {
            Logger.LogInfo("Add " + Name);
            MergeItems();

            Logger.LogInfo("Format " + Name);
            FormatItem();
            Logger.LogInfo("\n" +
                FormatedItem);

            Logger.LogInfo("Save " + Name);
            Data.SaveData(FilePath, Items);
            Logger.LogInfo("\n" +
                FilePath);

            CreateNewItem();
            FormatedItem = String.Empty;
        }
        public void DeleteItem(T obj)
        {
            Logger.LogInfo("Delete " + Name);
            if (obj == null) return;
            if (!Items.Contains(obj)) return;
            Items.Remove(obj);
        }
    }
}
