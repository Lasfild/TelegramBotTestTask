using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotTestTask.DTOs.Responses
{
    public class UserDto
    {
        public int Id { get; set; }
        public long TelegramId { get; set; }
        public string LastCity { get; set; }
    }
}
