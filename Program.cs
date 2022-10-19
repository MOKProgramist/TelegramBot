// See https://aka.ms/new-console-template for more information

using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

var botClient = new TelegramBotClient("5459925148:AAHf1xcPBrwFHo_Q9MuNt_TuTCGgElDH1qE");

using var cts = new CancellationTokenSource();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
};
botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (update.Message is not { } message)
        return;
    // Only process text messages
    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;
    var userInfo = $"{message?.From?.FirstName} {message?.From?.LastName}" ?? "Не указано";

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId} ({userInfo}).");

    //// Echo received message text
    //Message sentMessage = await botClient.SendTextMessageAsync(
    //    chatId: chatId,
    //    text: "Твое сообщение:\n" + messageText,
    //    replyToMessageId: message.MessageId,
    //    cancellationToken: cancellationToken);
    //// send sticker
    //Message sendSticer = await botClient.SendStickerAsync(chatId: chatId, sticker: "https://www.rbauto.ru/upload/resize_cache/webp/upload/iblock/df0/df07731a4ae4504ff073c635137afb5f.webp");
    //// button
    //await botClient.SendTextMessageAsync(chatId, messageText, replyMarkup: replyKeyboardMarkup());
    if (messageText == "/start")
    {
        await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: $"Добро пожаловать, {userInfo}, В нашего бота по отправки картинок животных :)",
        replyToMessageId: message.MessageId,
        replyMarkup: replyKeyboardMarkup(),
        cancellationToken: cancellationToken);
        return;
    }
    else if (messageText.ToLower() == "котик") 
    {
        await botClient.SendPhotoAsync(
            chatId: chatId,
            photo: "https://krasivosti.pro/uploads/posts/2021-07/1625880574_16-krasivosti-pro-p-milie-parnie-kotiki-koti-krasivo-foto-16.jpg",
            caption: "<b>Котик</b>. <i>Source</i>: <a href=\"https://krasivosti.pro\">Pixabay</a>",
        parseMode: ParseMode.Html,

            cancellationToken: cancellationToken);
        return;
    } else if(messageText.ToLower() == "собака")
    {

    }
    else
    {
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"{userInfo}, прости, такой команды пока нет.",
            cancellationToken: cancellationToken);
        return;
    }
}

IReplyMarkup? replyKeyboardMarkup()
{
    return new ReplyKeyboardMarkup(new[]
        {
        new KeyboardButton[] { "Котик", "Рыбка" },
        new KeyboardButton[] { "Собака", "Цветок" },
    });
}

    Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}