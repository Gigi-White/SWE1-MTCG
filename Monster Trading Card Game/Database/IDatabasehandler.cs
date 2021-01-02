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

        public bool insertTrading(string username, string tradeid, string cardid, double damage, string type);


        //updates------------------------------------------------------------------------------------

        public bool updatePlayerCoins(string player, int coins, bool plusMinus);
        public bool updatePlayerPoints(string player, int points, bool plusMinus);

        public bool updatePlayerData(string player, string newPlayerName, string newBio, string newImage);

        public bool updatePlayerCardTrade(string tradecard, string newowner);

        public bool updatePlayerCardDeck(string playername, string card, bool inDeck);

        public bool updatePlayerCardDeckEmpty(string playername);

        public bool updateBoosterUsed(int boosterID);


        //delete

        public bool deleteTrade(string card);


        //select

        public int selectPlayerCreated(string playername);

        public int selectPlayerPassword(string playername, string password);

        public string selectPlayerData(string playername);

        public List<string> selectPlayerCards(string playername);

        public List<string> selectPlayerDeck(string playername, int show);
        public int selectPlayerDeckNumber(string playername);

        public string selectPlayerPoints(string playername);

        public int selectPlayerCoins(string playername);

        public string selectPlayerScoreboard();

        public List<int> selectUnusedBooster();

        public string selectTradingOfferings();

        public List<string> selectCardInBooster(int boosterid);

        public int selectCardInDeck(string cardId);

        //check if certan card belongs to certen player
        public int selectCardBelongsToPlayer(string username, string cardId);

    }
}
