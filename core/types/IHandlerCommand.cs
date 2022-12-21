using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.core.types
{
    internal interface IHandlerCommand
    {
        object Context { get; set; }
        string Bot { get; set; }
    }
}
