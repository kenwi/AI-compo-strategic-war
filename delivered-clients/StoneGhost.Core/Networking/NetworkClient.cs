﻿using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using StoneGhost.Core.AI;

namespace StoneGhost.Core.Networking
{
    public class NetworkClient
    {
        private HostName _hostName;
        private readonly NetworkAdapter _adapter = null;

        private readonly string _host;
        private readonly string _port;

        public StreamSocket Socket;

        public NetworkClient(string host, string port)
        {
            _host = host;
            _port = port;
        }
        
        public async Task<string> ReadAsync()
        {
            var task = ReadAsync(Socket);
            var result = await task;
            return Encoding.UTF8.GetString(result);
        }

        private static async Task<byte[]> ReadAsync(StreamSocket socket)
        {
            var buffer = new byte[1024 * 1024].AsBuffer();
            await socket.InputStream.ReadAsync(buffer, buffer.Capacity, InputStreamOptions.Partial);
            return buffer.ToArray();
        }

        public async Task<string> ReadFromServerAsync(StreamSocket socket)
        {
            string message = null;

            var task = ReadAsync(Socket);
            var result = await task;
            message = Encoding.UTF8.GetString(result);

            return message;
        }

        public async Task SendAsync(string message)
        {
            try
            { 
                var writer = new DataWriter(Socket.OutputStream);
                writer.WriteString($"{message}\n");
                
                await writer.StoreAsync();
            }
            catch (Exception exception)
            {
                //if (SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown)
                {
                    throw; // TODO: rewrite
                }
            }
        }

        public void Disconnect()
        {
            Socket.Dispose();
        }

        public async Task Connect()
        {
            await Connect(_host, _port);
        }

        public async Task Connect(string bindAddress, string bindPort)
        {
            try
            {
                _hostName = new HostName(bindAddress);
            }
            catch (ArgumentException)
            {
                return;
            }

            Socket = new StreamSocket();
            Socket.Control.KeepAlive = false;

            try
            {
                if (_adapter == null)
                {
                    await Socket.ConnectAsync(_hostName, bindPort);
                }
                else
                {
                    await Socket.ConnectAsync(
                        _hostName,
                        bindPort,
                        SocketProtectionLevel.PlainSocket,
                        _adapter);
                }
            }
            catch (Exception)
            {
                //if (SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown)
                {
                    throw new Exception($"Could not connect to {_host}:{_port}");
                }
            }
        }
    }
}
