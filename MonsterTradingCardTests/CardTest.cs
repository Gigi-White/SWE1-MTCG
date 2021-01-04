
using Monster_Trading_Card_Game.Cardclasses.MonsterCardFolder;
using NUnit.Framework;
using System.Collections.Generic;

namespace Monster_Trading_Card_Game
{
    [TestFixture]
    public class CardTest
    {

        //------------------Check ob alle speziellen Kartenfähigkeiten funktionieren----------------------
       
        [Test]//FireElfe nimmt gegen Drachen keinen Schaden
        public void FireElfeFightAgainstDragon()
        {
            Card Legolas = new FireElfe(20);
            Card Fuchur = new Dragon(50);

            Legolas.SetDamage(Fuchur, Fuchur.Attack(Legolas));
            var actualDamage = Legolas.Damage; 

            Assert.AreEqual(0, actualDamage);
           

        }

        [Test]//Goblins können Drachen keinen Schaden zufügen
        public void GoblinFightAgainstDragon()
        {
            Card Smorph = new WaterGoblin(10);
            Card Smaug = new Dragon(35);

            var actualAttack = Smorph.Attack(Smaug);

            Assert.AreEqual(0, actualAttack);
        }

        [Test]//Knight ertrinkt wenn er gegen einen Wasserzauber kämpfen muss

        public void KnightFightAgainstWaterSpell()
        {
            Card Lancelot = new Knight(20);
            Card AvadaKedavra = new WaterSpell(15);

            var actualAttack = Lancelot.Attack(AvadaKedavra);
            Assert.AreEqual(0, actualAttack);
        }

        [Test]//Dem Kraken können Spells nichts anhaben 

        public void KrakenFightAgainstWaterSpell()
        {
            Card SquidKid = new Kraken(40);
            
            Card FireBall = new FireSpell(30);

            SquidKid.SetDamage(FireBall, FireBall.Attack(SquidKid));
            var actualDamage = SquidKid.Damage;

            Assert.AreEqual(0, actualDamage);

        }

        [Test]//Dem Wizzard können Orks nichts anhaben weil er sie verzaubert.
        
        public void WizzardFightAgainstOrk()
        {
            Card Jaina = new Wizzard(25);
            Card Thrall = new Ork(35);

            Jaina.SetDamage(Thrall, Thrall.Attack(Jaina));

            var actualDamage = Jaina.Damage;
            Assert.AreEqual(0, actualDamage);
        }


        [Test]//Monsterkampf gegen Spell

        public void MonsterCardFightAgainstSpellCard()
        {
            Card Mordechai = new Ork(30);
            Card Aquaknarre = new WaterSpell(20);

            Mordechai.SetDamage(Aquaknarre, Aquaknarre.Attack(Mordechai));
            Aquaknarre.SetDamage(Mordechai, Mordechai.Attack(Aquaknarre));

            var SpellDamage = Mordechai.Damage;
            var Orkdamage = Aquaknarre.Damage;
    

            Assert.AreEqual(60, Orkdamage);
            Assert.AreEqual(10, SpellDamage);
        }


       

    }
}