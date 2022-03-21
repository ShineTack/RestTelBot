using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishQuizTelegramBot.Models;
using EnglishQuizTelegramBot.Services.Classes.DAL;

namespace EnglishQuizTelegramBot.Services.Classes
{
    public class WordRepository : IWordRepository
    {
        private IDBEnqlishQuiz _dbEnglish;

        public WordRepository(IDBEnqlishQuiz dbEnglish)
        {
            _dbEnglish = dbEnglish;
        }
        public async Task<int> AddWord(Word word)
        {
            return await _dbEnglish.proc_AddWord(word);
        }

        public async Task<List<Word>> GetWordsForGame()
        {
            return await _dbEnglish.proc_GetWordsForGame();
        }
    }
}