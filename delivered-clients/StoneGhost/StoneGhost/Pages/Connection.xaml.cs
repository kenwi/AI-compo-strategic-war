﻿using System;
using System.Collections.Generic;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using StoneGhost.Core.AI;
using StoneGhost.Core.Networking;

namespace StoneGhost.Pages
{
    public sealed partial class Connection : Page
    {
        readonly MainPage _rootPage = MainPage.CurrentPage;
         List<AiClient> _clients = new List<AiClient>();

        public Connection()
        {
            InitializeComponent();

            //button_Click(null, null);
            //button_Click(null, null);
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var host = BindAddress.Text;
            var port = BindPort.Text;

            try
            {
                var aiClient = new AiClient
                {
                    Name = "StoneGhost" + _clients.Count,
                    NetworkClient = new NetworkClient(host, port)
                };
                var result = await aiClient.LoginAsync();
                _rootPage.DisplayMessage(result);
                _clients.Add(aiClient);

                while (_clients.Count >= 2)
                {
                    foreach (var client in _clients)
                    {
                        await client.Tick();

                        /*var writer = new DataWriter(client.NetworkClient.Socket.OutputStream);
                        writer.WriteString("Foobar");

                        await writer.StoreAsync();
                        */                
                    }
                }
            }
            catch (Exception exception)
            {
                _rootPage.DisplayMessage(exception.Message);
            }
        }
    }
}