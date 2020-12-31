using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game.REST_HTTPCode
{
    interface IMessageHandler
    {

        public bool CheckType();

        //extra methode to login user / check if user is loged in 
        public List<string> LoginPlayer(List<string> login);

        //extra methode that that creates booster when user is logged in
        public void CreateBooster(List<string> login);

        //checkout if message is Get Post------------------------------------
        public void CheckOrderGet();

        public void CheckOrderPost();
        public void CheckoutOrderPut();

        public void WrongType();


        //Send Response------------------------------------------------------
        public void ServerResponse(string status, string mime, string data);


        //handle Post requests-------------------------------------------------


        //create player/user
        public void HandlePostUsers();
        
        //login player/user
        public void HandlePostSession();


    }
}
