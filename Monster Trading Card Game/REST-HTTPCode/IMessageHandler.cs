using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game.REST_HTTPCode
{
    interface IMessageHandler
    {

        public void CheckType(List<string>login);

        //extra methode to login user / check if user is loged in 
        public List<string> LoginPlayer(List<string> login);

        //extra methode that that creates booster when user is logged in
        public void CreateBooster(List<string> login);
        //extra methode to buy a new booster
        public void AcquirePackage(List<string>login);
        // methode to show all cards
        public void ShowCards(List<string> login);
        // show deck of user
        public void ShowDeck(List<string> login, int show);

        //put cards in deck
        public void SetDeck(List<string> login);

        //get all cards out of your deck
        public void UnsetDeck(List<string> login);


        //checkout if message is Get Post------------------------------------
        public void CheckOrderGet(List<string>login);

        public void CheckOrderPost(List<string> login);
        public void CheckoutOrderPut(List<string> login);

        public void WrongType();


        //Send Response------------------------------------------------------
        public void ServerResponse(string status, string mime, string data);


        //handle Post requests-------------------------------------------------


        //create player/user
        public void HandlePostUsers();
       


    }
}
