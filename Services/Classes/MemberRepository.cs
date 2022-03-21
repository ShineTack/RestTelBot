using System.Threading.Tasks;
using EnglishQuizTelegramBot.Models;
using EnglishQuizTelegramBot.Services.Classes.DAL;

namespace EnglishQuizTelegramBot.Services.Classes
{
    public class MemberRepository : IMemberRepository
    {
        private readonly IDBEnqlishQuiz _dbEnqlishQuiz;

        public MemberRepository(IDBEnqlishQuiz dbEnqlishQuiz)
        {
            _dbEnqlishQuiz = dbEnqlishQuiz;
        }

        public async Task<int> AddMember(Member member)
        {
            return await _dbEnqlishQuiz.proc_AddMember(member);
        }

        public async Task<int> UpdateScore(long memberId)
        {
            return await _dbEnqlishQuiz.proc_UpdateScore(memberId);
        }

        public async Task<ScoreInfo> GetScore(long memberId)
        {
            return await _dbEnqlishQuiz.proc_GetScore(memberId);
        }
    }
}