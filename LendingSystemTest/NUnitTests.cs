using LendingSystemUI.Models;
using LendingSystemUI.Services;
using LendingSystemUI.ViewModels;
using Moq;
using NUnit.Framework;
using System.Collections.ObjectModel;

namespace LendingSystemUI.Test
{
    [TestFixture]
    public class NUnitTests
    {
        private Mock<INavigationService> _navigationServiceMock;
        private Mock<ILoggerService> _loggerServiceMock;
        private Mock<IDataService> _dataServiceMock;

        private Author author;
        private ObservableCollection<Author> authors;

        private MainViewModel mainViewModel;
        private AuthorViewModel authorViewModel;
        private StudentViewModel studentViewModel;
        private BookViewModel bookViewModel;
        private LendingViewModel lendingViewModel;
        private HomeViewModel homeViewModel;

        [SetUp]
        public void Setup()
        {
            _navigationServiceMock = new Mock<INavigationService>();
            _loggerServiceMock = new Mock<ILoggerService>();
            _dataServiceMock = new Mock<IDataService>();

            author = new Author { FirstName = "Name", LastName = "Nachname" };
            authors = new ObservableCollection<Author>() {
                new Author { FirstName = "Name1", LastName = "Nachname1" },
                new Author { FirstName = "Name2", LastName = "Nachname2" }
            };

            mainViewModel = new MainViewModel(_navigationServiceMock.Object, _loggerServiceMock.Object, _dataServiceMock.Object);
            authorViewModel = new AuthorViewModel(_navigationServiceMock.Object, _loggerServiceMock.Object, _dataServiceMock.Object);
            studentViewModel = new StudentViewModel(_navigationServiceMock.Object, _loggerServiceMock.Object, _dataServiceMock.Object);
            bookViewModel = new BookViewModel(_navigationServiceMock.Object, _loggerServiceMock.Object, _dataServiceMock.Object, authorViewModel);
            lendingViewModel = new LendingViewModel(_navigationServiceMock.Object, _loggerServiceMock.Object, _dataServiceMock.Object, studentViewModel, bookViewModel);
            homeViewModel = new HomeViewModel(_navigationServiceMock.Object, _loggerServiceMock.Object, _dataServiceMock.Object, lendingViewModel);

        }

        [Test]
        public void MainViewModel_Constructor()
        {
            // Arrange
            var viewModel = mainViewModel;

            // Assert
            Assert.AreEqual(_navigationServiceMock.Object, viewModel.Navigation);
            Assert.AreEqual(_loggerServiceMock.Object, viewModel.Logger);
            Assert.AreEqual(_dataServiceMock.Object, viewModel.Data);

            _navigationServiceMock.Verify(n => n.NavigateTo<HomeViewModel>(), Times.Once);
        }

        [Test]
        public void AuthorViewModel_FormatItems()
        {
            // Arrange
            var viewModel = authorViewModel;
            viewModel.Items = authors;

            // Act
            viewModel.FormatItems();

            // Assert
            Assert.AreEqual(2, viewModel.FormatedItems.Count);
            Assert.AreEqual("Autor: Name1 - Nachname1", viewModel.FormatedItems[0]);
            Assert.AreEqual("Autor: Name2 - Nachname2", viewModel.FormatedItems[1]);
        }

        [Test]
        public void AuthorViewModel_MergeItems()
        {
            // Arrange
            var viewModel = authorViewModel;
            viewModel.Item = author;

            // Act
            viewModel.MergeItems();

            // Assert
            Assert.AreEqual(1, viewModel.Items.Count);
            Assert.AreEqual(author, viewModel.Items[0]);
        }

        [Test]
        public void StudentViewModel_CreateNewItem()
        {
            // Arrange
            var viewModel = studentViewModel;

            // Act
            viewModel.CreateNewItem();
            var student = viewModel.Item;

            // Assert
            Assert.IsNotNull(student);
            Assert.AreEqual(typeof(Student), viewModel.Item.GetType());
            Assert.IsNotNull(student.Adress);
            Assert.AreEqual(typeof(Adress), student.Adress.GetType());
        }

        [Test]
        public void BookViewModel_MergeItems()
        {
            // Arrange
            var viewModel = bookViewModel;
            var selectedAuthorIndex = 0;
            authorViewModel.Items = authors;
            viewModel.SelectedAuthorIndex = selectedAuthorIndex;

            var book = new Book();
            viewModel.Item = book;

            // Act
            viewModel.MergeItems();

            // Assert
            Assert.AreEqual(authors[selectedAuthorIndex], book.Author);
        }

    }
}
