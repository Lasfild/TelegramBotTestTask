namespace TelegramBotTestTask.DataAccess.Models
{
    public class User
    {
        public int Id { get; set; }
        public long TelegramId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
