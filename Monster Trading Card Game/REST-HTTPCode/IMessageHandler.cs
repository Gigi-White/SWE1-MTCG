using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game.REST_HTTPCode
{
    interface IMessageHandler
    {

        public void CheckType(List<string>login);

        public void CheckOrderGet(List<string> login);

        public void CheckOrderPost(List<string> login);
        public void CheckoutOrderPut(List<string> login);

        //unknown Type
        public void WrongType();

        //unknown order
        public void WrongOrder();
        //additional method for orders with playername in it (/users/username)
        public string CheckOrderUserAddition(string order);

        //sends response back to Client
        public void ServerResponse(string status, string mime, string data);


        //create player/user (is not in CheckOrderPost)
        public void HandlePostUsers();

        // creates booster when user is logged in
        public void CreateBooster(List<string> login);

        // login user / check if user is loged in 
        public List<string> LoginPlayer(List<string> login);

        //change user Data
        public void ChangeUserData(List<string> login, string name);

        //put cards in deck
        public void SetDeck(List<string> login);
        
        //put trading offer on marketplace
        public void PutOutTradingOffer(List<string>login);

        // buy a new booster
        public void AcquirePackage(List<string> login);

        // show all cards
        public void ShowCards(List<string> login);

        // show deck of user
        public void ShowDeck(List<string> login, int show);

        //show points of player
        public void ShowPlayerStats(List<string> login);

        //show scoreboard
        public void ShowPlayerScores(List<string> login);

        //show all trading deals
        public void ShowTradingDeals(List<string>login);


        //show all cards out of your deck
        public void UnsetDeck(List<string> login);

        //show User Data
        public void GetUserData(List<string> login, string name);


    }
}
