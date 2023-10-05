namespace LendingSystemUI.Models
{
    public class Lending
    {
        public Student Student { get; set; }
        public Book Book { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}