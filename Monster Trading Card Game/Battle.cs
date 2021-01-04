using Monster_Trading_Card_Game.Cardclasses.MonsterCardFolder;
using Monster_Trading_Card_Game.Enums;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Monster_Trading_Card_Game
{
    public class Battle
    {
        private string UserOne;
        private string UserTwo;
        private List<Card> DeckOne;
        private List<Card> DeckTwo;
        private int Rounds = 1;
        public List<string> Log = new List<string>();
        public Winner betterfighter;
        private IDatabasehandler Database;


    public Battle( string playerOne, string playerTwo)
        {
            Database = new Databasehandler();
            

            UserOne = playerOne;
            UserTwo = playerTwo;

            DeckOne = CreateDeck(playerOne);
            DeckTwo = CreateDeck(playerTwo);

        }
        // for tests
        public Battle(string playerOne, string playerTwo, List<Card> deckOne, List<Card> deckTwo)
        {
            Database = new Databasehandler();


            UserOne = playerOne;
            UserTwo = playerTwo;

            DeckOne = deckOne;
            DeckTwo = deckTwo;

        }


        //----------------------Hauptfunktion welche das Spiel berechnet------------------------------
        public Winner BattleHandler() 
        {
            

            while(GameContinue()) //Ist das Spiel vorbei?
            {
                Round(); //Wenn nein-->Eine Runde wird gespielt
            }

            DeterminWinner();  //Wenn ja-->Wer hat gewonnen?



            //Nochmal ein Log Eintrag wer gewonnen hat und addieren bzw abziehen der Punkte 
            Log.Add("Game Over");
            if (betterfighter == Winner.FirstPlayer)
            {
                Log.Add("\n#####  " + UserOne + " has won the game and receives 3 points #####\n");
                Database.updatePlayerPoints(UserOne, 3, true);

                if (Database.selectPlayerJustPoints(UserTwo) - 5 > 0)
                {
                    Database.updatePlayerPoints(UserTwo, 5, false);
                    Log.Add("\n#####  " + UserTwo + " has lost the game and loses 5 points #####\n");
                }
                else
                {
                    Log.Add("\n#####  " + UserTwo + " has lost the game but can not drop deeper than 0 points #####\n");
                }
                
            }
            else if(betterfighter == Winner.SekondPlayer)
            {
                Log.Add("\n#####  " + UserTwo + " has won the game and receives 3 points #####\n");
                Database.updatePlayerPoints(UserTwo, 3, true);

                if (Database.selectPlayerJustPoints(UserOne) - 5 > 0)
                {
                    Database.updatePlayerPoints(UserOne, 5, false);
                    Log.Add("\n#####  " + UserOne + " has lost and loses 5 points #####\n");
                }
                else
                {
                    Log.Add("\n#####  " + UserOne + " has lost but can not drop deeper than to 0 points #####\n");
                }
            }
            else
            {
                Log.Add("#####  draw  #####");
            }


            //Ausgabe des Logs
            /*foreach (var sentence in Log) 
            {
                Console.WriteLine(sentence);
            }*/ 

            return betterfighter; //Sieger rausgeben 
        }


        
        //------------------------------Funktion die prüft ob das Spiel vorbei ist------------------------------
        private bool GameContinue() 
        {
            bool nextround = true;


            if(Rounds>100)
            {
                nextround = false;
            }

            else if(DeckOne.Count == 0 || DeckTwo.Count == 0)
            {
                nextround = false;
            }

            return nextround;
        }


        //----------------------------------Funktion die prüft wer gewonnen hat----------------------------------
        private void DeterminWinner()
        {
            if (DeckOne.Count > DeckTwo.Count)
            {
                betterfighter = Winner.FirstPlayer;
            }
            else if(DeckTwo.Count > DeckOne.Count)
            {
                betterfighter = Winner.SekondPlayer;
            }
            else
            {
                betterfighter = Winner.Draw;
            }

        }


        //-------------------------------------------Funktion die eine Kampfrunde berechnet---------------------------
        public void Round()
        {
            //wer greift zuerst an
            Random rnd = new Random();
            int turn = rnd.Next(1, 3);
            //welche Karte wird von dem jeweiligen Deck hergenommen?
            int cardPlayerOne = rnd.Next(0, DeckOne.Count);
            int cardPlayerTwo = rnd.Next(0, DeckTwo.Count);

            //zur Sicherheit werden die Schadenswerte beider Karten vor dem Kampf noch auf 0 gesetzt
            DeckOne[cardPlayerOne].Damage = 0;
            DeckTwo[cardPlayerTwo].Damage = 0;

            //Karten fügen sich gegenseitig Schaden zu
            DeckOne[cardPlayerOne].SetDamage(DeckTwo[cardPlayerTwo], DeckTwo[cardPlayerTwo].Attack(DeckOne[cardPlayerOne]));
            DeckTwo[cardPlayerTwo].SetDamage(DeckOne[cardPlayerOne], DeckOne[cardPlayerOne].Attack(DeckTwo[cardPlayerTwo]));



            //Log Eintrag wer wie viel Schaden verursacht

            Log.Add("Round " + Rounds + ": ");
            if (turn == 1) 
            {
                Log.Add("\n" + DeckOne[cardPlayerOne].Creature + " of " + UserOne + " attacks first and deals " + DeckTwo[cardPlayerTwo].Damage + " damage");
                Log.Add("\n" + DeckTwo[cardPlayerTwo].Creature + " of " + UserTwo + " attacks second and deals " + DeckOne[cardPlayerOne].Damage + " damage");
            }
            else
            {
                Log.Add("\n" + DeckTwo[cardPlayerTwo].Creature + " of " + UserTwo + " attacks first and deals " + DeckOne[cardPlayerOne].Damage + " damage");
                Log.Add("\n" + DeckOne[cardPlayerOne].Creature + " of " + UserOne + " attacks second and deals " + DeckTwo[cardPlayerTwo].Damage + " damage");
                
            }
            
            
            //Der Gewinner bekommt die Karte des Verlierers in sein Deck
            
            //Wenn Schaden gleich ist gewinnt der, der zuerst angegriffen hat
            if (DeckOne[cardPlayerOne].Damage == DeckTwo[cardPlayerTwo].Damage) 
            {

                
                Log.Add("\n" + "Both cards dealt the same amount of damage"); //Eintrag im Log bei Gleichstand

                //Schaden beider Karten wird auf 0 gestellt
                DeckOne[cardPlayerOne].Damage = 0;
                DeckTwo[cardPlayerTwo].Damage = 0;
                
                
                if (turn == 2)  //UserOne gewinnt diese Runde 
                {
                    Log.Add("\n" + UserOne + " winns this round. " +  DeckTwo[cardPlayerTwo].Creature + " gets in Deck of " + UserOne);
                    
                    DeckOne.Add(DeckTwo[cardPlayerTwo]);
                    DeckTwo.RemoveAt(cardPlayerTwo);
                }
                else //UserTwo gewinnt diese Runde
                {
                    Log.Add("\n" + UserTwo + " winns this round. " + DeckOne[cardPlayerOne].Creature + " gets in Deck of " + UserTwo);

                    DeckTwo.Add(DeckOne[cardPlayerOne]);
                    DeckOne.RemoveAt(cardPlayerOne);
                }
            }
            //Wenn der Schaden auf Der Karte von PlayerOne größer ist, gewinnt Player Two
            else if (DeckOne[cardPlayerOne].Damage > DeckTwo[cardPlayerTwo].Damage)
            {
                Log.Add("\n" + DeckTwo[cardPlayerTwo].Creature + " of " + UserTwo + " dealt more damage. ");
                Log.Add("\n" + UserTwo + " winns this round. " + DeckOne[cardPlayerOne].Creature + " gets in Deck of " + UserTwo);

                DeckOne[cardPlayerOne].Damage = 0;
                DeckTwo[cardPlayerTwo].Damage = 0;

                DeckTwo.Add(DeckOne[cardPlayerOne]);
                DeckOne.RemoveAt(cardPlayerOne);
            }

            //Wenn der Schaden auf Der Karte von PlayerOne kleiner ist, gewinnt Player One
            else if (DeckOne[cardPlayerOne].Damage < DeckTwo[cardPlayerTwo].Damage)
            {
                Log.Add("\n" + DeckOne[cardPlayerOne].Creature + " of " + UserOne + " dealt more damage. ");
                Log.Add("\n" + UserOne + " winns this round. " + DeckTwo[cardPlayerTwo].Creature + " gets in Deck of " + UserOne);

                DeckOne[cardPlayerOne].Damage = 0;
                DeckTwo[cardPlayerTwo].Damage = 0;

                DeckOne.Add(DeckTwo[cardPlayerTwo]);
                DeckTwo.RemoveAt(cardPlayerTwo);
            }
            Log.Add("\n" + UserOne + " has " + DeckOne.Count + " cards left in the deck ");
            Log.Add("\n" + UserTwo + " has " + DeckTwo.Count + " cards left in the deck ");
            Log.Add("\n" + "##########################################################################################################");
            Log.Add("\n");
            Rounds++;

        }


        //------------------------------------create deck for the player form the Database----------------------------------------
        private List<Card>CreateDeck(string user)
        {
            List<Card> deck = new List<Card>();
            List<string> cards = new List<string>();

            cards = Database.selectCardIdInDeck(user);
            foreach(string card in cards)
            {
                deck.Add(CreateCard(card));
            }
            return deck;
        }

        //----------------------------------------------------create the card-----------------------------------------------------------------
        private Card CreateCard(string cardId)
        {
            List<string> cardData = Database.selectCardData(cardId);
            string cardname = cardData[0];
            float damage = float.Parse(cardData[1]);
            string cardtype = cardData[2];
            string element = cardData[3];
            
            if(cardtype == "Monster")
            {
                switch (cardname)
                {
                    case "Dragon":
                        Card dragonCard = new Dragon(damage);
                        return dragonCard;
                    case "FireElfe":
                        Card elfCard = new FireElfe(damage);
                        return elfCard;
                    case "Knight":
                        Card knightCard = new Knight(damage);
                        return knightCard;
                    case "Ork":
                        Card orkCard = new Ork(damage);
                        return orkCard;
                    case "WaterGoblin":
                        Card goblinCard = new WaterGoblin(damage);
                        return goblinCard;
                    case "Wizzard":
                        Card wizzardCard = new Wizzard(damage);
                        return wizzardCard;
                }
            }
            else if (cardtype == "Spell")
            {
                switch (cardname)
                {
                    case "RegularSpell":
                        Card normalCard = new NormalSpell(damage);
                        return normalCard;
                    case "FireSpell":
                        Card fireCard = new FireSpell(damage);
                        return fireCard;
                    case "WaterSpell":
                        Card waterCard = new WaterSpell(damage);
                        return waterCard;
                }
            }


            return null;
        }
    }
}
