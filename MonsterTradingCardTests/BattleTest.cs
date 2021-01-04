using Monster_Trading_Card_Game;
using Monster_Trading_Card_Game.Enums;
using Monster_Trading_Card_Game.Cardclasses.MonsterCardFolder;
using NUnit.Framework;
using System.Collections.Generic;

namespace MonsterTradingCardTests
{
    [TestFixture]
    public class BattleTest
    {
        //--------------------------------------Battle Handler Test------------------------------------


        [Test]

        public void BattleTestOne() //Spieler Eins sollte gewinnen
        {
            List<Card> DeckEins = new List<Card>();
            List<Card> DeckZwei = new List<Card>();

            DeckEins.Add(new Dragon(45));
            DeckEins.Add(new Dragon(50));
            DeckEins.Add(new Dragon(48));

            DeckZwei.Add(new WaterGoblin(20));
            DeckZwei.Add(new WaterGoblin(35));
            DeckZwei.Add(new WaterGoblin(30));


            Battle Fight = new Battle("Yugi", "Kaiba", DeckEins, DeckZwei);
            var actualwinner = Fight.BattleHandler();

            Assert.AreEqual(Winner.FirstPlayer, actualwinner);
        }

        [Test]
        public void BattleTestTwo() //Spieler Zwei muss gewinnen
        {
            List<Card> DeckEins = new List<Card>();
            List<Card> DeckZwei = new List<Card>();

            DeckZwei.Add(new WaterGoblin(32));
            DeckZwei.Add(new WaterGoblin(30));
            DeckZwei.Add(new WaterGoblin(35));


            Battle Fight = new Battle("Geralt", "Triss", DeckEins, DeckZwei);
            var actualwinner = Fight.BattleHandler();

            Assert.AreEqual(Winner.SekondPlayer, actualwinner);
        }

    }
}