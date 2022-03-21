using System.Threading.Tasks;
using EnglishQuizTelegramBot.Models;

namespace EnglishQuizTelegramBot.Services.Classes
{
    public interface IMemberRepository
    {
        Task<int> AddMember(Member member);

        Task<int> UpdateScore(long memberId);

        Task<ScoreInfo> GetScore(long memberId);
    }
}