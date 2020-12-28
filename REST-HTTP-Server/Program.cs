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
        

            Console.WriteLine("Starting Server on Port 10001");
            HTTPServer server = new HTTPServer(10001);

            server.Run();



        }


        
    }
}
