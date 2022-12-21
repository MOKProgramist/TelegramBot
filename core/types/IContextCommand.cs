using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.core.types
{
    internal interface IContextCommand
    {
        string Command { get; set; } // команда
        string[] Body { get; set; } // команда
    }
}
