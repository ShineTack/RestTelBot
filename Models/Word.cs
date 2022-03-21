namespace EnglishQuizTelegramBot.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string English { get; set; }
        public string Translate { get; set; }
        public long MemberId { get; set; }
    }
}