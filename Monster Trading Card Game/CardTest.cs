
using Monster_Trading_Card_Game.Cardclasses.MonsterCardFolder;
using NUnit.Framework;

namespace Monster_Trading_Card_Game
{
    public class Tests
    {

        //------------------Check ob alle speziellen Kartenfähigkeiten funktionieren----------------------
       
        [Test]//FireElfe nimmt gegen Drachen keinen Schaden
        public void FireElfeFightAgainstDragon()
        {
            Card Legolas = new FireElfe();
            Card Fuchur = new Dragon();

            Legolas.SetDamage(Fuchur, Fuchur.Attack(Legolas));
            var actualDamage = Legolas.Damage; 

            Assert.AreEqual(0, actualDamage);
           

        }

        [Test]//Goblins können Drachen keinen Schaden zufügen
        public void GoblinFightAgainstDragon()
        {
            Card Smorph = new Goblin();
            Card Smaug = new Dragon();

            var actualAttack = Smorph.Attack(Smaug);

            Assert.AreEqual(0, actualAttack);
        }

        [Test]//Knight ertrinkt wenn er gegen einen Wasserzauber kämpfen muss

        public void KnightFightAgainstWaterSpell()
        {
            Card Lancelot = new Knight();
            Card AvadaKedavra = new WaterSpell();

            var actualAttack = Lancelot.Attack(AvadaKedavra);
            Assert.AreEqual(0, actualAttack);
        }

        [Test]//Dem Kraken können Spells nichts anhaben 

        public void KrakenFightAgainstWaterSpell()
        {
            Card SquidKid = new Kraken();
            
            Card FireBall = new FireSpell();

            SquidKid.SetDamage(FireBall, FireBall.Attack(SquidKid));
            var actualDamage = SquidKid.Damage;

            Assert.AreEqual(0, actualDamage);

        }

        [Test]//Dem Wizzard können Orks nichts anhaben weil er sie verzaubert.
        
        public void WizzardFightAgainstOrk()
        {
            Card Jaina = new Wizzard();
            Card Thrall = new Ork();

            Jaina.SetDamage(Thrall, Thrall.Attack(Jaina));

            var actualDamage = Jaina.Damage;
            Assert.AreEqual(0, actualDamage);
        }


        [Test]//Monsterkampf gegen Spell

        public void MonsterCardFightAgainstSpellCard()
        {
            Card Mordechai = new Ork();
            Card Aquaknarre = new WaterSpell();

            Mordechai.SetDamage(Aquaknarre, Aquaknarre.Attack(Mordechai));
            Aquaknarre.SetDamage(Mordechai, Mordechai.Attack(Aquaknarre));

            var SpellDamage = Mordechai.Damage;
            var Orkdamage = Aquaknarre.Damage;

            Assert.AreEqual(100, Orkdamage);
            Assert.AreEqual(15, SpellDamage);
        }
        
    }
}