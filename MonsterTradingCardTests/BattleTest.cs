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

            DeckEins.Add(new Dragon());
            DeckEins.Add(new Dragon());
            DeckEins.Add(new Dragon());

            DeckZwei.Add(new Goblin());
            DeckZwei.Add(new Goblin());
            DeckZwei.Add(new Goblin());


            Battle Fight = new Battle(DeckEins, DeckZwei, "Yugi", "Kaiba");
            var actualwinner = Fight.BattleHandler();

            Assert.AreEqual(Winner.FirstPlayer, actualwinner);
        }

        [Test]
        public void BattleTestTwo() //Spieler Zwei muss gewinnen
        {
            List<Card> DeckEins = new List<Card>();
            List<Card> DeckZwei = new List<Card>();

            DeckZwei.Add(new Goblin());
            DeckZwei.Add(new Goblin());
            DeckZwei.Add(new Goblin());


            Battle Fight = new Battle(DeckEins, DeckZwei, "Geralt", "Triss");
            var actualwinner = Fight.BattleHandler();

            Assert.AreEqual(Winner.SekondPlayer, actualwinner);
        }

    }
}