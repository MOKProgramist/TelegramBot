using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.commands.Starts
{
    internal class Starts
    {
        static public async Task Start(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            int messageId = message.MessageId | 0;
            var chatId = message.Chat.Id;
            var userInfo = $"{message?.From?.FirstName} {message?.From?.LastName}" ?? "Не указано";

            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Добро пожаловать, {userInfo}, В нашего бота по отправки картинок животных :)",
                replyToMessageId: messageId,
                replyMarkup: keyboardStartUser(),
                cancellationToken: cancellationToken
            );
        }

        public static async Task NotCommand(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            int messageId = message.MessageId | 0;
            var chatId = message.Chat.Id;
            var userInfo = $"{message?.From?.FirstName} {message?.From?.LastName}" ?? "Не указано";

            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"{userInfo}, команда не найдена.\n/help - чтобы узнать мои команды.",
                replyToMessageId: messageId,
                replyMarkup: keyboardStartUser(),
                cancellationToken: cancellationToken
            );
        }

        static IReplyMarkup? keyboardStartUser()
        {
            return new ReplyKeyboardMarkup(new[]
             {
        new KeyboardButton[] { "Котик", "Рыбка" },
        new KeyboardButton[] { "Собака", "Цветок" },
             });
        }
    }
}
