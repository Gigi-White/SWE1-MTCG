using Npgsql;
using REST_HTTP_Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Monster_Trading_Card_Game
{
    class Program
    {
        static void Main(String[] args)
        {
            

           
            int MaxThreadsCount = Environment.ProcessorCount * 4;
            ThreadPool.SetMaxThreads(MaxThreadsCount, MaxThreadsCount);
            ThreadPool.SetMinThreads(2, 2);


            Console.WriteLine("Starting Server on Port 10001");
            HTTPServer server = new HTTPServer(10001);

            server.Run();


        }
    }
}
