using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    public interface IDatabasehandler
    {
        //inserts------------------------------------------------------------------------------
        public bool insertPlayer(string name, string password, bool isadmin);

        public bool insertBooster(int id);

        public bool insertCard(string cardid, string cardname, double damage, string cardtype, string element);

        public bool insertBoosterCard(string cardid, int boosterid);

        public bool insertPlayerCard(string player, string cardid);

        public bool insertTrading(string player, string cardid, double damage, string type);


        //updates------------------------------------------------------------------------------------

        public bool updatePlayerCoins(string player, int coins, bool plusMinus);
        public bool updatePlayerPoints(string player, int points, bool plusMinus);

        public bool updatePlayerData(string player, string newPlayerName, string newBio, string newImage);

        public bool updatePlayerCardTrade(string tradecard, string newowner);

        public bool updatePlayerCardDeck(string playername, string card, bool inDeck);

        public bool updateBoosterUsed(int boosterID);


        //delete

        public bool deleteTrade(string card);


        //select

        public int selectPlayerCreated(string playername);

        public int selectPlayerPassword(string playername, string password);

        public List<string> selectPlayerCards(string playername);

        public List<string> selectPlayerDeck(string playername, int show);
        public int selectPlayerDeckNumber(string playername);

        public string selectPlayerPoints(string playername);

        public int selectPlayerCoins(string playername);

        public List<string> selectPlayerScoreboard();

        public List<int> selectUnusedBooster();

        public List<string> selectTradingOfferings();

        public List<string> selectCardInBooster(int boosterid);

        public List<int> selectBoosterNotUsed();
    }
}
