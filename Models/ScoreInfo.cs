namespace EnglishQuizTelegramBot.Models
{
    public class ScoreInfo
    {
        public int CountWords { get; set; }
        public int Score { get; set; }

        public string GetMessage(string user)
        {
            return $"{user}\n вы дали {Score} правильных ответов \nи добавили {CountWords} слов";
        }
    }
}