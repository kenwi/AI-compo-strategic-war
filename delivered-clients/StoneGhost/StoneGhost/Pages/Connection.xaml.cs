using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using StoneGhost.Core.AI;
using StoneGhost.Core.Networking;

namespace StoneGhost.Pages
{
    public sealed partial class Connection : Page
    {
        readonly MainPage _rootPage = MainPage.CurrentPage;
        readonly List<NetworkClient> _clients = new List<NetworkClient>();

        public Connection()
        {
            InitializeComponent();

            button_Click(null, null);
            button_Click(null, null);
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var host = BindAddress.Text;
            var port = BindPort.Text;

            try
            {
                var aiClient = new AiClient("StoneGhost");
                var networkClient = new NetworkClient(host, port, aiClient);
                var result = await networkClient.StartClientAsync();

                _rootPage.DisplayMessage(result);
                _clients.Add(networkClient);

                // Todo: Refactor this shit. Make each client do it's thing.
                while (true)
                {
                    result = await networkClient.ReadFromServerAsync(networkClient.Socket);

                    // Get last message from server
                    var serverResult = new ServerResult(result);

                    // Load the client with the map state
                    networkClient.MapState = serverResult.MapState;

                    // Perform the AI-moves
                    string clientResult = networkClient.AiClient.Run();

                    // Send moves to server
                    //await networkClient.SendAsync(clientResult);

                    _rootPage.DisplayMessage(clientResult);
                }
            }
            catch (Exception exception)
            {
                _rootPage.DisplayMessage(exception.Message);
            }
        }
    }
}