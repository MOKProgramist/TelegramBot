interface ICommand
{
    /**
     * @type {RegExp | string} регулярное выражение
     */
    pattern: RegExp | string;

  /**
   * @type {string} название команды
   */
  name?: string;

  /**
   * @type {string} краткое описание команды
   * @default ''
   */
  description?: string;

  /**
   * @type {Array<string>} категории команды
   * @default []
   */
  categories?: string[];

  /**
   * @type {Record<string, unknown>} дополнительные параметры
   * @default {}
   */
  params?: Record<string, unknown>;

  /**
   * @type {Array<ICommand>} массив подкоманд
   * @default []
   */
  commands?: ICommand[];

  /**
   * @type {THandlerCommand} функция обработки
   */
  handler: THandlerCommand;
}
