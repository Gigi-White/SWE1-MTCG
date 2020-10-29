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

        public void Start()
        {
            string folderName = "messages";
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }
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
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client connected");
                HandleClient(client);

                client.Close();
                

            }
            running = false;
            listener.Stop();

        }
        //handle the client----------------------------------------------
        private void HandleClient(TcpClient client) 
        {
            StreamReader reader = new StreamReader(client.GetStream());
            String msg = "";
            while (reader.Peek() != -1) 
            {
                msg += (char)reader.Read();   
            }

            Debug.WriteLine(msg);
            Request req = Request.GetRequest(msg);  //get the request infos
            
            foreach (var item in req.HeadRest)
            {      
                Console.WriteLine(item.ToString());
            }

            Response resp = Response.From(req); //make the response message
            resp.Post(client.GetStream()); //send the response message

        }

    }

}
