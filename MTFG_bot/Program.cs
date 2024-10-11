using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DotNetEnv;

namespace MTFG_bot;

public class Program
{
    private static DiscordSocketClient? _client;
    private static CommandService? _commandService;

    public static async Task Main()
    {
        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.All
        };

        _client = new DiscordSocketClient(config);
        _commandService = new CommandService();
        
        var _commandHandler = new CommandHandler(_client, _commandService);
        await _commandHandler.InstallCommandsAsync();
        
        DotNetEnv.Env.Load();
        var token = Environment.GetEnvironmentVariable("TOKEN");
        if (String.IsNullOrEmpty(token)){
            throw new ArgumentException("token was not found; setup a .env file at the root of the project");
        }
        _client.Log += Log;
        Console.WriteLine("logging");
        await _client.LoginAsync(TokenType.Bot, token);
        Console.WriteLine("starting");
        await _client.StartAsync();
        Console.WriteLine(_client.Status);
        _client.Ready += () =>
        {
            Console.WriteLine("Ready"); 
            return Task.CompletedTask;
        };

        // Block this task until the program is closed.
        await Task.Delay(-1);
    }
    public static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}