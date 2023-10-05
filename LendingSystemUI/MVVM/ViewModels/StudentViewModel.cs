using LendingSystemUI.Models;
using LendingSystemUI.Services;

namespace LendingSystemUI.ViewModels
{
    public class StudentViewModel : BaseItemViewModel<Student>
    {
        public StudentViewModel(INavigationService navigationService, ILoggerService loggerService, IDataService dataService)
            : base(navigationService, loggerService, dataService)
        {
        }

        public override void FormatItems()
        {
            foreach (Student studentItem in Items)
            {
                FormatedItem =
                    $"Student: {studentItem.FirstName} - {studentItem.LastName} - {studentItem.Mail} - {studentItem.Adress.Street} - {studentItem.Adress.Number} - {studentItem.Adress.ZipCode} - {studentItem.Adress.City}";
                FormatedItems.Add(FormatedItem);
            }
        }

        public override void FormatItem()
        {
            FormatedItem =
                $"Student: {Item.FirstName} - {Item.LastName} - {Item.Mail} - {Item.Adress.Street} - {Item.Adress.Number} - {Item.Adress.ZipCode} - {Item.Adress.City}";
            FormatedItems.Add(FormatedItem);
        }

        public override void MergeItems()
        {
            Items.Add(Item);
        }

        public override void CreateNewItem()
        {
            Student student = new Student();
            Adress adress = new Adress();
            student.Adress = adress;
            Item = student;
        }

        public override void GetName()
        {
            Name = "Student";
        }

        public override void GetFileName()
        {
            FileName = $"{Name}s";
        }
    }
}

