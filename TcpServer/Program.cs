using System;

using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;



namespace TcpServer
{
    internal class Program
    {
        static async Task Main(string[] args)

        {
            var ip = IPAddress.Any;
            var listener = new TcpListener(ip, 5000);
            listener.Start();
            Console.WriteLine("Server started. Waiting for connection.." + ip);

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                Console.WriteLine("Client connected!");

                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                string received = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Recieved from client:{received}");

                string response = $"Hello, client! You said: {received}";
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);


                client.Close();

            }
            
        }
    }
}
