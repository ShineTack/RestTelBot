using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishQuizTelegramBot.Models;

namespace EnglishQuizTelegramBot.Services.Classes.DAL
{
    public interface IDBEnqlishQuiz
    {
        Task<int> proc_AddMember(Member member);

        Task<int> proc_AddWord(Word word);

        Task<List<Word>> proc_GetWordsForGame();

        Task<int> proc_UpdateScore(long memberId);

        Task<ScoreInfo> proc_GetScore(long memberId);
    }
}