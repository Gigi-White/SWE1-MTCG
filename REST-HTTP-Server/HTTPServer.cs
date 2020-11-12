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
    public class HTTPServer : IHTTPServer
    {

        private bool running = false;
        private TcpListener listener;

        private ITcpClient myTcpClient;


        public HTTPServer(int port) 
        {
            listener = new TcpListener(IPAddress.Any,port);
        }
       
        public HTTPServer(ITcpClient client)
        {
            myTcpClient = client;
        }
        //Prgramstart

        //run the connection-----------------------------------------------------
        public void Run() 
        {
            string folderName = "messages";
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            running = true;
            listener.Start();
            while (running) 
            {
                Console.WriteLine("Waiting for connection...");
                System.Net.Sockets.TcpClient client = GetClient();  //Connection with client
                myTcpClient = new TcpClient(client);
                Console.WriteLine("Client connected");
                HandleClient(); //hanlde function

                myTcpClient.End();
                

            }
            running = false;
            listener.Stop();

        }

        //handle the client----------------------------------------------
        public void HandleClient() 
        {
            //StreamReader reader = new StreamReader(client. GetStream());
            String msg = ReadStream();
            /*while (reader.Peek() != -1) 
            {
                msg += (char)reader.Read();   
            }*/
            

            Debug.WriteLine(msg);
            Request req = new Request(msg);  //create Request Object that has all the Request infos
            
            foreach (var item in req.HeadRest)
            {      
                Console.WriteLine(item.ToString());
            }
            FileHandler filehandler = new FileHandler();
            Response resp = new Response(req, filehandler); //make the response message with the "From" function
            resp.ServerResponse(myTcpClient.GetStreamWriter()); //send the response message

        }

        //-------------------gets the TcpClient--------------------------
        public System.Net.Sockets.TcpClient GetClient() 
        {
            System.Net.Sockets.TcpClient client = listener.AcceptTcpClient();

            return client;
        }

        //--------------------------------gets the Stream by crerating a StreamReader and reads from it------------------------------------------
        public string ReadStream()           
        {
            StreamReader reader = myTcpClient.GetStreamReader();//create reader

            string msg = "";
            while (reader.Peek() != -1)  //read from the Stream and save it in "msg"
            {
                msg += (char)reader.Read();
            }
            return msg;
        }

    }

}
