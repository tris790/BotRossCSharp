using Discord;
using Discord.Commands;
using System.Threading.Tasks;
namespace BotRoss
{
    [Module]
    public class Tags
    {
        [Command("addtag"), Summary("Adds a tag."), Alias("addtags")]
        public async Task AddCommand(IUserMessage msg, [Summary("The tag's keyword.")] string tag, [Summary("The tag's content."), Remainder] string output)
        {
            await msg.Channel.SendMessageAsync(Tag.AddTag(tag, output) ? $"Tag: <{tag}> added!" : $"Tag: <{tag}> already exists!");
        }
        [Command("findtag"), Summary("Finds tags."), Alias("findtags")]
        public async Task FindTag(IUserMessage msg, [Summary("The tag to find.")] string tag)
        {
            await msg.Channel.SendMessageAsync(Tag.FindTag(tag));
        }
        [Command("tag"), Summary("Tag.")]
        public async Task ExecuteTag(IUserMessage msg, [Summary("The tag to execute.")] string tag)
        {
            await msg.Channel.SendMessageAsync(Tag.ExecuteTag(tag));
        }
        [Command("removetag"), Summary("Removes a tag.")]
        public async Task RemoveTag(IUserMessage msg, [Summary("The tag to remove.")] string tag)
        {
            await msg.Channel.SendMessageAsync(Tag.RemoveTag(tag));
        }
        [Command("alltag"), Summary("Gets all tag."), Alias("alltags","tags")]
        public async Task GetAllTag(IUserMessage msg)
        {
            await msg.Channel.SendMessageAsync(Tag.FindTag(""));
        }
    }
}
