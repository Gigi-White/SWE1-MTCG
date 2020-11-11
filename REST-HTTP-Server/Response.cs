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
    public class Response : IResponse
    {

        private String Data;
        private String Status;
        private String Mime;
        public Response(Request request) 
        {
               
            String[]mydata = From(request);
            Mime = mydata[0];
            Status = mydata[1];
            Data = mydata[2];

        }

        //Build my Response values
        public string[] From (Request request)
        {
           if(request.Type == "POST")
            {
               return PostHandler(request);
            }
           else if (request.Type == "GET")
            {
                return GetHandler(request);
            }
            else if(request.Type =="DELETE")
            {
                return DeleteHandler(request);
            }
           else if(request.Type == "PUT")
            {
                return PutHandler(request);
            }


            string[] myResponse = { "text/plain", "404 Not Found", "" };

            return myResponse;

        }
        //############################# Functions to handle Post Request ###################################
        //If request is POST--------------------------------------
        public string[] PostHandler(Request request) 
        {
            
            if (request.Order != "/messages") 
            {
                string[] badResponse = { "text/plain", "404 Not Found", "" };
                return badResponse;
            }
            String body = "";

            int bodystart = 0;
            for(int i=0; i < request.HeadRest.Length; i++)
            {
                if (request.HeadRest[i] == "\r") 
                {
                    bodystart = i+1;
                    //body = request.HeadRest[i + 1];
                }
            }
            for (int a = bodystart; a < request.HeadRest.Length; a++) 
            {
                body += request.HeadRest[a];
            }

            string ID = CreateNewFile(body, request.Order);


            string[] goodResponse = { "text/plain", "200 Request sucess", ID };
            return goodResponse;
        }
       

        //----------------------make the actual file and get the new id back---------------------------
        public string  CreateNewFile(string body, string path) 
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


        //####################################################################################################################


        //########################### Functions to handle GET Request ########################################################
        
        //-------------------------------handle GET Request---------------------------------
        public string[] GetHandler(Request request)
        {
            
            if (request.Order == "/messages") //GET requet with all messages
            {
                return GetAll(request.Order);

            }

            int message = ValidOrder(request.Order); //check if GET request with special message (/messages/1) is valid and get the number of the message as int
            
            if (message<0) //if ValidOrder is invalid  the int message will be -1 
            {
                string[] badRequest = { "text/plain", "404 Not Found", "" };
                return badRequest;
            }
            else 
            {
                return GetOne(message);
            }

            
            
            //return new Response("text/plain", "200 Request sucess", "empty body I guess");
        }

        //------------get all messages----------------------
        public string[] GetAll(String path) 
        {
            string theWay = (AppDomain.CurrentDomain.BaseDirectory + path);
            String body = "";
            DirectoryInfo directory = new DirectoryInfo(theWay);
            FileInfo[] files = directory.GetFiles("*.txt");
            if (files.Length == 0)
            {
                body = "No files are available";
                string[] firstResponse = { "text/plain", "200 Request sucess", body };
                return firstResponse;
            }
            
            int number = 1;
            foreach(var item in files)
            {
                body += number.ToString() + ". Message:\n" + File.ReadAllText(item.ToString()) + "\n";
                number++;
            }


            string[] secondResponse = { "text/plain", "200 Request sucess", body };
            return secondResponse;
        }


       
        //----------------------------------------Gets on specific message---------------------------
        public string[] GetOne(int number)
        {
            String path = "/messages";
            
            string theWay = (AppDomain.CurrentDomain.BaseDirectory + path);
            String body = "";
            DirectoryInfo directory = new DirectoryInfo(theWay);
            FileInfo[] files = directory.GetFiles("*.txt");
            if (files.Length < number)
            {
                body = "File Number " + number.ToString()  + " is not available";
                string[] firstResponse = { "text/plain", "200 Request sucess", body };
                return firstResponse;
            }
            else
            {
                body += number.ToString() + ". Message:\n" + File.ReadAllText(files[number-1].ToString()) + "\n";
                string[] secondResponse = { "text/plain", "200 Request sucess", body };
                return secondResponse;
            }
        }
        //#######################################################################################################################


        //##############################Functions to handle the DELETE Request###################################################


        public string[] DeleteHandler(Request request)
        {
            int number = ValidOrder(request.Order);

            if (number<0)
            {
                string[] badResponse = { "text/plain", "404 Not Found", ""};
                return badResponse;
            }
            else 
            {
                return DeleteOne(number);
            }
            
        }
        //------------------------Delete the actual file-------------------------------------------- 
        public string[] DeleteOne(int number) 
        {
            string body= "";
            string path = "/messages";

            string theWay = (AppDomain.CurrentDomain.BaseDirectory + path); //get the the path of the program (there also lies the message folder)
            DirectoryInfo directory = new DirectoryInfo(theWay);
            FileInfo[] files = directory.GetFiles("*.txt");
            if (files.Length < number)
            {
                body = "File Number " + number.ToString() + " is not available";
                string[] firstResponse = { "text/plain", "200 Request sucess", body};
                return firstResponse;
            }
            else
            {
                File.Delete(files[number - 1].ToString());  //file gets deleted
                body = "File Number " + number.ToString() + " is deleted";
                string[] secondResponse = { "text/plain", "200 Request sucess", body};
                return secondResponse;

            }
            
        }



        //###############################################################################################################


        //###################################Functions that handle the PUT Request#######################################

        //Hanldes the PUT Request over all
        public string[] PutHandler(Request request)
        {
            int number = ValidOrder(request.Order); //check if PUT request with special message (/messages/1) is valid and get the number of the message as int

            if (number < 0) //if ValidOrder is invalid  the int message will be -1 
            {
                string[] badRequest = { "text/plain", "404 Not Found", "" };
                return badRequest;
            }

            string body = "";

            int bodystart = 0;
            for (int i = 0; i < request.HeadRest.Length; i++)
            {
                if (request.HeadRest[i] == "\r")
                {
                    bodystart = i + 1;
                    //body = request.HeadRest[i + 1];
                }
            }
            for (int a = bodystart; a < request.HeadRest.Length; a++)
            {
                body += request.HeadRest[a];
            }
            return PutOne(number, body);
            
        }


        public string[] PutOne(int number, string message) 
        {
            string path = "/messages";
            string body = "";
            string theWay = (AppDomain.CurrentDomain.BaseDirectory + path); //get the the path of the program (there also lies the message folder)
            DirectoryInfo directory = new DirectoryInfo(theWay);
            FileInfo[] files = directory.GetFiles("*.txt");
            if (files.Length < number)
            {
                body = "File Number " + number.ToString() + " is not available";
                string[] firstResponse = { "text/plain", "200 Request sucess", body };
                return firstResponse;            }
            else
            {
                File.WriteAllText(files[number-1].ToString(), message);
                body = "File Number " + number.ToString() + " was changed";
                string[] secondResponse = { "text/plain", "200 Request sucess", body };
                return secondResponse;

            }

        }


        //##############################################################################################################

        //-----------check if Ordermessage is valid and give messagenumber as int back------------
        public int ValidOrder(string order)
        {
            String standard = "/messages";
            int number = 0;

            if (order.Length <= standard.Length)
            {
                return -1;
            }

            string[] collection = order.Split('/');

            if (collection[1] != "messages")
            {
                return -1;
            }

            //try to convert string into int
            try
            {
                number = Int32.Parse(collection[2]);

                return number;

            }
            catch (FormatException) //if this fails return -1
            {
                Console.WriteLine($"Unable to parse '{collection[1]}'");
                return -1;
            }

        }

        //--------------------------------create Server Response Headers message-----------------------------------
        public void ServerResponse(StreamWriter writer) 
        {
            int dataLength = Data.Length; 
            String header = "";
            header = "HTTP/1.1 "+ Status + "\n";
            header += "Content-Type: " + Mime + "\n";
            header += "Content-Lenght: "+ dataLength.ToString() + "\n";
            header += "\n";
            header += Data;
            // Console.WriteLine(header + "\n");
            //StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
            writer.WriteLine(header);
        }

    }
}
