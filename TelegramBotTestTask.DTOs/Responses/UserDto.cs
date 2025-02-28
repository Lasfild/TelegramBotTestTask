using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotTestTask.DTOs.Responses
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public List<string> WeatherQueryHistory { get; set; }
        public long TelegramId { get; set; }
    }
}
