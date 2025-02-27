using Newtonsoft.Json;

namespace TelegramBotTestTask.BusinessLogic.Models
{
    public class Message
    {
        [JsonProperty("message_id")]
        public int MessageId { get; set; }

        [JsonProperty("chat")]
        public Chat Chat { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Chat
    {
        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
