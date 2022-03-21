using System.Threading.Tasks;
using EnglishQuizTelegramBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EnglishQuizTelegramBot.Services.Classes.Commands
{
    public class ScoreCommand : ICommand
    {
        private readonly IMemberRepository _repository;

        public ScoreCommand(IMemberRepository repository)
        {
            _repository = repository;
        }
        public Task<int> ExecuteAsync(TelegramBotClient client, Update update)
        {
            return Task.Run(async () =>
            {
                ScoreInfo info = await _repository.GetScore(update.Message.From.Id);

                await client.SendTextMessageAsync(update.Message.Chat.Id,
                    info.GetMessage(update.Message.From.FirstName), ParseMode.Markdown);
                
                return 0;
            });
        }
    }
}