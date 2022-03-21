using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EnglishQuizTelegramBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EnglishQuizTelegramBot.Services.Classes.Commands
{
    public class GameCommand : ICommand
    {
        private readonly IWordRepository _repository;
        private List<Word> _words;
        public static List<PollInfo> Polls { get; set; } = new List<PollInfo>();
        public GameCommand(IWordRepository repository)
        {
            _repository = repository;

            /*_words = new List<Word>()
            {
                new Word()
                {
                    English = "Green",
                    Translate = "Zeleniy"
                },
                new Word()
                {
                    English = "Red",
                    Translate = "Krasniy"
                },
                new Word()
                {
                    English = "Blue",
                    Translate = "Siniy"
                },
                new Word()
                {
                    English = "White",
                    Translate = "Beliy"
                }
            };*/
        }

        public Task<int> ExecuteAsync(TelegramBotClient client, Update update)
        {
            return Task.Run(async () =>
            {
                _words = await _repository.GetWordsForGame();
                Random random = new Random(Guid.NewGuid().GetHashCode());
                int resultId = random.Next(0, 3);
                Word ask = _words[resultId];

                Message message = client.SendPollAsync(update.Message.Chat.Id, $"Выбери перевод слова : {ask.English}", new string[]
                {
                    _words[0].Translate,
                    _words[1].Translate,
                    _words[2].Translate,
                    _words[3].Translate
                }, type:PollType.Quiz, correctOptionId: resultId, isAnonymous : false, closeDate: DateTime.Now + TimeSpan.FromMinutes(2)).Result;
                
                PollInfo info = new PollInfo()
                {
                    PollId = message.Poll.Id,
                    answerId = message.Poll.CorrectOptionId,
                    ChatId = message.Chat.Id
                };
                
                Polls.Add(info);

                Task.Run(() =>
                {
                    Thread.Sleep(2 * 60 * 1000);
                    Polls.Remove(info);
                });
                
                return 0;
            });
        }
    }

    public class PollInfo
    {
        public long ChatId
        {
            get;
            set;
        }
        public string PollId { get; set; }
        public int? answerId { get; set; }
    }
}