using System;
using System.Threading.Tasks;
using EnglishQuizTelegramBot.Services.Classes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EnglishQuizTelegramBot.Controllers
{
    [Route("api/message")]
    public class TelegramController : ControllerBase
    {
        public TelegramService _service;

        public TelegramController(TelegramService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Post([FromBody] object upd)
        {
            Update update = JsonConvert.DeserializeObject<Update>(upd.ToString());
            if (update.Type == UpdateType.Message)
            {
                await _service.CommandExecute(update);
            }
            else
            {
                if (update.Type == UpdateType.PollAnswer)
                {
                    PollAnswer answer = update.PollAnswer;
                    await _service.CommandExecute(answer);
                }
            }
            return Ok();
        }

        [HttpGet]
        [Route("start")]
        public async Task<IActionResult> Start()
        {
            return Ok();
        }
    }
}