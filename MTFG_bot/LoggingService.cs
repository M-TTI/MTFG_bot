using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace MTFG_bot;

public class LoggingService
{
    public LoggingService(DiscordSocketClient client, CommandService command)
    {
        client.Log += LogAsync;
        command.Log += LogAsync;
    }

    private Task LogAsync(LogMessage msg)
    {
        if (msg.Exception is CommandException cmdException)
        {
            Console.WriteLine($"[Command/{msg.Severity}] {cmdException.Command.Aliases.First()}"
                + $"Failed to execute {cmdException.Context.Channel}.");
            Console.WriteLine(cmdException);
        }
        else
            Console.WriteLine($"[General/{msg.Severity}] {msg}");
        return Task.CompletedTask;
    }
}