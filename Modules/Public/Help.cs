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
    [Command("helpcommand"), Summary("Helps you with a command."), Alias("help")]
    public async Task GetHelp(IUserMessage msg, [Summary("Command")] string command)
    {
        var result = _commands.Commands.FirstOrDefault(x => x.Text.ToLower() == command.ToLower());
        if (result == null)
            result = _commands.Commands.FirstOrDefault(x => x.Aliases.Any(y => y == command.ToLower()));

        await msg.Channel.SendMessageAsync(result != null ? $"[**{result.Module}**] <*{result.Name}*> {string.Join(", ", result.Parameters)}{Environment.NewLine}{result.Summary}{Environment.NewLine}Aliases: {string.Join(", ", result.Aliases)}" : "No commands found!");
    }

    [Command("findcommand"), Summary("Finds all commands with a given keyword or return all commands."), Alias("findcommands", "command", "commands")]
    public async Task GetCommand(IUserMessage msg, [Summary("Command")] string command = "")
    {
        string result = "";
        if (command == "")
            foreach (var module in _commands.Modules)
                result += $"[**{module.Name}**]{Environment.NewLine}    {string.Join(", ", module.Commands.OrderBy(x => x.Text).Select(x => x.Text))}{Environment.NewLine}";
        else
            result = string.Join(", ", _commands.Commands.Where(x => x.Text.ToLower().Contains(command.ToLower())).Select(x => x.Text));
        await msg.Channel.SendMessageAsync(result != "" ? result : "No commands found!");

        //var result = command == "" ? string.Join(", ", _commands.Commands.Select(x => x.Text)) : string.Join(", ", _commands.Commands.Where(x => x.Text.ToLower().Contains(command.ToLower())).Select(x => x.Text));
        //await msg.Channel.SendMessageAsync(result != null ? $"{string.Join(", ", result)}" : "No commands found!");
    }
}
