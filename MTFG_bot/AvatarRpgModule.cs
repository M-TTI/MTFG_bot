using Discord.Commands;
using System;
using System.Collections;
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
            var task = ReplyAsync($"{RollDiceEmoji()} {RollDiceEmoji()}");
            var disposable = Context.Channel.EnterTypingState();

            await task;
            disposable.Dispose();
        }

        private string RollDiceEmoji() {
            var nb = _random.Next(1, 7);
            
            return nb switch {
                1 => "<:d6_1:1294307625303019641>",
                2 => "<:d6_2:1294315102652334080>",
                3 => "<:d6_3:1294315106649378898>",
                4 => "<:d6_4:1294315108578754560>",
                5 => "<:d6_5:1294315110583631882>",
                6 => "<:d6_6:1294315112500428821>",
                _ => "❔"
            };
        }
    }
}