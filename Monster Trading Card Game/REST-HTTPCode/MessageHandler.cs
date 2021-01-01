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

        //acquire packages-----------------------------------

        public void AcquirePackage(List<string> login) 
        {
            bool isOnline = false;
            

            for (int i = 0; i < login.Count; i++)
            {
                if (login[i] == authorization)
                {
                    isOnline = true;
                }
            }
            if (!isOnline)
            {
                string data = "\nuser is not logged in \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }

            int lenght = authorization.IndexOf("-mtcgToken");
            string playername = authorization.Substring(0, lenght);

            int coins = Database.selectPlayerCoins(playername);
            if (coins < 5)
            {
                string data = "\nplayer has not enough coins \n";
                string status = "200 Success";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }

            //----------andere Threads wärend ausführung stoppen--------------------------
            List<int> boosterid = Database.selectUnusedBooster();
            if (boosterid.Count == 0 || boosterid[0]== 0) 
            {
                string data = "\nno booster available \n";
                string status = "200 Success";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            List<string> cards = Database.selectCardInBooster(boosterid[0]);
            if (cards[0]=="0")
            {
                string data = "\nSorry, there was a database error \n";
                string status = "200 Success";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            foreach(string mycard in cards) 
            {
                Database.insertPlayerCard(playername, mycard);
            }
            Database.updateBoosterUsed(boosterid[0]);
            Database.updatePlayerCoins(playername, 5, false);
            string mydata = "\nYou acquired a new booster pack \n";
            string mystatus = "200 Success";
            string mymime = "text/plain";
            ServerResponse(mystatus, mymime, mydata);
            //----------andere Threads wärend ausführung stoppen--------------------------

        }

        //shows the cards of user--------------------------
        public void ShowCards(List<string> login) 
        {
            bool isOnline = false;
            for (int i = 0; i < login.Count; i++)
            {
                if (login[i] == authorization)
                {
                    isOnline = true;
                }
            }
            if (!isOnline)
            {
                string data = "\nuser is not logged in \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            int lenght = authorization.IndexOf("-mtcgToken");
            string playername = authorization.Substring(0, lenght);

            List<string> playercards=Database.selectPlayerCards(playername);
            string message = "";
            foreach(string card in playercards) 
            {
                message += card + "\n\n";
            }
            string mystatus = "200 Success";
            string mymime = "text/plain";
            ServerResponse(mystatus, mymime, message);
            return;

        }

        //show Deck from user-------------------------
        public void ShowDeck(List<string>login, int show) 
        {
            bool isOnline = false;
            for (int i = 0; i < login.Count; i++)
            {
                if (login[i] == authorization)
                {
                    isOnline = true;
                }
            }
            if (!isOnline)
            {
                string data = "\nuser is not logged in \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            int lenght = authorization.IndexOf("-mtcgToken");
            string playername = authorization.Substring(0, lenght);

            List<string> myDeck = Database.selectPlayerDeck(playername, show);
            string message = "";
            foreach (string card in myDeck)
            {
                message += card + "\n\n";
            }
            string mystatus = "200 Success";
            string mymime = "text/plain";
            ServerResponse(mystatus, mymime, message);
            return;
        }

        //set your Deck-------------------------
        public void SetDeck(List<string> login) 
        {
            bool isOnline = false;
            for (int i = 0; i < login.Count; i++)
            {
                if (login[i] == authorization)
                {
                    isOnline = true;
                }
            }
            if (!isOnline)
            {
                string data = "\nuser is not logged in \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            int lenght = authorization.IndexOf("-mtcgToken");
            string playername = authorization.Substring(0, lenght);

            JArray jasonArray = JArray.Parse(body);
            if(jasonArray.Count < 4) 
            {
                string data = "\nyour deck needs 4 cards \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            if (jasonArray.Count > 4)
            {
                string data = "\nyour deck needs 4 cards \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }

            int inDeck = Database.selectPlayerDeckNumber(playername);
            if (inDeck == 4)
            {
                string data = "\ndeck is already full \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }


            foreach (string cardid in jasonArray) 
            {
                Database.updatePlayerCardDeck(playername, cardid, true);
            }
            string mydata = "\nNew Cards in your Deck\n";
            string mystatus = "200 Success";
            string mymime = "text/plain";
            ServerResponse(mystatus, mymime, mydata);
            return;

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
