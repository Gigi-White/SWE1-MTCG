using Monster_Trading_Card_Game.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game.Cardclasses.MonsterCardFolder
{
    public class Knight : MonsterCard
    {
  
        public Knight(float attackPower) :base(Element.Normal, attackPower, CreatureType.Knight)
        {
            
        }
        //Gegen Wasserspells kann der Knight nicht angreifen. Er ertrinkt einfach.
        override public float Attack(Card card)
        {
            float realDamage = AttackPower;
            if(card.Type == CardType.Spell && card.Element == Element.Water)
            {
                realDamage = 0;
            }

            return realDamage;
        }
    }
}
