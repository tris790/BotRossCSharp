using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

[Module]
public class Help
{
    private CommandService _commands;

    public Help(CommandService command)
    {
        _commands = command;
    }
    [Command("help"), Summary("Helps you with a command.")]
    public async Task GetHelp(IUserMessage msg,
    [Summary("Command")] string command)
    {
        var result = _commands.Commands.FirstOrDefault(x => x.Text.ToLower() == command.ToLower());
        if(result == null)
            result = _commands.Commands.FirstOrDefault(x => x.Aliases.Any(y => y == command.ToLower()));
        var output = "";
        output += $"[{result.Module}] <{result.Name}> {string.Join(", ", result.Parameters)}{Environment.NewLine}{result.Summary}";
        await msg.Channel.SendMessageAsync(result != null ? output : "No commands found!");
    }

    [Command("command"), Summary("Finds all commands with a given keyword or return all commands."), Alias("findcommands", "commands")]
    public async Task GetCommand(IUserMessage msg,
    [Summary("Command")] string command = "")
    {
        var result = command == "" ? string.Join(Environment.NewLine, _commands.Commands.Select(x => x.Text)) : string.Join(Environment.NewLine, _commands.Commands.Where(x => x.Text.ToLower().Contains(command.ToLower())).Select(x => x.Text));
        await msg.Channel.SendMessageAsync(result != null ? $"{string.Join(", ", result)}" : "No commands found!");
    }
}
