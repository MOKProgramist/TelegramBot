using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TelegramBot;

namespace TelegramBot.core.types
{
    internal interface ICommand
    {
        /**
            * @type {RegExp | string} регулярное выражение
         */
        Regex Pattern { get; set; } // выполнение команды

        /**
         * @type {THandlerCommand} функция обработки
         */
        IHandlerCommand HandlerComman { get; set; }

        /**
         * @type {string} название команды
         */
        string? Name { get; set; }

        /**
         * @type {string} краткое описание команды
         * @default ''
         */
        string? Description {get; set;}

        /**
         * @type {string[]} категории команды
         * @default []
         */
        string[]? Categories { get; set; }
  /**
   * @type {Record<string, unknown>} дополнительные параметры
   * @default {}
   */
  params?: Record<string, unknown>;

  /**
   * @type {Command[]} массив подкоманд
   * @default []
   */
  commands?: Command[];
    }
}
