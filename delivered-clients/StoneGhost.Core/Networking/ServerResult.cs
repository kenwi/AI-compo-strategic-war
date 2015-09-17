using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Windows.Data.Json;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StoneGhost.Core.Networking
{
    public class ServerResult
    {
        private readonly string _json;

        public MapState MapState => JsonConvert.DeserializeObject<MapState>(_json);

        public ServerResult(string jsonString)
        {
            _json = jsonString;
            //var obj = JObject.Parse(jsonString);
            //IList<JToken> tokens = obj["map"].Children().ToList();

            //MapState = JsonConvert.DeserializeObject<MapState>(jsonString);

            /*
            var walls = tokens.Select(s => s).Where(s => s["is_wall"] != null );
            var spawners = tokens.Select(s => s).Where(s => s["spawner"] != null);
            var units = tokens.Select(s => s).Where(s => s["unit"] != null);
            */
            //var walls = tokens.Select(s => s["is_wall"]).Where(s => s != null);
            //var spawners = tokens.Select(s => s["spawner"]).Where(s => s != null);
            //var units = tokens.Select(s => s["unit"]).Where(s => s != null);
        }
    }
}
