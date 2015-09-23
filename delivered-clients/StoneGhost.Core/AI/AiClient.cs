using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using Newtonsoft.Json;
using StoneGhost.Core.Networking;

namespace StoneGhost.Core.AI
{
    public class AiClient
    {
        public  NetworkClient NetworkClient;

        public string Name { get; set; }
        public Tile[] Map { get; set; }

        public AiClient()
        {

        }

        public AiClient(string name)
        {
            Name = name;
        }

        public async Task Tick()
        {
            await NetworkClient.SendAsync("Foo"); // Will and should return error as of now.
            var result = NetworkClient.ReadAsync(); 
        }

        public async Task<string> LoginAsync()
        {
            await NetworkClient.Connect();

            await NetworkClient.SendAsync($"name {Name}");

            return await NetworkClient.ReadAsync();
        }

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
                result = JsonConvert.SerializeObject(clientResult);
            }

            return result;
        }
        

    }
}