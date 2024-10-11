using Discord.Commands;
using System;
using System.Threading.Tasks;


namespace MTFG_bot {
    [Group("avatar")]
    public class AvatarRpgModule : ModuleBase<SocketCommandContext>
    {
        private readonly Random _random = new Random();

        // The roll command is part of the "avatar" group, so the user will type "!avatar roll"
        [Command("roll")]
        [Summary("Rolls two dice.")]
        public async Task RollAsync()
        {
            // Roll two numbers between 1 and 6
            int firstRoll = _random.Next(1, 7);
            int secondRoll = _random.Next(1, 7);

            // Reply with the two dice results
            await ReplyAsync($"{firstRoll} {secondRoll}");
        }
    }
}