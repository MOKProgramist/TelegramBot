// See https://aka.ms/new-console-template for more information

using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

using TelegramBot.commands;

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
    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (update.Message is not { } message)
        return;
    // Only process text messages
    if (message.Text is not { } messageText)
        return;

    int messageId = message.MessageId | 0;

    var chatId = message.Chat.Id;
    var userInfo = $"{message?.From?.FirstName} {message?.From?.LastName}" ?? "Не указано";

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId} ({userInfo}).");

    // Начальная команда
    if (messageText == "/start")
    {
        await Starts.Start(botClient, message, cancellationToken);
    }
    else if (messageText.ToLower() == "котик")
    {
        await Animals.Cat(botClient, message, cancellationToken);
        return;
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