using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BotRoss
{
    public class Auth
    {
        public static Auth auth { get; set; } = new Auth();
        public string DiscordToken { get; set; }
        public string GoogleToken { get; set; }
        public string GoogleSearchToken { get; set; }
        public string LeagueToken { get; set; }

        public const string Path = "Authentication\\Auth.json";
        public Auth()
        {
            auth = this;
        }
        public static void LoadAuth()
        {
            var t = System.IO.File.ReadAllText(Path);
            auth = t == "" ? new Auth() : JsonConvert.DeserializeObject<Auth>(t);
        }
        public static void CreateAuth(string discordToken = "", string googleToken = "", string googleSearchToken = "", string leagueToken = "")
        {
            auth.DiscordToken = discordToken;
            auth.GoogleToken = googleToken;
            auth.GoogleSearchToken = googleSearchToken;
            auth.LeagueToken = leagueToken;
            System.IO.File.WriteAllText(Path, JsonConvert.SerializeObject(auth));
        }
    }
}
