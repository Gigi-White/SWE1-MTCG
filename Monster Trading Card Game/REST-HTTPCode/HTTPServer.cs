using Monster_Trading_Card_Game.REST_HTTPCode;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Threading;



namespace REST_HTTP_Server
{
    public class HTTPServer
    {

        private bool running = false;
        private TcpListener listener;
        public List<string> login;
        //private ITcpClient myTcpClient;


        public HTTPServer(int port) 
        {
            listener = new TcpListener(IPAddress.Any,port);
            login = new List<string> ();
        }

        //Prgramstart

        //run the connection-----------------------------------------------------
        public void Run()
        {

            running = true;
            listener.Start(5);
            
            while (running) 
            {

                Console.WriteLine("Waiting for connection...");
                var client = listener.AcceptTcpClient();  //Connection with client

                ThreadPool.QueueUserWorkItem(new WaitCallback (HandleClient), client);

                

                

            }
            running = false;
            listener.Stop();

        }

        //handle the client----------------------------------------------
        private void HandleClient(Object client) 
        {
            Console.WriteLine("Client connected");
            TcpClient myTcpClient = ((TcpClient)client);
            String msg = ReadStream(myTcpClient);
            
            Debug.WriteLine(msg);
            
            Request req = new Request(msg);  //create Request Object that has all the Request infos

            foreach (var item in req.HeadRest)
            {
                Console.WriteLine(item.ToString());
            }

            IMessageHandler handler = new MessageHandler(login, myTcpClient, req.Type, req.Order, req.authorization, req.body);

            
            if (req.Order == "/sessions")
            {
                //only one thread after another is allowed to do that at the time----------------
                login = handler.LoginPlayer(login);
                //------------------------------------------------------------------------------
            }
            else if(req.Order == "/packages") 
            {
                handler.CreateBooster(login);
            } 

            else
            {
                handler.CheckType();
            }
            
            

            //FileHandler filehandler = new FileHandler();
            //Response resp = new Response(req, filehandler); //make the response message with the "From" function
            //resp.ServerResponse(new StreamWriter (myTcpClient.GetStream()) { AutoFlush = true }); //send the response message
            Thread.Sleep(2000);
            myTcpClient.Close();
            

        }

        //-------------------gets the TcpClient--------------------------
        private TcpClient GetClient() 
        {
            TcpClient client = listener.AcceptTcpClient();

            return client;
        }

        //--------------------------------gets the Stream by crerating a StreamReader and reads from it------------------------------------------
        private string ReadStream(TcpClient myTcpClient)           
        {
            
            StreamReader reader = new StreamReader(myTcpClient.GetStream()); //create reader
            
            
            string msg = "";
            while (reader.Peek() != -1)  //read from the Stream and save it in "msg"
            {
                msg += (char)reader.Read();
            }
            //reader.Close();
            return msg;    
        }

    }

}
