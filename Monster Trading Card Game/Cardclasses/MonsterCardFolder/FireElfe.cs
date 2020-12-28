using Monster_Trading_Card_Game.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game.Cardclasses.MonsterCardFolder
{
    public class FireElfe : MonsterCard
    {
        public FireElfe(float attackPower) : base(Element.Fire, attackPower, CreatureType.FireElve)
        {

        }

        public override void SetDamage(Card card ,float damage)
        {
            float realDamage = damage;

            if (card.Type == CardType.Spell)
            {
                if (this.Element == Element.Fire && card.Element == Element.Water || this.Element == Element.Water && card.Element == Element.Normal || this.Element == Element.Normal && card.Element == Element.Fire)
                {
                    realDamage = realDamage * 2;
                }
                else if (this.Element == Element.Fire && card.Element == Element.Normal || this.Element == Element.Water && card.Element == Element.Fire || this.Element == Element.Normal && card.Element == Element.Water)
                {
                    realDamage = realDamage / 2;
                }
            }
            else if (card.Type == CardType.Monster && card.Creature == CreatureType.Dragon)
            {
                realDamage = 0;
            }


            Damage = realDamage;
        }
    
    }
}
