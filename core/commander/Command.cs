using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TelegramBot.core.commander
{
    internal class Command
    {
        /**
   * @type {RegExp | string} паттерн команды
   */
        public Pattern: RegExp | string;

  /**
   * @type {string} название команды
   */
        public Name?: string;

  /**
   * @type {string} короткое описание команды
   */
  public Description?: string;

  /**
   * @type {Array<string>}
   */
  public Categories: string[];

  /**
   * @type {Record<string, unknown>} дополнительные параметры
   */
  public Params: Record<string, unknown>;

  /**
   * @type {Array<Command>} массив подкоманд
   */
  public Commands: Command[];

  /**
   * @type {THandlerCommand} функция обработки команды
   */
    public Handler: THandlerCommand;

  [key: string]: unknown;

  /**
   * конструктор команды
   * @param {ICommand} data
   * @example
   *
   * new Command({
   *  pattern: /test/i,
   *  name: 'test',
   *  description: 'test command',
   *  categories: ['test'],
   *
   *  params: {
   *      emoji: '📄'
   * },
   *
   *  handler(context) {
   *      context.send('test');
   *  }
   * })
   */
  public Command(data: ICommand)
        {
            if (!data.pattern)
            {
                throw new ConfigureError(
                  'Не указан pattern команды (регулярное выражение)',
          
                );
            }

            if (!(data.pattern instanceof RegExp)) {
                data.pattern = new RegExp(data.pattern);
            }

            if (!data.handler)
            {
                throw new ConfigureError('Не указан обработчик команды');
            }

            if (typeof data.handler !== 'function')
            {
                throw new ConfigureError('Обработчик команды не является функцией');
            }

            const {
                pattern,
      name,
      description,
      categories,
      params,
      commands,
      handler,
    } = data;

            this.pattern = pattern;
            this.name = name;
            this.description = description || '';
            this.categories = categories || [];
            this.params = params || { };
            this.commands = < Command[] | [] > commands || [];
            this.handler = handler;
        }

        get[Symbol.toStringTag] (): string {
    return 'Command';
  }

    /**
     * @description поиск подкоманд
     * @param {Record<string, any>} context
     * @returns {Command}
     */
    findSubCommand<c extends Context>(context: c & IContext): Command {
    let command: Command;

    for (const subCommand of this.commands) {
        if ((< RegExp > subCommand.pattern).test(context.body[1]))
        {
            command = subCommand;
        }
    }

    if (!command) {
        return this;
    }

    context.body = context.body [1].match(command.pattern);

    if (command.commands.length) {
        command = (< Command > command).findSubCommand<c>(context);
    }

    return command;
    }
}
}
