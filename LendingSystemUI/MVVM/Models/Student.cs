namespace LendingSystemUI.Models
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Adress Adress { get; set; }
        public string Mail { get; set; }
    }

    public class Adress
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}