using LendingSystemUI.Models;
using LendingSystemUI.Services;

namespace LendingSystemUI.ViewModels
{
    public class AuthorViewModel : BaseItemViewModel<Author>
    {
        public AuthorViewModel(INavigationService navigationService, ILoggerService loggerService, IDataService dataService)
            : base(navigationService, loggerService, dataService)
        {
        }

        public override void FormatItems()
        {
            foreach (Author authorItem in Items)
            {
                FormatedItem =
                    $"Autor: {authorItem.FirstName} - {authorItem.LastName}";
                FormatedItems.Add(FormatedItem);
            }
        }

        public override void FormatItem()
        {
            FormatedItem =
                $"Autor: {Item.FirstName} - {Item.LastName}";
            FormatedItems.Add(FormatedItem);
        }

        public override void MergeItems()
        {
            Items.Add(Item);
        }

        public override void CreateNewItem()
        {
            Item = new Author();
        }

        public override void GetName()
        {
            Name = "Author";
        }

        public override void GetFileName()
        {
            FileName = $"{Name}s";
        }
    }
}

