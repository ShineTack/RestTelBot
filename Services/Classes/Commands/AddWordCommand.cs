using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EnglishQuizTelegramBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EnglishQuizTelegramBot.Services.Classes.Commands
{
    public class AddWordCommand : ICommand
    {
        private readonly IWordRepository _repository;

        public AddWordCommand(IWordRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<int> ExecuteAsync(TelegramBotClient client, Update update)
        {
            return await Task.Run(async () =>
            {
                string text = update.Message.Text;
                string[] words = text.Split(" ");

                string message = "";
                
                if (words.Length != 3)
                {
                    message = "Неверное количество параметров";
                }
                else if(!new Regex("^[a-zA-Z]{1,40}$").IsMatch(words[1]))
                {
                    message = "Неверно указанно слово на английском";
                }
                else if(!new Regex("^[а-яА-Я]{1,40}$").IsMatch(words[2]))
                {
                    message = "Неверно указан перевод";
                }
                else
                {
                    Word word = new Word()
                    {
                        English = words[1],
                        Translate = words[2],
                        MemberId = update.Message.From.Id
                    };

                    int result = await _repository.AddWord(word);

                    message = result == 0 ? "Слово успешно добавленно в словарь" : "Такое слово уже есть в словаре";
                }
                
                await client.SendTextMessageAsync(update.Message.Chat.Id, message, ParseMode.Markdown);
                
                return 0;
            });
        }
    }
}