using Monster_Trading_Card_Game.Cardclasses.MonsterCardFolder;
using Monster_Trading_Card_Game.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    public class Goblin : MonsterCard
    {

        public Goblin(float attackPower) : base(Element.Normal, attackPower,CreatureType.Goblin)
        {

        }

        override public float Attack(Card card)
        {
            float realDamage = AttackPower;
            if (card.Creature == CreatureType.Dragon)
            {
                realDamage = 0;
            }
            return realDamage;
        }
       

    }
}
