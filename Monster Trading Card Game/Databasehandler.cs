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


        public bool insertPlayer(string name, string password)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();

                var sql = "INSERT INTO player(playername, playerpassword, coins, points) VALUES(@name, @password,20,100)";
                using var cmd = new NpgsqlCommand(sql, con);

                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("password", password);
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

        //changes ownership of card after trade
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

        //changes on Table if card is in deck or not
        public bool updatePlayerCardDeck(string card, bool inDeck)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

                using var con = new NpgsqlConnection(cs);
                con.Open();
                
                var sql = "UPDATE playercard SET indeck = @inDeck WHERE cardid = @card";
                

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("inDeck", inDeck);
                cmd.Parameters.AddWithValue("card", card);
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

        //check if player is already created
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

        //select all cards of player. Gives info back as a List of strings
        public List<string> selectPlayerCards(string player)
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
    }
}
