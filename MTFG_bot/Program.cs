using Discord.WebSocket;
using Discord;
using System.Runtime.InteropServices;
using Discord.Commands;
using MTFG_bot;

public class Program
{
    private static DiscordSocketClient _client;
    private static CommandService _commandService;

    public static async Task Main()
    {
        var config = new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.All
        };

        _client = new DiscordSocketClient(config);
        _commandService = new CommandService();
        
        var _commandHandler = new CommandHandler(_client, _commandService);
        await _commandHandler.InstallCommandsAsync();
        
        _client.Log += Log;
        Console.WriteLine("loging");
        await _client.LoginAsync(TokenType.Bot,
            "token XD");
        Console.WriteLine("starting");
        await _client.StartAsync();
        Console.WriteLine(_client.Status);

        // Block this task until the program is closed.
        await Task.Delay(-1);
    }
    public static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}