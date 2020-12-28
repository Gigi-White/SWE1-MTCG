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
        
            Console.WriteLine("Starting Server on Port 8080");
            HTTPServer server = new HTTPServer(8080);

            server.Run();



        }


        
    }
}
