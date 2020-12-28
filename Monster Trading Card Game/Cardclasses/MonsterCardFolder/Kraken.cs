using Monster_Trading_Card_Game.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game.Cardclasses.MonsterCardFolder
{
    public class Kraken :MonsterCard
    {
        public Kraken(float attackPower) : base(Element.Water, attackPower, CreatureType.Kraken)
        {

        }

        //Überschreibung des erlittenen Schadens. Der Kraken ist immun gegen Spell Angriffen
        public override void SetDamage(Card card, float damage)
        {
            float realDamage = damage;
            if (card.Type == CardType.Spell)
            {
                realDamage = 0;
            }
            Damage = realDamage;
        }
    }
}
