using System;

using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace TcpClientApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            Console.WriteLine("Connecting to server...");

            TcpClient client = new TcpClient();
            await client.ConnectAsync("127.0.0.1", 5000);

            NetworkStream stream = client.GetStream();

            // Send message
            string message = "Hello from client!";
            byte[] data = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(data, 0, data.Length);

            // Read response
            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Console.WriteLine("Server replied: " + response);

            Console.WriteLine("Press ENTER to close connection.");
            Console.ReadLine();

            client.Close();
        }
    }
}



