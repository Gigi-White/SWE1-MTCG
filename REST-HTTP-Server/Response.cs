using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;

namespace REST_HTTP_Server
{
    class Response
    {

        private String Data;
        private String Status;
        private String Mime;
        private Response(String mime, String status, String data) 
        {
               
            Mime = mime;
            Status = status;
            Data = data;

        }

        //Build my Response values
        public static Response From (Request request)
        {
           if(request.Type == "POST")
            {
               return Posthandler(request);
            }
            
            Response myResponse = new Response("text/plain", "200 Request sucess", "Dies ist mein scharfer Body");

            return myResponse;

        }
        //If request is POST--------------------------------------
        private static Response Posthandler(Request request) 
        {

            if(request.Order != "/messages") 
            {

                return new Response("text/plain", "404 Not Found", "");
            }
            String body = "";
            
            
            for(int i=0; i < request.HeadRest.Length; i++)
            {
                if (request.HeadRest[i] == "\r") 
                {
                    body = request.HeadRest[i + 1];
                }
            }
            string ID = CreateNewFile(body, request.Order);




            return new Response("text/plain", "200 Request sucess", ID);
        }
       

        //make the actual file get the new id back
        private static string  CreateNewFile(string body, string path) 
        {
            string ID = "";
            int data = 0;
            int current = 0;
            string filename = "";
            string theWay = (AppDomain.CurrentDomain.BaseDirectory + path);
            DirectoryInfo directory = new DirectoryInfo(theWay);
            FileInfo[] files = directory.GetFiles("*.txt");
            
            //if there is no file in the folder yet; 
            if (files.Length < 1) 
            {
                filename = (theWay + "/" + "1.txt");
                using (StreamWriter sw = File.CreateText(filename))
                {
                    sw.WriteLine(body);                
                }
           
                ID = "Created new Id: 1";
                return ID;

            }
            //if there are other files check the names of the files (names should all be number + .txt)
            foreach (var item in files)
            {
                String checkout = item.ToString();
                checkout = checkout.Substring(checkout.LastIndexOf('\\')+1);
                checkout = checkout.Remove(checkout.LastIndexOf('.'));
                current = Int16.Parse(checkout);
                
                if (current > data) 
                {
                    data = current;
                }
            }
            data++;
            filename = (theWay + "/" + data.ToString() + ".txt");
            using (StreamWriter sw = File.CreateText(filename))
            {
                sw.WriteLine(body);
            }
            ID = ("Created new Id: " + data.ToString());
            return ID;
        }
       




        //create postmessage
        public void Post(NetworkStream stream) 
        {
            int dataLength = Data.Length; 
            String header = "";
            header = "HTTP/1.1 "+ Status + "\n";
            header += "Content-Type: " + Mime + "\n";
            header += "Content-Lenght: "+ dataLength.ToString() + "\n";
            header += "\n";
            header += Data;
            // Console.WriteLine(header + "\n");
            StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
            writer.WriteLine(header);
        }

    }
}
