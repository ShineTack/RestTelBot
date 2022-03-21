using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EnglishQuizTelegramBot.Services.Classes.Commands
{
    public class NotFoundCommand : ICommand
    {
        public async Task<int> ExecuteAsync(TelegramBotClient client, Update update)
        {
            return await Task.Run(async () =>
            {
                await client.SendTextMessageAsync(update.Message.Chat.Id, "Извините такой команды нет",
                    ParseMode.Markdown);
                return 0;
            });
        }
    }
}