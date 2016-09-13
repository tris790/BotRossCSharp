using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace BotRoss
{
    public class Program
    {
        static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();
        private DiscordSocketClient _client;
        private CommandHandler _commands;

        public async Task Start()
        {
            Console.WriteLine("Bot Ross");
            Auth.LoadAuth();
            _client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Info,
                AudioMode = Discord.Audio.AudioMode.Outgoing,
            });
            _client.Log += _client_Log;
            _commands = new CommandHandler();

            await _client.LoginAsync(TokenType.Bot, Auth.auth.DiscordToken);
            await _client.ConnectAsync();
            await _commands.Install(_client);
            _client.MessageReceived += _commands.HandleCommand;
            await Task.Delay(-1);
        }

        private Task _client_Log(LogMessage arg)
        {
            Console.WriteLine(arg.ToString());
            return Task.CompletedTask;
        }
    }
}
