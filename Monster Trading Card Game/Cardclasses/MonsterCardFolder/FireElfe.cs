using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game.Cardclasses.MonsterCardFolder
{
    class FireElfe : MonsterCard
    {
        public FireElfe(): base(element.fire, 40, creatureType.FireElve)
        {

        }

        public override void SetDamage(Card card ,int damage)
        {
            int realDamage = damage;

            if (card.Type == cardType.spell)
            {
                if (this.Element == element.fire && card.Element == element.water || this.Element == element.water && card.Element == element.normal || this.Element == element.normal && card.Element == element.fire)
                {
                    realDamage = realDamage * 2;
                }
                else if (this.Element == element.fire && card.Element == element.normal || this.Element == element.water && card.Element == element.fire || this.Element == element.normal && card.Element == element.water)
                {
                    realDamage = realDamage / 2;
                }
            }
            else if (card.Type == cardType.monster && card.Creature == creatureType.Dragon)
            {
                realDamage = 0;
            }


            Damage = realDamage;
        }
    
    }
}
