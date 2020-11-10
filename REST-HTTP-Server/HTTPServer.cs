using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml.Serialization;


namespace REST_HTTP_Server
{
    class HTTPServer
    {

        private bool running = false;
        private TcpListener listener;


        public HTTPServer(int port) 
        {
            listener = new TcpListener(IPAddress.Any,port);
        }
        //Prgramstart
        public void Start()
        {
            //Zuerst wird die Directory erstellt
            string folderName = "messages";
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }
            //neuer Thread mit der Run Funktion wird erstellt
            Thread serverThread = new Thread(new ThreadStart(Run));
            serverThread.Start();
        }


        //run the connection-----------------------------------------------------
        public void Run() 
        {
            running = true;
            listener.Start();
            while (running) 
            {
                Console.WriteLine("Waiting for connection...");
                TcpClient client = listener.AcceptTcpClient();  //Connection with client
                Console.WriteLine("Client connected");
                HandleClient(client); //hanlde function

                client.Close();
                

            }
            running = false;
            listener.Stop();

        }
        //handle the client----------------------------------------------
        private void HandleClient(TcpClient client) 
        {
            StreamReader reader = new StreamReader(client. GetStream()); //create reader
            String msg = "";
            while (reader.Peek() != -1) 
            {
                msg += (char)reader.Read();   
            }

            Debug.WriteLine(msg);
            Request req = new Request(msg);  //create Request Object that has all the Request infos
            
            foreach (var item in req.HeadRest)
            {      
                Console.WriteLine(item.ToString());
            }

            Response resp = new Response(req); //make the response message with the "From" function
            resp.ServerResponse(client.GetStream()); //send the response message

        }

    }

}
