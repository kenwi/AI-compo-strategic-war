using System.Linq;
using Windows.Data.Json;
using Newtonsoft.Json;
using StoneGhost.Core.Networking;

namespace StoneGhost.Core.AI
{
    public class ClientResult
    {
        public string mode;
        public object[] moves;
    }

    public class AiClient
    {
        public AiClient(string name)
        {
            Name = $"name {name}";
        }

        public string Name { get; private set; }
        public Tile[] Map { get; set; }

        // todo: make some sense.
        public string Run()
        {
            string result = null;

            var walls = Map.Where(wall => wall.is_wall);
            var units = Map.Where(unit => unit.unit != null);
            var spawners = Map.Where(spawner => spawner.spawner != null);

            foreach (var unit in units)
            {
                var clientResult = new 
                {
                    mode = "standard",
                    moves = new object[]
                    {
                        new object[]
                        {
                            unit.position[0], unit.position[1], unit.unit.PerformMove()
                        }
                    }
                };

                /*
                var clientResult = new ClientResult
                {
                    mode = "standard",
                    moves = new object[]
                    {
                        unit.position[0], unit.position[1], unit.unit.PerformMove()
                    }
                };*/
                result = JsonConvert.SerializeObject(clientResult);
            }

            return result;
        }
    }
}