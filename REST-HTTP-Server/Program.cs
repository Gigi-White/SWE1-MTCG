using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
namespace REST_HTTP_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            /* FileHandler Filehandler = new FileHandler();
             TcpListener listener = new TcpListener(IPAddress.Loopback, 8000);
             listener.Start(5);
             Console.WriteLine("Waiting for Connection...");
             Console.CancelKeyPress += (sender, e) => Environment.Exit(0);

             while (true)
             {
                 try
                 {
                     var socket = await listener.AcceptTcpClientAsync();
                     using var writer = new StreamWriter(socket.GetStream()) { AutoFlush = true };
                     writer.WriteLine("Welcome to myserver!");
                     writer.WriteLine("Please enter your commands...");

                     using var reader = new StreamReader(socket.GetStream());
                     string message;

                     do
                     {
                         message = reader.ReadLine();
                         Console.WriteLine("received: " + message);
                         Filehandler.NewMessage(message); //create new txt file with message

                         writer.WriteLine("Message recived");
                     } while (message != "quit");
                     Console.WriteLine("diconnected");

                 }
                 catch (Exception exc)
                 {
                     Console.WriteLine("error occurred: " + exc.Message);
                 }
             }*/
            Console.WriteLine("Starting Server on Port 8080");
            HTTPServer server = new HTTPServer(8080);
            server.Run();



        }


        
    }
}
