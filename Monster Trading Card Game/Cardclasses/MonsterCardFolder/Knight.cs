using Monster_Trading_Card_Game.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game.Cardclasses.MonsterCardFolder
{
    public class Knight : MonsterCard
    {
  
        public Knight():base(Element.Normal, 40, CreatureType.Knight)
        {
            
        }
        //Gegen Wasserspells kann der Knight nicht angreifen. Er ertrinkt einfach.
        override public int Attack(Card card)
        {
            int realDamage = AttackPower;
            if(card.Type == CardType.Spell && card.Element == Element.Water)
            {
                realDamage = 0;
            }

            return realDamage;
        }
    }
}
