﻿using System;
using Npgsql;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    class Databasehandler : IDatabasehandler
    {

        //############################## database inserts #######################################//

        //insert player-----------------------------------------------


        public bool insertPlayer(string name, string password, bool isadmin)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";
                string username = name;
                using var con = new NpgsqlConnection(cs);
                con.Open();

                //var sql = "INSERT INTO player(username, playername, playerpassword, coins, points, bio, image, isadmin) VALUES(@name,'Player', @password,20,100,'','',@isadmn)";
                var sql = "INSERT INTO player (playername, playerpassword, coins, points, bio, image, isadmin, username) VALUES(@name, @password, 20, 100, '', '', @isadmin, @username)";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("password", password);
                cmd.Parameters.AddWithValue("isadmin", isadmin);
                cmd.Prepare();

                cmd.ExecuteNonQuery();

                Console.WriteLine("row inserted");
                return true;
            }
            catch(Exception)
            {
                Console.WriteLine("Error while trying to insert in player");
                return false;
            }
        }


        //insert booster-------------------------------------------------------------------------------
        public bool insertBooster(int id)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();
                var sql = "INSERT INTO booster(boosterid,available) VALUES(@id, true)";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("id", id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                Console.WriteLine("row inserted");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error while trying to insert in booster");
                return false;
            }

        }
        //insert card-------------------------------------------------------------
        public bool insertCard(string cardid, string cardname, double damage, string cardtype, string element) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();
                var sql = "INSERT INTO card(cardid, cardname, damage, cardtype, element) VALUES(@cardid, @cardname, @damage, @cardtype, @element)";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("cardid", cardid);
                cmd.Parameters.AddWithValue("cardname", cardname);
                cmd.Parameters.AddWithValue("damage", damage);
                cmd.Parameters.AddWithValue("cardtype", cardtype);
                cmd.Parameters.AddWithValue("element", element);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                Console.WriteLine("row inserted");
      

                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error while trying to insert in card");
                return false;
            }
        
        }


        //insert boostercard-------------------------------------------------------------------
        public bool insertBoosterCard(string cardid, int boosterid)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();
                var sql = "INSERT INTO boostercard(cardid, boosterid) VALUES(@cardid, @boosterid)";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("cardid", cardid);
                cmd.Parameters.AddWithValue("boosterid", boosterid);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Console.WriteLine("row inserted");
                return true;
            }
            catch(Exception)
            {
                Console.WriteLine("Error while trying to insert in boostercard");
                return false;
            }
        }
        
        //insert into playercard---------------------------------------------------------------
        public bool insertPlayerCard(string user, string cardid) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();
                var sql = "INSERT INTO playercard(username, cardid, indeck) VALUES(@username, @cardid, false)";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("username", user);
                cmd.Parameters.AddWithValue("cardid", cardid);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Console.WriteLine("row inserted");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error while trying to insert in PlayerCard");
                return false;
            }
        }


        //insert into traiding table------------------------------------ 
        public bool insertTrading(string username,string tradeid, string cardid, double damage, string type) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();
                var sql = "INSERT INTO trading(username, cardid, damage, type, tradeid) VALUES(@username, @cardid, @damage, @type, @tradeid)";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("cardid", cardid);
                cmd.Parameters.AddWithValue("damage", damage);
                cmd.Parameters.AddWithValue("type", type);
                cmd.Parameters.AddWithValue("tradeid", tradeid);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Console.WriteLine("row inserted");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error while trying to insert in Trading");
                return false;
            }
        }

        //############################## update tables #########################################


        //---------------------------------------update player------------------------------------

        // update coins-----------------------
        // when plusMinus set true add coins, when plusMinus set false deduct coins
        public bool updatePlayerCoins(string username, int coins, bool plusMinus) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                string sql;
                if (plusMinus)
                {
                    sql = "UPDATE player SET coins = coins + @number WHERE username = @username";
                }
                else
                {
                    sql = "UPDATE player SET coins = coins - @number WHERE username = @username";
                    
                }

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("number", coins);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Console.WriteLine("row updated");
                return true;

            }
            catch (Exception)
            {
                Console.WriteLine("Error while trying update Coins");
                return false;
            }
        }
        //update points-----------------------------------------------
        // when plusMinus set true add points, when plusMinus set false deduct points
        public bool updatePlayerPoints(string username, int points, bool plusMinus) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                string sql;
                if (plusMinus)
                {
                    sql = "UPDATE player SET points = points + @number WHERE username = @username";
                }
                else
                {
                    sql = "UPDATE player SET points = points - @number WHERE username = @username";

                }

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("number", points);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Console.WriteLine("row updated");
                return true;

            }
            catch (Exception)
            {
                Console.WriteLine("Error while trying update Points");
                return false;
            }
        }
        //update rest of player data--------------------
        public bool updatePlayerData(string username, string newPlayerName, string newBio, string newImage)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();
                var sql = "UPDATE player SET playername = @newName, bio = @newBio, image = @newimage WHERE username = @username";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("newName", newPlayerName);
                cmd.Parameters.AddWithValue("newBio", newBio);
                cmd.Parameters.AddWithValue("newimage", newImage);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

               

                Console.WriteLine("row updated");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error while trying update PlayerData");
                return false;
            }
        }


        //---------------------------------------update playercard------------------------------------

        //changes ownership of card after trade------------------------------------------------------
        public bool updatePlayerCardTrade(string tradecard, string newowner)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();
                var sql = "UPDATE playercard SET username = @newowner, indeck = false WHERE cardid = @tradecard";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("newowner", newowner);
                cmd.Parameters.AddWithValue("tradecard", tradecard);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Console.WriteLine("row updated");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error while trying update PlayerCardTrade");
                return false;
            }
        }

        //changes on Table if card is in deck or not-------------------------------------
        public bool updatePlayerCardDeck(string username, string card, bool inDeck)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();
                
                var sql = "UPDATE playercard SET indeck = @inDeck WHERE cardid = @card AND username =@username";
                

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("inDeck", inDeck);
                cmd.Parameters.AddWithValue("card", card);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Console.WriteLine("row updated");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error while trying update PlayerCardDeck");
                return false;
            }
        }
        //takes all cards out of your deck
        public bool updatePlayerCardDeckEmpty(string username) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "UPDATE playercard SET indeck = false WHERE username =@username";


                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Console.WriteLine("row updated");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error while trying update PlayerCardDeckEmpty");
                return false;
            }
        }


        //update Booster when it is used-----------------------------------------------
        public bool updateBoosterUsed(int boosterID)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "UPDATE booster SET available = false WHERE boosterid = @boosterID";
            
           
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("boosterID", boosterID);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Console.WriteLine("row updated");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error while trying to update booster");
                return false;
            }

        }

        public bool updateCardBelongsToPlayer(string username, string cardId) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "UPDATE playercard SET username = @username WHERE cardid = @cardId";


                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("cardId", cardId);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Console.WriteLine("row updated");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error while trying to update booster");
                return false;
            }
        }



        //############################## delete #########################################

        public bool deleteTrade(string tradeId) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "DELETE FROM trading WHERE tradeId = @tradeId";


                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("tradeId", tradeId);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Console.WriteLine("row updated");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error while trying deleting on trading");
                return false;
            }

        }

        


        //##################################select statements###########################

        //check if player is already created-----------------------------------------------------
        public int selectPlayerCreated(string username)
        {

            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT COUNT(*) FROM player WHERE username = @username";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Prepare();

                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                int count = 0;
                while (rdr.Read())
                {
                    count = rdr.GetInt32(0);

                }

                return count;
            }
            catch(Exception){
                Console.WriteLine("Error while checking if Player is already created");
                return 2;
            }
        }
        //check if username and password is right
        public int selectPlayerPassword(string username, string password) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT COUNT(*) FROM player WHERE username = @username AND playerpassword = @password";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                cmd.Prepare();

                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                int count = 0;
                while (rdr.Read())
                {
                    count = rdr.GetInt32(0);

                }

                return count;
            }
            catch (Exception)
            {
                Console.WriteLine("Error while checking if Player is already created");
                return 2;
            }

        }

        public string selectPlayerData(string username) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT playername, coins, points, bio, image FROM player WHERE username = @username";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Prepare();

                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                string playerdata = "";
                while (rdr.Read())
                {
                    playerdata = "\nName: " + rdr.GetString(0) + "  /Coins: " + rdr.GetInt32(1) + "  /Points: " + rdr.GetInt32(2) + "  /Bio: " + rdr.GetString(3) + "  /Image: " + rdr.GetString(4)+ "\n";

                }

                return playerdata;
            }
            catch (Exception)
            {
                Console.WriteLine("Error while checking if Player is already created");
                string playerdata = "0";
                return playerdata;
            }

        }


        //select all cards of player. Gives info back as a List of strings -----------------------------------------
        public List<string> selectPlayerCards(string username)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT cardname, damage, cardtype, element, indeck FROM card INNER JOIN playercard ON card.cardid = playercard.cardid WHERE username = @username";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Prepare();

                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                int count = 1;
                List<string> cardData = new List<string>();

                while (rdr.Read())
                {
                    string oneline = count.ToString() + ". Name: " + rdr.GetString(0) + " /Damage: " + rdr.GetInt32(1).ToString() + " /Cardtype: " + rdr.GetString(2) + " /Element: " + rdr.GetString(3) + " /In deck?: " + rdr.GetBoolean(4).ToString();
                    cardData.Add(oneline);
                    count++;
                }

                return cardData;
            }
            catch (Exception)
            {
                List<string> cardData = new List<string>();
                cardData.Add("0");
                return cardData;

            }
        }


        //check deck of player
        public List<string> selectPlayerDeck(string username, int show)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT cardname, damage, cardtype, element FROM card INNER JOIN playercard ON card.cardid = playercard.cardid WHERE username = @username AND indeck = true";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Prepare();

                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                int count = 1;
                List<string> cardData = new List<string>();
                if (show == 0)
                {
                    while (rdr.Read())
                    {
                        string oneline = count.ToString() + ". Name: " + rdr.GetString(0) + " /Damage: " + rdr.GetInt32(1).ToString() + " /Cardtype: " + rdr.GetString(2) + " /Element: " + rdr.GetString(3);
                        cardData.Add(oneline);
                        count++;
                    }
                }
                else if (show == 1)
                {
                    while (rdr.Read()) 
                    {
                        string oneline = rdr.GetString(0) + "  " + rdr.GetInt32(1).ToString() + "  " + rdr.GetString(2) + "  " + rdr.GetString(3);
                        cardData.Add(oneline);
                       
                    }
                }
                if (cardData.Count == 0)
                {
                    cardData.Add("There is no card in your Deck");
                }

                return cardData;
            }
            catch (Exception)
            {
                List<string> cardData = new List<string>();
                cardData.Add("0");
                return cardData;

            }
        }


        //get number of cards in players deck
        public int selectPlayerDeckNumber(string username)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT COUNT(*) FROM playercard WHERE username = @username AND indeck = true";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Prepare();

                using NpgsqlDataReader rdr = cmd.ExecuteReader();


                int decknumber;

                rdr.Read();
                decknumber = rdr.GetInt32(0);

                return decknumber;
            }
            catch (Exception)
            {
                int decknumber = -1;
                return decknumber;

            }
        }


        // get the stats (points) of the player
        public string selectPlayerPoints(string username) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT playername, points FROM player  WHERE username = @username";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Prepare();

                using NpgsqlDataReader rdr = cmd.ExecuteReader();


                string cardData;

                rdr.Read();              
                cardData = "\n" +  rdr.GetString(0) + " gathered " + rdr.GetInt32(1) + " Points\n" ;
           
                return cardData;
            }
            catch (Exception)
            {
                string cardData = "0";
                return cardData;

            }

        }

        // get the number of coins the user has
        public int selectPlayerCoins(string username)
        {
            int coins;
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT coins FROM player  WHERE username = @username";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Prepare();

                using NpgsqlDataReader rdr = cmd.ExecuteReader();
          

                rdr.Read();
                coins =  rdr.GetInt32(0);

                return coins;
            }
            catch (Exception)
            {
                coins  = -1;
                return coins;
            }  
        }

        //get the overall scoreboard
        public string selectPlayerScoreboard() 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT playername, points FROM player WHERE isadmin = false ORDER BY points DESC";

                using var cmd = new NpgsqlCommand(sql, con);

                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                int count = 1;
                string scoreboard = "";

                while (rdr.Read())
                {
                    scoreboard += "\n" +  count.ToString() + ". Place: " + rdr.GetString(0) + " /Points: " + rdr.GetInt32(1).ToString();
                    count++;
                }

                return scoreboard;
            }
            catch (Exception)
            {
                string scoreboard = "0";
                return scoreboard;

            }

        }


        //get all booster ids that are were not used yet
        public List<int> selectUnusedBooster() 
        {
            try 
            { 
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT boosterid FROM booster WHERE available = true";

                using var cmd = new NpgsqlCommand(sql, con);

                using NpgsqlDataReader rdr = cmd.ExecuteReader();

          
                List<int> boosterid = new List<int>();

                while (rdr.Read())
                {
                    boosterid.Add (rdr.GetInt32(0));
         
                }

                return boosterid;
            }
            catch (Exception)
            {
                List<int> boosterid = new List<int>();
                boosterid.Add(0);
                return boosterid;

            }

}

        // check tradin angebote-----------------------------------------
        public string selectTradingOfferings() 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT p.playername, c.cardname, c.damage, c.cardtype, c.element, t.type, t.damage FROM trading t INNER JOIN card c ON t.cardid = c.cardid INNER JOIN player p ON t.username = p.username";

                using var cmd = new NpgsqlCommand(sql, con);

                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                
                string tradingOffers = "";

                while (rdr.Read())
                {
                    tradingOffers +=    "\n" + rdr.GetString(0) + " offers " + rdr.GetString(1) + " /Damage: " + rdr.GetDouble(2).ToString() + " /Cardtype: " + rdr.GetString(3) + " /Element: " + rdr.GetString(4) +
                                        "\nfor \n" +
                                        "Cardtype: " + rdr.GetString(5) + " with at least " + rdr.GetDouble(6).ToString() + " damage \n";
                    
                }

                if (tradingOffers == "")
                {
                    tradingOffers = "\nThere are no trade offerings at the moment\n";
                }

                return tradingOffers;
            }
            catch (Exception)
            {
                string tradingOffers = "0";
                return tradingOffers;

            }

        }
        //get all card ids of a booster------------------------------------
        public List<string> selectCardInBooster(int boosterid) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT cardid FROM boostercard WHERE boosterid = @boosterid";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("boosterid", boosterid);
                cmd.Prepare();
                using NpgsqlDataReader rdr = cmd.ExecuteReader();


                List<string> cardData = new List<string>();

                while (rdr.Read())
                {
                    cardData.Add(rdr.GetString(0));
                }
                return cardData;
            }
            catch (Exception)
            {
                List<string> cardData = new List<string>();
                cardData.Add("0");
                return cardData;

            }

        }
        //check if card is in deck---------------------------------------------
        public int selectCardInDeck(string cardId) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT COUNT(*) FROM playercard WHERE cardid = @cardid AND indeck = true";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("cardid", cardId);
                cmd.Prepare();
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                int inDeck = 0;
                while (rdr.Read())
                {
                    inDeck = (rdr.GetInt32(0));
                }

                return inDeck;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        //check if card belongs to a certan player-------------------------------------
        public int selectCardBelongsToPlayer(string username, string cardId) 
        {
            try 
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT COUNT(*) FROM playercard WHERE cardid = @cardid AND username = @username";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("cardid", cardId);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Prepare();
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                int belongsToUser = 0;
                while (rdr.Read())
                {
                    belongsToUser = (rdr.GetInt32(0));
                }

                return belongsToUser;
            }

            catch(Exception)
            {
                return -1;
            }
        
        }

        //check if trading deal belongs to a certan player-------------------------------------
        public int selectTradeBelongsToPlayer(string username, string tradeId)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT COUNT(*) FROM trading WHERE tradeid = @tradeid AND username = @username";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("tradeid", tradeId);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Prepare();
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                int belongsToUser = 0;
                while (rdr.Read())
                {
                    belongsToUser = (rdr.GetInt32(0));
                }

                return belongsToUser;
            }
            catch(Exception)
            {
                return -1;

            }
        }


        //check if trade exists
        public int selectTradeExists(string tradeId) 
        {

            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT COUNT(*) FROM trading WHERE tradeid = @tradeid";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("tradeid", tradeId);
                cmd.Prepare();
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                int tradeExists = 0;
                while (rdr.Read())
                {
                    tradeExists = (rdr.GetInt32(0));
                }

                return tradeExists;
            }
            catch (Exception)
            {
                return -1;
            }

        
        }

        //get damage of card
        public double selectCardDamage(string cardId)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT damage FROM card WHERE cardid = @cardId";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("cardId", cardId);
                cmd.Prepare();
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                double damage = 0;
                while (rdr.Read())
                {
                    damage = (rdr.GetInt32(0));
                }

                return damage;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public string selectCardType(string cardId)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT cardtype FROM card WHERE cardid = @cardId";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("cardId", cardId);
                cmd.Prepare();
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                string type = "";
                while (rdr.Read())
                {
                    type = (rdr.GetString(0));
                }

                return type;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public int selectMeetsTradeCondition(string tradeId, double damage, string type)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT COUNT(*) FROM trading WHERE tradeid = @tradeId AND type = @type AND damage <= @damage";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("tradeId", tradeId);
                cmd.Parameters.AddWithValue("type", type);
                cmd.Parameters.AddWithValue("damage", damage);
                cmd.Prepare();
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                int meetsCondition = 0;
                while (rdr.Read())
                {
                    meetsCondition = (rdr.GetInt32(0));
                }

                return meetsCondition;
            }
            catch
            {
                return -1;
            }
        }


        //get cardid from trading table
        public string selectCardInTrade(string tradeId)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT cardid FROM trading WHERE tradeid = @tradeId";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("tradeId", tradeId);
                cmd.Prepare();
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                string cardId = "";
                while (rdr.Read())
                {
                    cardId = (rdr.GetString(0));
                }

                return cardId;
            }
            catch
            {
                return "";
            }
        }

        public string selectUserInTrade(string tradeId)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT username FROM trading WHERE tradeid = @tradeId";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("tradeId", tradeId);
                cmd.Prepare();
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                string username = "";
                while (rdr.Read())
                {
                    username = (rdr.GetString(0));
                }

                return username;
            }
            catch
            {
                return "";
            }

        }


        //Get ID of every card in deck of user
        public List<string> selectCardIdInDeck(string username)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT cardid FROM playercard WHERE username = @username AND indeck = true";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Prepare();
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                List<string> cards = new List<string>();
                while (rdr.Read())
                {
                    cards.Add(rdr.GetString(0));
                }

                return cards;
            }
            catch
            {
                List<string> cards = new List<string>();
                cards.Add("0");
                return cards;
            }
        }

        public List<string> selectCardData(string cardId)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT cardname, damage, cardtype, element FROM card WHERE cardid = @cardId";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("cardId", cardId);
                cmd.Prepare();
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                List<string> cardData = new List<string>();
                while (rdr.Read())
                {
                    cardData.Add(rdr.GetString(0));
                    cardData.Add(rdr.GetDouble(1).ToString());
                    cardData.Add(rdr.GetString(2));
                    cardData.Add(rdr.GetString(3));
                }

                return cardData;
            }
            catch
            {
                List<string> cardData = new List<string>();
                cardData.Add("0");
                return cardData;
            }
        }
        public int selectPlayerJustPoints(string username)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT points FROM player WHERE username = @username";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Prepare();
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                int points =0;
                while (rdr.Read())
                {
                    points = (rdr.GetInt32(0));
                }

                return points;
            }
            catch
            {
                return 0;
            }
        }

    }

    
}
