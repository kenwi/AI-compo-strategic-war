using System.Collections.Generic;
using Windows.Data.Json;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using StoneGhost.Core.Networking;

namespace StoneGhost.Pages
{
    public sealed partial class Connection : Page 
    {
        readonly MainPage _rootPage = MainPage.CurrentPage;
        readonly List<NetworkClient> _clients = new List<NetworkClient>();

        public Connection()
        {
            this.InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var host = BindAddress.Text;
            var port = BindPort.Text;

            var client = new NetworkClient(host, port);
            var result = await client.StartClientAsync();
            
            _rootPage.DisplayMessage(result);
            _clients.Add(client);

            while( true )
            { 
                result = await client.ReadFromServerAsync(client.Socket);

                //JsonObject json = JsonObject.Parse(result);
                var serverResult = new ServerResult(result);
                
                /*
                JsonObject json = JsonObject.Parse(result);

                var player_id = json["player_id"];
                var numPlayers = json["num_players"];
                var mapSize = json["map_size"];                                
                */
                /*
                foreach( var cell in json["map"].GetArray() )
                {
                    var cellObject = cell.GetObject();

                    var position = cellObject["position"];
                    var is_wall = cellObject?["is_wall"];
                    var spawner = cellObject?["unit"];
                }*/

                _rootPage.DisplayMessage(result);
            }
        }
    }
}