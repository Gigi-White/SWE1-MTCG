using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Monster_Trading_Card_Game
{
    class Battle
    {
        private string UserOne;
        private string UserTwo;
        private List<Card> DeckOne;
        private List<Card> DeckTwo;
        private int Rounds = 1;
        private List<string> Log = new List<string>();
        private winner betterfighter;


    public Battle(List<Card> playerOne, List<Card> playerTwo, string firstUser, string secondUser)
        {
            DeckOne = playerOne;
            DeckTwo = playerTwo;

            UserOne = firstUser;
            UserTwo = secondUser;

        }
        
        //----------------------Hauptfunktion welche das Spiel berechnet------------------------------
        public winner BattleHandler() 
        {
            

            while(GameContinue()) //Ist das Spiel vorbei?
            {
                Round(); //Wenn nein-->Eine Runde wird gespielt
            }

            DeterminWinner();  //Wenn ja-->Wer hat gewonnen?



            //Nochmal ein Log Eintrag wer gewonnen hat
            Log.Add("Spiel ist beendet");
            if (betterfighter == winner.FirstPlayer)
            {
                Log.Add("#####  " + UserOne + " hat gewonnen  #####");
            }
            else if(betterfighter == winner.SekondPlayer)
            {
                Log.Add("#####  " + UserTwo + " hat gewonnen  #####");
            }
            else
            {
                Log.Add("#####  unentschiden  #####");
            }


            //Ausgabe des Logs
            foreach (var sentence in Log) 
            {
                Console.WriteLine(sentence);
            } 

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
                betterfighter = winner.FirstPlayer;
            }
            else if(DeckTwo.Count > DeckOne.Count)
            {
                betterfighter = winner.SekondPlayer;
            }
            else
            {
                betterfighter = winner.Draw;
            }

        }


        //-------------------------------------------Funktion die eine Kampfrunde berechnet---------------------------
        private void Round()
        {
            //wer hat zuerst angegriffen
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

            Log.Add("Runde " + Rounds + ": ");
            if (turn == 1) 
            {
                Log.Add(DeckOne[cardPlayerOne].Creature + " von " + UserOne + " greift zuerst an und verursacht " + DeckTwo[cardPlayerTwo].Damage + " Schaden");
                Log.Add(DeckTwo[cardPlayerTwo].Creature + " von " + UserTwo + " greift als Zweites an und verursacht " + DeckOne[cardPlayerOne].Damage + " Schaden");
            }
            else
            {
                Log.Add(DeckTwo[cardPlayerTwo].Creature + " von " + UserTwo + " greift zuerst an und verursacht " + DeckOne[cardPlayerOne].Damage + " Schaden");
                Log.Add(DeckOne[cardPlayerOne].Creature + " von " + UserOne + " greift als Zweites an und verursacht " + DeckTwo[cardPlayerTwo].Damage + " Schaden");
                
            }
            
            
            //Der Gewinner bekommt die Karte des Verlierers in sein Deck
            
            //Wenn Schaden gleich ist gewinnt der, der zuerst angegriffen hat
            if (DeckOne[cardPlayerOne].Damage == DeckTwo[cardPlayerTwo].Damage) 
            {

                
                Log.Add("Beide Karten haben gleich viel Schaden verursacht"); //Eintrag im Log bei Gleichstand

                //Schaden beider Karten wird auf 0 gestellt
                DeckOne[cardPlayerOne].Damage = 0;
                DeckTwo[cardPlayerTwo].Damage = 0;
                
                
                if (turn == 1)  //UserOne gewinnt diese Runde 
                {
                    Log.Add(UserOne + " gewinnt diese Runde. " +  DeckTwo[cardPlayerTwo].Creature + " kommt in Deck von " + UserOne);
                    
                    DeckOne.Add(DeckTwo[cardPlayerTwo]);
                    DeckTwo.RemoveAt(cardPlayerTwo);
                }
                else //UserTwo gewinnt diese Runde
                {
                    Log.Add(UserTwo + " gewinnt diese Runde. " + DeckOne[cardPlayerOne].Creature + " kommt in Deck von " + UserTwo);

                    DeckTwo.Add(DeckOne[cardPlayerOne]);
                    DeckOne.RemoveAt(cardPlayerOne);
                }
            }
            //Wenn der Schaden auf Der Karte von PlayerOne größer ist, gewinnt Player Two
            else if (DeckOne[cardPlayerOne].Damage > DeckTwo[cardPlayerTwo].Damage)
            {
                Log.Add(DeckTwo[cardPlayerTwo].Creature + " von " + UserTwo + " hat mehr Schaden ausgeteilt. ");
                Log.Add(UserTwo + " gewinnt diese Runde. " + DeckOne[cardPlayerOne].Creature + " kommt in Deck von " + UserTwo);

                DeckOne[cardPlayerOne].Damage = 0;
                DeckTwo[cardPlayerTwo].Damage = 0;

                DeckTwo.Add(DeckOne[cardPlayerOne]);
                DeckOne.RemoveAt(cardPlayerOne);
            }

            //Wenn der Schaden auf Der Karte von PlayerOne kleiner ist, gewinnt Player One
            else if (DeckOne[cardPlayerOne].Damage < DeckTwo[cardPlayerTwo].Damage)
            {
                Log.Add(DeckOne[cardPlayerOne].Creature + " von " + UserOne + " hat mehr Schaden ausgeteilt. ");
                Log.Add(UserOne + " gewinnt diese Runde. " + DeckTwo[cardPlayerTwo].Creature + " kommt in Deck von " + UserOne);

                DeckOne[cardPlayerOne].Damage = 0;
                DeckTwo[cardPlayerTwo].Damage = 0;

                DeckOne.Add(DeckTwo[cardPlayerTwo]);
                DeckTwo.RemoveAt(cardPlayerTwo);
            }
            Log.Add(UserOne + " hat noch " + DeckOne.Count + " Karten im Deck");
            Log.Add(UserTwo + " hat noch " + DeckTwo.Count + " Karten im Deck");
            Log.Add("##########################################################################################################");
            Log.Add("");
            Rounds++;

        }
    }
}
