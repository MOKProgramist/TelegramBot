using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.commands.Animals
{
    internal class Animals
    {
        // Команда с показом кошки
        static public async Task Cat(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            int messageId = message.MessageId | 0;
            var chatId = message.Chat.Id;
            var userInfo = $"{message?.From?.FirstName} {message?.From?.LastName}" ?? "Не указано";

            await botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: "https://krasivosti.pro/uploads/posts/2021-07/1625880574_16-krasivosti-pro-p-milie-parnie-kotiki-koti-krasivo-foto-16.jpg",
                        caption: "<b>Это Котик, ну и ты тоже :D</b>.",
                        parseMode: ParseMode.Html,
                        replyMarkup: keyboardShowMore("котик"),
                        cancellationToken: cancellationToken);
        }

        static public async Task Cat(ITelegramBotClient botClient, CallbackQuery message, CancellationToken cancellationToken)
        {
            int messageId = message.Message.MessageId | 0;
            var chatId = message.Message.Chat.Id;
            var userInfo = $"{message?.From?.FirstName} {message?.From?.LastName}" ?? "Не указано";

            await botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: "https://krasivosti.pro/uploads/posts/2021-07/1625880574_16-krasivosti-pro-p-milie-parnie-kotiki-koti-krasivo-foto-16.jpg",
                        caption: "<b>Держи еще!\nЭто Котик, ну и ты тоже :D</b>.",
                        parseMode: ParseMode.Html,
                        replyMarkup: keyboardShowMore("котик"),
                        cancellationToken: cancellationToken);
        }

        static public async Task Fish(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            int messageId = message.MessageId | 0;
            var chatId = message.Chat.Id;
            var userInfo = $"{message?.From?.FirstName} {message?.From?.LastName}" ?? "Не указано";

            await botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: "https://fanfishka.ru/uploads/posts/2017-06/1497704304_23-copy.jpg",
                        caption: "<b>Это рыбка, она тоже плавает в мыслях:D</b>.",
                        parseMode: ParseMode.Html,
                        replyMarkup: keyboardShowMore("рыбка"),
                        cancellationToken: cancellationToken);
        }

        static public async Task Fish(ITelegramBotClient botClient, CallbackQuery message, CancellationToken cancellationToken)
        {
            int messageId = message.Message.MessageId | 0;
            var chatId = message.Message.Chat.Id;
            var userInfo = $"{message?.From?.FirstName} {message?.From?.LastName}" ?? "Не указано";

            await botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: "https://fanfishka.ru/uploads/posts/2017-06/1497704304_23-copy.jpg",
                        caption: "<b>Держи еще!\nЭто рыбка, она тоже плавает в мыслях:D</b>.",
                        parseMode: ParseMode.Html,
                        replyMarkup: keyboardShowMore("рыбка"),
                        cancellationToken: cancellationToken);
        }

        // Показать еще животное - кнопка
        static IReplyMarkup? keyboardShowMore(string animal)
        {
            return new InlineKeyboardMarkup(new[]
    {
        // first row
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Еще!", animal),

        },
    });
        }
    }
}
