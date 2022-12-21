using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.core.types
{
    internal interface IContext
    {
        string Text { get; set; } // название
    }
}
