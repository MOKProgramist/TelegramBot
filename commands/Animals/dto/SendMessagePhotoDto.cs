using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.commands.Animals.dto
{
    internal class SendMessagePhotoDto
    {
        public int chatId { get; set; }
        public string photo { get; set; }

        public string caption { get; set; }
    }
}
