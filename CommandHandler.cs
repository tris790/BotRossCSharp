using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace BotRoss
{
    public class CommandHandler
    {
        private CommandService commands;
        private DiscordSocketClient client;
        private ISelfUser self;

        public async Task Install(DiscordSocketClient c)
        {
            client = c;
            commands = new CommandService();
            self = await client.GetCurrentUserAsync();

            // Set up the dependency map
            var map = new DependencyMap();
            map.Add(client);
            map.Add(self);

            // Load modules
            await commands.LoadAssembly(Assembly.GetEntryAssembly(), map);
        }

        public async Task HandleCommand(IMessage parameterMessage)
        {
            var msg = parameterMessage as IUserMessage;
            if (msg == null) return;
            int argPos = 0;
            if (msg.HasMentionPrefix(self, ref argPos) || msg.HasCharPrefix('!', ref argPos))
            {
                var result = await commands.Execute(msg, argPos);
                if (!result.IsSuccess)
                    await msg.Channel.SendMessageAsync(result.ErrorReason);
                else
                    Console.WriteLine($"{msg.Channel} {msg.Author}   {msg.Content}");
            }
            else
                Console.WriteLine($"{msg.Channel} {msg.Author}   {msg.Content}");
        }
    }
}