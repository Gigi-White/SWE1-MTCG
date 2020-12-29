using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;


namespace Monster_Trading_Card_Game
{
    class Program
    {
        static void Main(String[] args)
        {
            Console.WriteLine("Hello World!");

            IDatabasehandler handler = new Databasehandler();

         

            List<string> ausgabe = handler.selectPlayerCards("Gigi");
            for (int i=0; i< ausgabe.Count; i++) 
            {
                Console.WriteLine(ausgabe[i]);
            }
        }
    }
}
