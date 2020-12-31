using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Monster_Trading_Card_Game.REST_HTTPCode
{
    class MessageHandler : IMessageHandler
    {

        public TcpClient client;
        public string type;
        public string order;
        public string authorization;
        public string body;
        public List<string> login;
        IDatabasehandler Database;


        public MessageHandler(List<string> mylogin, TcpClient myClient, string myType, string myOrder, string myAuthorization, string myBody) 
        {
            client = myClient;
            type = myType;
            order = myOrder;
            authorization = myAuthorization;
            body = myBody;
            Database = new Databasehandler();
            login = mylogin;
        }


        //extra thing for login---------------------------------------------
        public List<string> LoginPlayer(List<string>login) 
        {
            dynamic jasondata = JObject.Parse(body);
            string username = jasondata.Username;
            string password = jasondata.Password;
            bool alreadyOnline = false;
            for (int i=0; i< login.Count; i++) 
            {
                if (login[i] == username + "-mtcgToken")
                {
                    alreadyOnline = true;
                }
            }
            if (alreadyOnline == true)
            {
                string data = "\nPlayer is already logged in \n";
                string status = "404 Not Found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return login;
            }

            if(Database.selectPlayerPassword(username, password) == 1) 
            {
                login.Add(username + "-mtcgToken");

                string data = "\nPlayer is logged in \n";
                string status = "200 Success";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return login;

            }
            else
            {
                string data = "\nWrong username or password \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return login;
            }
        }

        //extra thing for creation of a booster-------------------------------------------
        public void CreateBooster(List<string> login) 
        {
            bool isOnline = false;
            if (authorization == "admin-mtcgToken")
            {
                
                for (int i = 0; i < login.Count; i++)
                {
                    if (login[i] == authorization)
                    {
                        isOnline = true;
                    }
                }

                if (isOnline == true)
                {
                    
                    JArray jasonArray = JArray.Parse(body);


                    var myobject = jasonArray[0];
                    int boosterid = (int)myobject["BoosterId"];
                    Database.insertBooster(boosterid);


                    for (int i = 1; i<jasonArray.Count; i++) 
                    {
                        string cardid = (string)jasonArray[i]["Id"];
                        string cardname = (string)jasonArray[i]["Name"];
                        double damage = (double)jasonArray[i]["Damage"];
                        string cardType = (string)jasonArray[i]["Cardtype"];
                        string element = (string)jasonArray[i]["Element"];

                        Database.insertCard(cardid, cardname, damage, cardType, element);
                        Database.insertBoosterCard(cardid, boosterid);
                    }
                    string data = "\nBooster was created \n";
                    string status = "200 Success";
                    string mime = "text/plain";
                    ServerResponse(status, mime, data);


                }
                else
                {
                    string data = "\nAdmin not logged in \n";
                    string status = "404 Not found";
                    string mime = "text/plain";
                    ServerResponse(status, mime, data);
                }
            }
            else 
            {
                string data = "\nyou are not the admin \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
            }
        }




        public bool CheckType() 
        {
            switch (type)
            {
                case "GET":
                    CheckOrderGet();
                    break;
                case "POST":
                    CheckOrderPost();
                    break;
                case "PUT":
                    CheckoutOrderPut();
                    break;
                default:
                    WrongType();
                    break;
            }

            return true;
        }

        public void CheckOrderGet()
        {

        }

        public void CheckOrderPost() 
        {
            switch (order)
            {
                case "/users":
                    HandlePostUsers();
                    break;
            }
        }

        public void CheckoutOrderPut()
        {

        }




        //Response when type is not known-----------------------
        public void WrongType() 
        {
            string data = "This Message has a unknown type";
            string status = "404 Not Found";
            string mime = "text/plain";
            ServerResponse(status, mime, data);
        }



        //##################################### handle post requests ##################################################
        
        //create user----------------------------------
        public void HandlePostUsers()
        {
            dynamic jasondata = JObject.Parse(body);

                string username = jasondata.Username;
                string password = jasondata.Password;
                bool isadmin = false;
                if(username == "admin")
                {
                    isadmin = true;
                }
                if (Database.selectPlayerCreated(username)== 0) 
                {
                    if (Database.insertPlayer(username, password, isadmin))
                    {
                        string data = "\nPlayer was created\n";
                        string status = "200 Success";
                        string mime = "text/plain";
                        ServerResponse(status, mime, data);
                    }
                    else
                    {
                        string data = "\nDatabase Error while trying to creat player\n";
                        string status = "200 Success";
                        string mime = "text/plain";
                        ServerResponse(status, mime, data);
                    }
                }
                else
                {
                    string data = "\nPlayer already exists\n";
                    string status = "200 Success";
                    string mime = "text/plain";
                    ServerResponse(status, mime, data);
                }
            

        }
        //login user
        public void HandlePostSession() 
        {
        
        }



















        //sends response to client--------------------------------
        public void ServerResponse(string status, string mime, string data)
        {
            StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true };

            int dataLength = data.Length;
            String header = "";
            header = "HTTP/1.1 " + status + "\n";
            header += "Content-Type: " + mime + "\n";
            header += "Content-Lenght: " + dataLength.ToString() + "\n";
            header += "\n";
            header += data;
            // Console.WriteLine(header + "\n");
            //StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
            writer.WriteLine(header);
        }


    }
}
