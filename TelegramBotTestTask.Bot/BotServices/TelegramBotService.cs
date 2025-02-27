using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotTestTask.Bot.Handlers;

namespace TelegramBotTestTask.Bot.BotServices
{
    public class TelegramBotService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly MessageHandler _messageHandler;
        private readonly CallbackHandler _callbackHandler;

        public TelegramBotService(ITelegramBotClient botClient, MessageHandler messageHandler, CallbackHandler callbackHandler)
        {
            _botClient = botClient;
            _messageHandler = messageHandler;
            _callbackHandler = callbackHandler;
        }

        public async Task HandleUpdateAsync(Update update)
        {
            if (update.Message != null)
            {
                await _messageHandler.HandleMessageAsync(update.Message);
            }
            else if (update.CallbackQuery != null)
            {
                await _callbackHandler.HandleCallbackAsync(update.CallbackQuery);
            }
        }
    }
}
