using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using MTFG_bot;

namespace MTFG_bot
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;

        public CommandHandler(DiscordSocketClient _client, CommandService _commands)
        {
            client = _client;
            commands = _commands;
        }

        public async Task InstallCommandsAsync()
        {
            client.MessageReceived += HandleCommandAsync;

            await commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            var msgString = message.Content.ToLower();
            var match = Regex.Match(msgString.ToLower(), @"q(\s*)(u+)(\s*)([o0°]+)(\s*)[il1|]");
            if (match.Success)
            {
                await messageParam.Channel.SendMessageAsync("feur.", messageReference: new MessageReference(messageParam.Id, messageParam.Channel.Id));
            }
            
            int argPos = 0;
            
            if (message.Author.IsBot) return;

            await Program.Log(new Discord.LogMessage(Discord.LogSeverity.Verbose, message.Author.ToString(), message.Content.ToString()));

            var context = new SocketCommandContext(client, message);

            await commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null
                );
        }
    }
}
