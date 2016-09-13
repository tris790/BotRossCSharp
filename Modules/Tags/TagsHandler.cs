using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotRoss
{
    public class Tag
    {
        public const string Path = "Modules\\Tags\\Tags.json";
        public static bool AddTag(string tag, string output)
        {
            var dict = GetTags();
            if (!(dict.ContainsKey(tag)))
            {
                dict.Add(tag, output.ToString());
                System.IO.File.WriteAllText(Path, JsonConvert.SerializeObject(dict));
                return true;
            }
            return false;
        }
        public static Dictionary<string, string> GetTags()
        {
            var t = System.IO.File.ReadAllText(Path);
            return t == "" ? new Dictionary<string, string>() : JsonConvert.DeserializeObject<Dictionary<string, string>>(t);
        }
        public static string FindTag(string req)
        {
            var commands = GetTags();
            var result = commands.Where(x => x.Key.Contains(req));
            var output = "";
            foreach (var item in result)
            {
                output += $"Tag: <{item.Key}>   `{item.Value}`{Environment.NewLine}";
            }
            return result != null ? output : $"No results found ({req})";
        }
        public static string ExecuteTag(string tag)
        {
            var result = GetTags().FirstOrDefault(x => x.Key == tag).Value;
            return result == "" ? $"Could not find {tag}" : result;
        }
        public static string RemoveTag(string tag)
        {
            var dict = GetTags();
            if ((dict.ContainsKey(tag)))
            {
                dict.Remove(tag);
                System.IO.File.WriteAllText(Path, JsonConvert.SerializeObject(dict));
                return $"<{tag}> was removed!";
            }
            return $"<{tag}> was not found!";
        }
    }
}
