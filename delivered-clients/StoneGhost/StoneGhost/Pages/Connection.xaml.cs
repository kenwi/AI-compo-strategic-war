using System;
using System.Collections.Generic;
using Windows.Data.Json;
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
            this.InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var host = BindAddress.Text;
            var port = BindPort.Text;

            try
            {
                var client = new NetworkClient(host, port, new AiClient("StoneGhost"));
                var result = await client.StartClientAsync();

                _rootPage.DisplayMessage(result);
                _clients.Add(client);

                // Todo: Refactor this shit. Make each client do it's thing.
                while (true)
                {
                    result = await client.ReadFromServerAsync(client.Socket);

                    var serverResult = new ServerResult(result);

                    _rootPage.DisplayMessage(result);
                }
            }
            catch (Exception exception)
            {
                _rootPage.DisplayMessage(exception.Message);
            }
        }
    }
}