using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

[Module]
public class Admin
{
    [Command("deletemessage"), Summary("Deletes n amount of messages."), Alias("rm", "removemessage", "removemessages", "deletemessages"), RequirePermission(GuildPermission.Administrator)]
    public async Task DeleteMessage(IUserMessage msg, [Summary("Number of message.")] int count = 2)
    {
        await msg.Channel.DeleteMessagesAsync(await msg.Channel.GetMessagesAsync(count));
    }
}

