using System.Threading.Tasks;
using EnglishQuizTelegramBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EnglishQuizTelegramBot.Services.Classes.Commands
{
    public class RegisterCommand : ICommand
    {
        private readonly IMemberRepository _repository;

        public RegisterCommand(IMemberRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> ExecuteAsync(TelegramBotClient client, Update update)
        {
            return await Task.Run(async () =>
            {
                Member member = new Member()
                {
                    Id = update.Message.From.Id,
                    ChatId = update.Message.Chat.Id,
                    Username = update.Message.From.Username?? "Unknown"
                };
                
                int result = await _repository.AddMember(member);

                string message = result == 0 ? "Поздравляю вы успешно зарегистрированны" : "Упс, возможно вы уже зарегистрированны";
                
                await client.SendTextMessageAsync(member.ChatId, message, ParseMode.Markdown);
                return 0;
            });
        }
    }
}