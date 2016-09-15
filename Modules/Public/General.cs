using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

[Module]
public class General
{
    private DiscordSocketClient _client;

    public General(DiscordSocketClient client)
    {
        _client = client;
    }

    [Command("say"), Summary("Echos a message."), Alias("echo")]
    public async Task Say(IUserMessage msg, [Summary("The text to echo")] string echo)
    {
        await msg.Channel.SendMessageAsync(echo = echo.StartsWith("!") ? echo.TrimStart('!') : echo);
    }

    [Command("userinfo"), Summary("Returns info about the current user, or the user parameter, if one passed.")]
    [Alias("user", "whois")]
    public async Task UserInfo(IUserMessage msg, [Summary("The (optional) user to get info for")] IUser user = null)
    {
        var userInfo = user ?? await _client.GetCurrentUserAsync();
        await msg.Channel.SendMessageAsync($"{userInfo.Username}#{userInfo.Discriminator}{Environment.NewLine}ID: {userInfo.Id}{Environment.NewLine}Playing: {userInfo.Game}{Environment.NewLine}{userInfo.AvatarUrl}");
    }

    [Command("info"), Summary("Returns the bot's information.")]
    public async Task Info(IUserMessage msg)
    {
        var application = await _client.GetApplicationInfoAsync();
        await msg.Channel.SendMessageAsync(
            $"{Format.Bold("Info")}\n" +
            $"- Author: {application.Owner.Username} (ID {application.Owner.Id})\n" +
            $"- Library: Discord.Net ({DiscordConfig.Version})\n" +
            $"- Runtime: {RuntimeInformation.FrameworkDescription} {RuntimeInformation.OSArchitecture}\n" +
            $"- Uptime: {GetUptime()}\n\n" +

            $"{Format.Bold("Stats")}\n" +
            $"- Heap Size: {GetHeapSize()} MB\n" +
            $"- Guilds: {(await _client.GetGuildSummariesAsync()).Count}\n" +
            $"- Channels: {(await _client.GetGuildsAsync()).Select(async g => await g.GetChannelsAsync()).Count()}" +
            $"- Users: {(await _client.GetGuildsAsync()).Select(async g => await g.GetUsersAsync()).Count()}"
        );
    }

    [Command("join"), Summary("Returns the Invite URL of the bot.")]
    public async Task Join(IUserMessage msg)
    {
        var application = await _client.GetApplicationInfoAsync();
        await msg.Channel.SendMessageAsync($"A user with `MANAGE SERVER` can invite me to your server here:{Environment.NewLine}<https://discordapp.com/oauth2/authorize?permissions=536345663&scope=bot&client_id=168214818459877376>");
    }

    // TODO: Limit this command to Server Moderators (waiting on permissions)
    [Command("leave"), Summary("Instructs the bot to leave this server"), RequirePermission(GuildPermission.Administrator)]
    public async Task Leave(IUserMessage msg)
    {
        var guild = (msg.Channel as IGuildChannel)?.Guild ?? null;
        if (guild == null) { await msg.Channel.SendMessageAsync("This command can only be ran in a server."); return; }
        await msg.Channel.SendMessageAsync($"Leaving {guild.Name}");
        await guild.LeaveAsync();
    }

    private static string GetUptime()
            => (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
    private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
}

