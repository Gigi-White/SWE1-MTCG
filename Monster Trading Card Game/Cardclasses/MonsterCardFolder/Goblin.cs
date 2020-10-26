using Monster_Trading_Card_Game.Cardclasses.MonsterCardFolder;
using Monster_Trading_Card_Game.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    public class Goblin : MonsterCard
    {

        public Goblin() : base(Element.Normal, 30,CreatureType.Goblin)
        {

        }

        override public int Attack(Card card)
        {
            int realdamage = AttackPower;
            if (card.Creature == CreatureType.Dragon)
            {
                realdamage = 0;
            }
            return realdamage;
        }
       

    }
}
