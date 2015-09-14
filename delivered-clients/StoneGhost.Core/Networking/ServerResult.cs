using System.Linq;
using Windows.Data.Json;

namespace StoneGhost.Core.Networking
{
    public class ServerResult
    {
        public int NumPlayers;
        public int PlayerId;
        public JsonArray JsonMap;

        public ServerResult(string jsonString)
        {
            // todo: Implement proper deserialization and consider exceptions
            var jsonObject = JsonObject.Parse(jsonString);

            var jsonValue = jsonObject.GetNamedValue("num_players");
            NumPlayers = (int)jsonValue.GetNumber();

            jsonValue = jsonObject.GetNamedValue("player_id");
            PlayerId = (int)jsonValue.GetNumber();

            JsonMap = jsonObject.GetNamedArray("map");
        }
    }
}
