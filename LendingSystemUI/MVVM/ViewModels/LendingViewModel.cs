using LendingSystemUI.Models;
using LendingSystemUI.Services;
using System.Collections.ObjectModel;

namespace LendingSystemUI.ViewModels
{
    public class LendingViewModel : BaseItemViewModel<Lending>
    {
        private StudentViewModel _studentViewModel;

        private string _selectedStudent;
        public string SelectedStudent
        {
            get { return _selectedStudent; }
            set { _selectedStudent = value; OnPropertyChanged(nameof(SelectedStudent)); }
        }

        private int _selectedStudentIndex;
        public int SelectedStudentIndex
        {
            get { return _selectedStudentIndex; }
            set { _selectedStudentIndex = value; OnPropertyChanged(nameof(SelectedStudentIndex)); }
        }

        private ObservableCollection<string> _formatedStudent;
        public ObservableCollection<string> FormatedStudents
        {
            get { return _formatedStudent; }
            set { _formatedStudent = value; OnPropertyChanged(nameof(FormatedStudents)); }
        }

        private BookViewModel _bookViewModel;

        private string _selectedBook;
        public string SelectedBook
        {
            get { return _selectedBook; }
            set { _selectedBook = value; OnPropertyChanged(nameof(SelectedBook)); }
        }

        private int _selectedBookIndex;
        public int SelectedBookIndex
        {
            get { return _selectedBookIndex; }
            set { _selectedBookIndex = value; OnPropertyChanged(nameof(SelectedBookIndex)); }
        }

        private ObservableCollection<string> _formatedBooks;
        public ObservableCollection<string> FormatedBooks
        {
            get { return _formatedBooks; }
            set { _formatedBooks = value; OnPropertyChanged(nameof(FormatedBooks)); }
        }

        public LendingViewModel(INavigationService navigationService, ILoggerService loggerService, IDataService dataService,
            StudentViewModel studentViewModel, BookViewModel bookViewModel)
            : base(navigationService, loggerService, dataService)
        {
            _studentViewModel = studentViewModel;
            _bookViewModel = bookViewModel;

            FormatedStudents = _studentViewModel.FormatedItems;
            FormatedBooks = _bookViewModel.FormatedItems;
        }

        public override void FormatItems()
        {
            foreach (Lending lendingItem in Items)
            {
                FormatedItem =
                    $"Ausleihe: {lendingItem.StartTime} - {lendingItem.EndTime}\n" +
                    $" => Student: {lendingItem.Student.FirstName} - {lendingItem.Student.LastName} - {lendingItem.Student.Mail} - {lendingItem.Student.Adress.Street} - {lendingItem.Student.Adress.Number} - {lendingItem.Student.Adress.ZipCode} - {lendingItem.Student.Adress.City}\n" +
                    $" => Buch: {lendingItem.Book.Author.FirstName} - {lendingItem.Book.Author.LastName} => {lendingItem.Book.Title} - {lendingItem.Book.ISBN} - {lendingItem.Book.Publisher}";
                FormatedItems.Add(FormatedItem);
            }
        }

        public override void FormatItem()
        {
            FormatedItem =
                $"Ausleihe: {Item.StartTime} - {Item.EndTime}\n" +
                $" => {SelectedStudent}\n" +
                $" => {SelectedBook}";
            FormatedItems.Add(FormatedItem);
        }

        public override void MergeItems()
        {
            Item.Student = _studentViewModel.Items[SelectedStudentIndex];
            Item.Book = _bookViewModel.Items[SelectedBookIndex];
            Items.Add(Item);
        }

        public override void CreateNewItem()
        {
            Item = new Lending();
        }

        public override void GetName()
        {
            Name = "Lending";
        }

        public override void GetFileName()
        {
            FileName = $"{Name}s";
        }
    }
}

