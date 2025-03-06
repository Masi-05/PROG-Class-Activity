namespace ConsoleWordle.Model
{
    public class Words
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public int Attempts { get; set; } = 5;
    }
}
