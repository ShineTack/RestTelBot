using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EnglishQuizTelegramBot.Services.Classes.Commands
{
    public interface ICommand
    {
        Task<int> ExecuteAsync(TelegramBotClient client, Update update);
    }
}