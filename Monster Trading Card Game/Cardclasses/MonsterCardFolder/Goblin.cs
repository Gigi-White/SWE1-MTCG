using Monster_Trading_Card_Game.Cardclasses.MonsterCardFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    class Goblin : MonsterCard
    {

        public Goblin() : base(element.normal, 30,creatureType.Goblin)
        {

        }

        override public int Attack(Card card)
        {
            int realdamage = AttackPower;
            if (card.Creature == creatureType.Dragon)
            {
                realdamage = 0;
            }
            return realdamage;
        }
       

    }
}
