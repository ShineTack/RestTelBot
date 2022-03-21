using System.Linq;
using System.Threading.Tasks;
using EnglishQuizTelegramBot.Services.Classes.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EnglishQuizTelegramBot.Services.Classes
{
    public class TelegramService
    {
        private readonly TelegramBotClient _client;
        private readonly IMemberRepository _memberRepository;
        private readonly IWordRepository _wordRepository;
        
        public TelegramService(TelegramBot bot, IMemberRepository memberRepository, IWordRepository wordRepository)
        {
            _memberRepository = memberRepository;
            _wordRepository = wordRepository;
            _client = bot.GetBot().Result;
        }

        public async Task CommandExecute(PollAnswer answer)
        {
            PollInfo info = GameCommand.Polls.FirstOrDefault(p => p.PollId == answer.PollId);
            if (info != null)
            {
                if (answer.OptionIds[0] == info.answerId)
                {
                    await _memberRepository.UpdateScore(answer.User.Id);
                }
            }
        }
        public async Task CommandExecute(Update update)
        {
            if (update == null)
            {
                return;
            }

            ICommand command;
            
            switch (update.Message.Type)
            {
                case MessageType.Text : command = await TextCommandExecute(update); break;
                default: command = new NotFoundCommand(); break;
            }

            command.ExecuteAsync(_client, update);
        }

        private async Task<ICommand> TextCommandExecute(Update update)
        {
            ICommand command;
            switch (update.Message.Text)
            {
                case "/start" : return new StartCommand();
                case "/register" : return new RegisterCommand(_memberRepository);
                case "/start_game" : return new GameCommand(_wordRepository);
                case "/my_score": return new ScoreCommand(_memberRepository);
                default: return GetCommandWithParams(update);
            }
        }

        private ICommand GetCommandWithParams(Update update)
        {
            if (update.Message.Text.Contains("/add_word"))
            {
                return new AddWordCommand(_wordRepository);
            }
            else
            {
                return new NotFoundCommand();
            }
        }
    }
}