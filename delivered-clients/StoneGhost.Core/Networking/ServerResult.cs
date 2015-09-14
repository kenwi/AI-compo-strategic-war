using System.Collections.Generic;
using System.Linq;
using Windows.Data.Json;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StoneGhost.Core.Networking
{
    public class ServerResult
    {
        public int NumPlayers;
        public int PlayerId;
        public JsonArray JsonMap;

        public ServerResult(string jsonString)
        {
            var obj = JObject.Parse(jsonString);
            IList<JToken> tokens = obj["map"].Children().ToList();
            
            var walls = tokens.Select(s => s["is_wall"]).Where(s => s != null);
            var spawners = tokens.Select(s => s["spawner"]).Where(s => s != null);
            var units = tokens.Select(s => s["unit"]).Where(s => s != null);

        }
    }
}
