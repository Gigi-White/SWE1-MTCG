using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game.Cardclasses.MonsterCardFolder
{
    class Kraken :MonsterCard
    {
        public Kraken() : base(element.water, 50, creatureType.Kraken)
        {

        }

        //Überschreibung des erlittenen Schadens. Der Kraken ist immun gegen Spell Angriffen
        public override void SetDamage(Card card, int damage)
        {
            int realDamage = damage;
            if (card.Type == cardType.spell)
            {
                realDamage = 0;
            }
            Damage = realDamage;
        }
    }
}
