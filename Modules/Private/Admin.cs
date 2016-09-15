using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

[Module]
public class Admin
{
    [Command("deletemessage"), Summary("Turns off the bot."), Alias("rm", "removemessage"), RequirePermission(GuildPermission.Administrator)]
    public async Task DeleteMessage(IUserMessage msg,[Summary("Number of message.")] int count=2)
    {
        await msg.Channel.DeleteMessagesAsync(await msg.Channel.GetMessagesAsync(count));
    }

    [Command("shutdown"), Summary("Turns off the bot."), Alias("turnoff", "close", "exit"), RequirePermission(GuildPermission.Administrator)]
    public async Task Shutdown(IUserMessage msg)
    {
        Environment.Exit(0);
    }

    [Command("reboot"), Summary("Reboots the bot."), Alias("restart"), RequirePermission(GuildPermission.Administrator)]
    public async Task Reboot(IUserMessage msg)
    {
        Process.GetCurrentProcess().Start();
        Environment.Exit(0);
    }
    
    private static string GetUptime()
            => (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
    private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
}

