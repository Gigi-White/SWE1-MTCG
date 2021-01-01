using System;
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

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "INSERT INTO player(playername, playerpassword, coins, points, isadmin) VALUES(@name, @password,20,100,@isadmin)";
                using var cmd = new NpgsqlCommand(sql, con);

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
        public bool insertPlayerCard(string player, string cardid) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();
                var sql = "INSERT INTO playercard(player, cardid, indeck) VALUES(@player, @cardid, false)";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("player", player);
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
        public bool insertTrading(string player, string cardid, double damage, string type) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();
                var sql = "INSERT INTO trading(playername, cardid, damage, type) VALUES(@player, @cardid, @damage, @type)";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("player", player);
                cmd.Parameters.AddWithValue("cardid", cardid);
                cmd.Parameters.AddWithValue("damage", damage);
                cmd.Parameters.AddWithValue("type", type);
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
        public bool updatePlayerCoins(string player, int coins, bool plusMinus) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                string sql;
                if (plusMinus)
                {
                    sql = "UPDATE player SET coins = coins + @number WHERE playername = @player";
                }
                else
                {
                    sql = "UPDATE player SET coins = coins - @number WHERE playername = @player";
                    
                }

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("player", player);
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
        public bool updatePlayerPoints(string player, int points, bool plusMinus) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                string sql;
                if (plusMinus)
                {
                    sql = "UPDATE player SET points = points + @number WHERE playername = @player";
                }
                else
                {
                    sql = "UPDATE player SET points = points - @number WHERE playername = @player";

                }

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("player", player);
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
        public bool updatePlayerData(string player, string newPlayerName, string newBio, string newImage)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();
                var sql = "UPDATE player SET playername = @newName, bio = @newBio, image = @newimage WHERE playername = @player";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("newName", newPlayerName);
                cmd.Parameters.AddWithValue("newBio", newBio);
                cmd.Parameters.AddWithValue("newimage", newImage);
                cmd.Parameters.AddWithValue("player", player);
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
                var sql = "UPDATE playercard SET player = @newowner, indeck = false WHERE cardid = @tradecard";
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
        public bool updatePlayerCardDeck(string playername, string card, bool inDeck)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();
                
                var sql = "UPDATE playercard SET indeck = @inDeck WHERE cardid = @card AND player =@playername";
                

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("inDeck", inDeck);
                cmd.Parameters.AddWithValue("card", card);
                cmd.Parameters.AddWithValue("playername", playername);
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

        //############################## delete #########################################

        public bool deleteTrade(string card) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "DELETE FROM trading WHERE cardid = @card";


                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("card", card);
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
        public int selectPlayerCreated(string playername)
        {

            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT COUNT(*) FROM player WHERE playername = @playername";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("playername", playername);
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
        public int selectPlayerPassword(string playername, string password) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT COUNT(*) FROM player WHERE playername = @playername AND playerpassword = @password";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("playername", playername);
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




        //select all cards of player. Gives info back as a List of strings -----------------------------------------
        public List<string> selectPlayerCards(string player)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT cardname, damage, cardtype, element, indeck FROM card INNER JOIN playercard ON card.cardid = playercard.cardid WHERE player = @player";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("player", player);
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
        public List<string> selectPlayerDeck(string playername, int show)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT cardname, damage, cardtype, element FROM card INNER JOIN playercard ON card.cardid = playercard.cardid WHERE player = @player AND indeck = true";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("player", playername);
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
        public int selectPlayerDeckNumber(string playername)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT COUNT(*) FROM playercard WHERE player = @player AND indeck = true";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("player", playername);
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
        public string selectPlayerPoints(string playername) 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT points FROM player  WHERE playername = @player";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("player", playername);
                cmd.Prepare();

                using NpgsqlDataReader rdr = cmd.ExecuteReader();


                string cardData;

                rdr.Read();              
                cardData = "You gathered " + rdr.GetInt32(0) + " Points" ;
           
                return cardData;
            }
            catch (Exception)
            {
                string cardData = "0";
                return cardData;

            }

        }

        // get the number of coins the user has
        public int selectPlayerCoins(string playername)
        {
            int coins;
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT coins FROM player  WHERE playername = @player";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("player", playername);
                cmd.Prepare();

                using NpgsqlDataReader rdr = cmd.ExecuteReader();


                string cardData;

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
        public List<string> selectPlayerScoreboard() 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT playername, points FROM player ORDER BY points DESC";

                using var cmd = new NpgsqlCommand(sql, con);

                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                int count = 1;
                List<string> cardData = new List<string>();

                while (rdr.Read())
                {
                    string oneline = count.ToString() + ". Place: " + rdr.GetString(0) + " /Points: " + rdr.GetInt32(1).ToString();
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
        public List<string> selectTradingOfferings() 
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT t.playername, c.cardname, c.damage, c.cardtype, c.element, t.type, t.damage " +
                            "FROM trading t INNER JOIN card c ON t.cardid = c.cardid";

                using var cmd = new NpgsqlCommand(sql, con);

                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                
                List<string> cardData = new List<string>();

                while (rdr.Read())
                {
                    string oneline =    rdr.GetString(0) + " offers " + rdr.GetString(1) + " /Damage: " + rdr.GetDouble(2).ToString() + " /Cardtype: " + rdr.GetString(3) + " /Element: " + rdr.GetString(4) +
                                        "\nfor \n" +
                                        "Cardtype: " + rdr.GetString(5) + " with at least " + rdr.GetDouble(6).ToString() + " damage \n";
                    cardData.Add(oneline);
                    
                }

                if (cardData.Count == 0)
                {
                    cardData.Add("There are no trade offerings at the moment");
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
        //get booster that are were not used yet-------------------------------
        public List<int> selectBoosterNotUsed()
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "SELECT boosterid FROM booster  WHERE available = true";

                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader rdr = cmd.ExecuteReader();


                List<int> boosterData = new List<int>();

                while (rdr.Read())
                {
                    boosterData.Add(rdr.GetInt32(0));
                }
                return boosterData;
            }
            catch (Exception)
            {
                List<int> boosterData = new List<int>();
                boosterData.Add(0);
                return boosterData;

            }
        }


    }
}
