using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishQuizTelegramBot.Models;

namespace EnglishQuizTelegramBot.Services.Classes
{
    public interface IWordRepository
    {
        Task<int> AddWord(Word word);

        Task<List<Word>> GetWordsForGame();
    }
}