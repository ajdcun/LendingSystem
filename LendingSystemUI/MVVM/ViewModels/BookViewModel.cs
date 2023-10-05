using LendingSystemUI.Models;
using LendingSystemUI.Services;
using System.Collections.ObjectModel;

namespace LendingSystemUI.ViewModels
{
    public class BookViewModel : BaseItemViewModel<Book>
    {
        private AuthorViewModel _authorViewModel;

        private string _selectedAuthor;
        public string SelectedAuthor
        {
            get { return _selectedAuthor; }
            set { _selectedAuthor = value; OnPropertyChanged(nameof(SelectedAuthor)); }
        }

        private int _selectedAuthorIndex;
        public int SelectedAuthorIndex
        {
            get { return _selectedAuthorIndex; }
            set { _selectedAuthorIndex = value; OnPropertyChanged(nameof(SelectedAuthorIndex)); }
        }

        private ObservableCollection<string> _formatedAuthors;
        public ObservableCollection<string> FormatedAuthors
        {
            get { return _formatedAuthors; }
            set { _formatedAuthors = value; OnPropertyChanged(nameof(FormatedAuthors)); }
        }

        public BookViewModel(INavigationService navigationService, ILoggerService loggerService, IDataService dataService,
            AuthorViewModel authorViewModel)
            : base(navigationService, loggerService, dataService)
        {
            _authorViewModel = authorViewModel;
            FormatedAuthors = _authorViewModel.FormatedItems;
        }

        public override void FormatItems()
        {
            foreach (Book bookItem in Items)
            {
                FormatedItem =
                    $"Buch: {bookItem.Title} - {bookItem.ISBN} - {bookItem.Publisher}\n" +
                    $" => Autor: {bookItem.Author.FirstName} - {bookItem.Author.LastName}";
                FormatedItems.Add(FormatedItem);
            }
        }

        public override void FormatItem()
        {
            FormatedItem =
                $"Buch: {Item.Title} - {Item.ISBN} - {Item.Publisher}\n" +
                $" => {SelectedAuthor}";
            FormatedItems.Add(FormatedItem);
        }

        public override void MergeItems()
        {
            Item.Author = _authorViewModel.Items[SelectedAuthorIndex];
            Items.Add(Item);
        }

        public override void CreateNewItem()
        {
            Item = new Book();
        }

        public override void GetName()
        {
            Name = "Book";
        }

        public override void GetFileName()
        {
            FileName = $"{Name}s";
        }
    }
}

