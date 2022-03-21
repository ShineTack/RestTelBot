using System.Threading.Tasks;
using EnglishQuizTelegramBot.Tools;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace EnglishQuizTelegramBot.Services.Classes
{
    public class TelegramBot
    {
        private readonly TelegramBotConfig _config;
        private TelegramBotClient _client;

        public TelegramBot(IOptions<TelegramBotConfig> options)
        {
            _config = options.Value;
        }

        public async Task<TelegramBotClient> GetBot()
        {
            if (_client != null)
            {
                return _client;
            }

            _client = new TelegramBotClient(_config.Token);

            var url = _config.NgrokUrl;
            await _client.SetWebhookAsync(url);

            return _client;
        }
    }
}