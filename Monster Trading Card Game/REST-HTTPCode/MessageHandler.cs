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
        static object anotherLock = new object();
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

        public void CheckType(List<string>login)
        {
            switch (type)
            {
                case "GET":
                    CheckOrderGet(login);
                    break;
                case "POST":
                    CheckOrderPost(login);
                    break;
                case "PUT":
                    CheckOrderPut(login);
                    break;
                case "DELETE":
                    CheckOrderDelete(login);
                    break;
                default:
                    WrongType();
                    break;
                
            }

            return;
        }

        public void CheckOrderGet(List<string>login)
        {
            string name = CheckOrderUserAddition();
            if (name != "")
            {
                GetUserData(login, name);
                return;
            }


            switch (order) 
            {
                case "/cards":
                    ShowCards(login);
                    break;
                case "/deck":
                    ShowDeck(login, 0);
                    break;
                case "/deck?format=plain":
                    ShowDeck(login, 1);
                    break;
                case "/stats":
                    ShowPlayerStats(login);
                    break;
                case "/score":
                    ShowPlayerScores(login);
                    break;
                case "/tradings":
                    ShowTradingDeals(login);
                    break;
                default:
                    WrongOrder();
                    break;


            }
        }

        public void CheckOrderPost(List<string> login)
        {
            string tradeId = CheckOrderTradingAdition();
            if (tradeId != "")
            {
                MakeTradeDeal(login, tradeId);
                return;
            }

            switch (order)
            {
                case "/packages":
                    CreateBooster(login);
                    break;
                case "/transactions/packages":
                    AcquirePackage(login);
                    break;
                case "/tradings":
                    PutOutTradingOffer(login);
                    break;
                default:
                    WrongOrder();
                    break;
            }
        }

        public void CheckOrderPut(List<string> login)
        {
            string name = CheckOrderUserAddition();
            if (name != "")
            {
                ChangeUserData(login, name);
                return;
            }

            switch (order)
            {
                case "/deck":
                    SetDeck(login);
                    break;
                case "/deck/unset":
                    UnsetDeck(login);
                    break;
                default:
                   WrongOrder();
                    break;

            }
        }

        public void CheckOrderDelete(List<string> login) 
        {
            string tradeid = CheckOrderTradingAdition();
            if (tradeid != "")
            {
                DeleteTradeDeal(login, tradeid);
                return;
            }

        }



        //Response when type is not known-----------------------
        public void WrongType()
        {
            string data = "This Message has an unknown type";
            string status = "404 Not Found";
            string mime = "text/plain";
            ServerResponse(status, mime, data);
        }

        //Response when order is not known------------------------
        public void WrongOrder()
        {
            string data = "This Message has an unknown Order";
            string status = "404 Not Found";
            string mime = "text/plain";
            ServerResponse(status, mime, data);
        }

        //check Order user addition
        public string CheckOrderUserAddition()
        {
            string name="";
            if (order.Contains("/users/")&& order.Length>7)
            {
                name = order.Substring(7);
            }

            return name; 
        }
        public string CheckOrderTradingAdition() 
        {
            string tradeId = "";
            if (order.Contains("/tradings/") && order.Length > 7)
            {
                tradeId = order.Substring(10);
            }

            return tradeId;
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


        //login user---------------------------------------------
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

        //creation of a booster-------------------------------------------
        public void CreateBooster(List<string> login) 
        {
            bool isOnline = false;
            if (authorization != "admin-mtcgToken")
            {
                string data = "\nyou are not the admin \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
        
                
            for (int i = 0; i < login.Count; i++)
            {
                if (login[i] == authorization)
                {
                    isOnline = true;
                }
            }

            if (isOnline == false)
            {
                string data = "\nAdmin not logged in \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
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
            string mydata = "\nBooster was created \n";
            string mystatus = "200 Success";
            string mymime = "text/plain";
            ServerResponse(mystatus, mymime, mydata);     
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

            lock (anotherLock)
            {
                List<int> boosterid = Database.selectUnusedBooster();
                if (boosterid.Count == 0 || boosterid[0] == 0)
                {
                    string data = "\nno booster available \n";
                    string status = "200 Success";
                    string mime = "text/plain";
                    ServerResponse(status, mime, data);
                    return;
                }
                List<string> cards = Database.selectCardInBooster(boosterid[0]);
                if (cards[0] == "0")
                {
                    string data = "\nSorry, there was a database error \n";
                    string status = "200 Success";
                    string mime = "text/plain";
                    ServerResponse(status, mime, data);
                    return;
                }
                foreach (string mycard in cards)
                {
                    Database.insertPlayerCard(playername, mycard);
                }
                Database.updateBoosterUsed(boosterid[0]);
                Database.updatePlayerCoins(playername, 5, false);
                string mydata = "\nYou acquired a new booster pack \n";
                string mystatus = "200 Success";
                string mymime = "text/plain";
                ServerResponse(mystatus, mymime, mydata);
            }
            

        }

        //shows the cards of user----------------------------------------------
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

        //show Deck from user------------------------------------------------
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

        //set your Deck-------------------------------------------------
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

        //unset your deck-----------------------------------------------------------
        public void UnsetDeck(List<string> login) 
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
            Database.updatePlayerCardDeckEmpty(playername);
            string mydata = "\n \n";
            string mystatus = "200 Success";
            string mymime = "text/plain";
            ServerResponse(mystatus, mymime, mydata);
            return;

        }

        //get user data-------------------------------------------------
        public void GetUserData(List<string>login,string name)
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

            if (playername != name) 
            {
                string data = "\nyou cant get the info of another player \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;

            }
            string playerdata = Database.selectPlayerData(name);
            if (playerdata=="")
            {
                string data = "\nDatabase Error \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            else 
            {
                
                string status = "200 Successs";
                string mime = "text/plain";
                ServerResponse(status, mime, playerdata);
                return;
            }

        }

        //change player data------------------------------------
        public void ChangeUserData(List<string> login, string name) 
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

            if (playername != name)
            {
                string data = "\nyou cant get the info of another player \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;

            }
            dynamic jasondata = JObject.Parse(body);
            string newname = jasondata.Name;
            string newbio = jasondata.Bio;
            string newimage = jasondata.Image;


            bool done = Database.updatePlayerData(playername, newname, newbio, newimage);
            if (!done)
            {
                string data = "\nDatabase Error \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            else
            {
                string data = "\nPlayer Data was updated \n";
                string status = "200 Successs";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }


        }


        //show points of one player----------------------------------
        public void ShowPlayerStats(List<string> login)
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
            string mymessage = Database.selectPlayerPoints(playername);
            if (mymessage == "0") 
            {
                string data = "\nDatabase Error \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            else
            {
                
                string status = "200 Success";
                string mime = "text/plain";
                ServerResponse(status, mime, mymessage);
            }


        }


        //show scoreboard
        public void ShowPlayerScores(List<string> login) 
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

            string scoreboard = Database.selectPlayerScoreboard();
            if (scoreboard == "0") 
            {
                string data = "\nDatabase Error \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
            }
            else
            {
                string status = "200 Success";
                string mime = "text/plain";
                ServerResponse(status, mime, scoreboard);
            }

        }
        public void ShowTradingDeals(List<string> login) 
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

            string tradingdeals = Database.selectTradingOfferings();

            if(tradingdeals == "0") 
            {
                string data = "\nError in Database \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;

            }
            else 
            {
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, tradingdeals);
                return;
            }

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

        //Put a card on the marketplace ------------------------------------------------
        public void PutOutTradingOffer(List<string> login) 
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

            dynamic jasondata = JObject.Parse(body);
            string cardId = jasondata.CardToTrade;
            string tradeType = jasondata.Type;
            double minimumDamage = jasondata.MinimumDamage;
            string tradeId = jasondata.Id;


            int lenght = authorization.IndexOf("-mtcgToken");
            string username = authorization.Substring(0, lenght);

            //ceck if card belongs to the player
            int cardBelonsToUser = Database.selectCardBelongsToPlayer(username, cardId);
            if (cardBelonsToUser == -1)
            {
                string data = "\nDatabase Error \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            if (cardBelonsToUser == 0)
            {
                string data = "\nYou dont own this card\n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }



            //check if card is in the deck
            int inDeck = Database.selectCardInDeck(cardId);
            if(inDeck == -1) 
            {
                string data = "\nDatabase Error \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            if(inDeck == 1)
            {
                string data = "\nCard can not be put on the marketplace as long as it is in your deck \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            //check if tradedeal is already on market
            int onMarket = Database.selectTradeBelongsToPlayer(username, tradeId);
            if (onMarket == -1 || onMarket > 1)
            {
                string data = "\nDatabase Error while trying to delete trade deal\n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            if (onMarket == 1)
            {
                string data = "\nThis trade offer already exists \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }

            //put card on the database table trading
            bool done = Database.insertTrading(username, tradeId, cardId, minimumDamage, tradeType);

            if (!done) 
            {
                string data = "\nDatabase Error \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            else
            {
                string data = "\nCard was put on the Marketplace \n";
                string status = "200 Success";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }

        }
        //make trade deal---------------------------------------------
        public void MakeTradeDeal(List<string> login, string tradeId)
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
            string username = authorization.Substring(0, lenght);

            dynamic jasondata = JProperty.Parse(body);
            string cardId = jasondata;

            //check if trade deal exists
            int tradeExists = Database.selectTradeExists(tradeId);
            if (tradeExists == -1 || tradeExists > 1)
            {
                string data = "\nDatabase Error while trying to trade\n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            if (tradeExists == 0)
            {
                string data = "\nThis trade offer does not exists \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }

            //check if trade deal belongs to the user
            int tradeDealbelongsToPlayer = Database.selectTradeBelongsToPlayer(username, tradeId);
            if (tradeDealbelongsToPlayer == -1 || tradeDealbelongsToPlayer > 1)
            {
                string data = "\nDatabase Error while trying to trade\n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            if (tradeDealbelongsToPlayer == 1)
            {
                string data = "\n You can not trade with yourselfe \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }

            //check if the trade card belongs to user
            int cardBelongsToPlayer = Database.selectCardBelongsToPlayer(username, cardId);
            if (cardBelongsToPlayer ==-1 || cardBelongsToPlayer > 1)
            {
                string data = "\nDatabase Error while trying to trade\n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            if(cardBelongsToPlayer == 0)
            {
                string data = "\nYou can not trade a card that does not belong to you\n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }

            //check if trade card is in deck
            int cardInDeck = Database.selectCardInDeck(cardId);
            
            if(cardInDeck == -1 || cardInDeck > 1)
            {
                string data = "\nDatabase Error while trying to trade\n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            if(cardInDeck == 1)
            {
                string data = "\nYou can not trade a card that is in your deck\n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }

            //check if the trade card meets the conditions
            double carddamage = Database.selectCardDamage(cardId);
            string cardtype = Database.selectCardType(cardId);

            int meetsCondition = Database.selectMeetsTradeCondition(tradeId, carddamage, cardtype);
            if (meetsCondition == -1 || meetsCondition > 1)
            {
                string data = "\nDatabase Error while trying to trade\n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            if (meetsCondition == 0)
            {
                string data = "\nYour offer does not meet the trading conditions\n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }

            //trade
            string OwnerOfTrade=Database.selectUserInTrade(tradeId);
            string TradeCard = Database.selectCardInTrade(tradeId);

            bool firstrade = Database.updateCardBelongsToPlayer(OwnerOfTrade,cardId);
            bool secondtrade = Database.updateCardBelongsToPlayer(username, TradeCard);
            bool tradeDelete = Database.deleteTrade(tradeId);
            if (!firstrade || !secondtrade)
            {
                string data = "\nDatabase Error while trying to trade\n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            else
            {
                string data = "\nTrade went through\n";
                string status = "200 Success";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }



        }
        //########################### Type Delete ########################################


        //delete trade deal --------------------------------------------
        public void DeleteTradeDeal(List<string> login, string tradeId) 
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

            //check if trading deal belongs to user who wants to delete it and if trade deal even exists
            int lenght = authorization.IndexOf("-mtcgToken");
            string username = authorization.Substring(0, lenght);

            int belongsToPlayer = Database.selectTradeBelongsToPlayer(username, tradeId);
            if (belongsToPlayer == -1 || belongsToPlayer > 1)
            {
                string data = "\nDatabase Error while trying to delete trade deal\n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            if (belongsToPlayer == 0)
            {
                string data = "\nThis trade offer does not belong to this player or does not exist \n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }

            //delete Trade deal
            bool done = Database.deleteTrade(tradeId);
            if (!done) 
            {
                string data = "\nDatabase Error while trying to delete trade deal\n";
                string status = "404 Not found";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
            else
            {
                string data = "\nTrade deal was deleted \n";
                string status = "200 Success";
                string mime = "text/plain";
                ServerResponse(status, mime, data);
                return;
            }
        }




    }
}
