using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace EnglishQuizTelegramBot.Services.Classes.Commands
{
    public class StartCommand : ICommand
    {
        public async Task<int> ExecuteAsync(TelegramBotClient client, Update update)
        {
            return await Task.Run(async () =>
            {
                List<BotCommand> commands = new List<BotCommand>()
                {
                    new BotCommand()
                    {
                        Command = "/start",
                        Description = "ConfigureBot"
                    },
                    new BotCommand()
                    {
                        Command = "/register",
                        Description = "Write info about you to database"
                    },
                    new BotCommand()
                    {
                        Command = "/add_word",
                        Description = "Add word to database. As example /addWord englishWord russianTranslate"
                    },
                    new BotCommand()
                    {
                        Command = "/start_game",
                        Description = "Start quiz"
                    },
                    new BotCommand()
                    {
                        Command = "/my_score",
                        Description = "Show your score"
                    }
                };
                var keyboard = new ReplyKeyboardMarkup(new []
                {
                    new []
                    {
                        new KeyboardButton("/start_game"),
                        new KeyboardButton("/register"),
                        new KeyboardButton("/my_score")
                    }
                });
                //await client.DeleteMyCommandsAsync();
                await client.SetMyCommandsAsync(commands);
                await client.SendTextMessageAsync(update.Message.Chat.Id,
                    $"Привет {update.Message.From.FirstName}, бот успешно запущен для тебя!", ParseMode.Markdown, replyMarkup: keyboard);
                return 0;
            });
        }
    }
}