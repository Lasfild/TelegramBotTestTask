using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotTestTask.DTOs.Responses
{
    public class WeatherResponseDto
    {
        public string City { get; set; }
        public double Temperature { get; set; }
        public string Description { get; set; }
    }
}
