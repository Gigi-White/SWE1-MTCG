using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game.Cardclasses.MonsterCardFolder
{
    class Knight : MonsterCard
    {
  
        public Knight():base(element.normal, 40, creatureType.Knight)
        {
            
        }
        //Gegen Wasserspells kann der Knight nicht angreifen. Er ertrinkt einfach.
        override public int Attack(Card card)
        {
            int realDamage = AttackPower;
            if(card.Type == cardType.spell && card.Element == element.water)
            {
                realDamage = 0;
            }

            return realDamage;
        }
    }
}
