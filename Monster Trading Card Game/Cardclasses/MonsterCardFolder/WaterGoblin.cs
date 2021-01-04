using Monster_Trading_Card_Game.Cardclasses.MonsterCardFolder;
using Monster_Trading_Card_Game.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    public class WaterGoblin : MonsterCard
    {

        public WaterGoblin(float attackPower) : base(Element.Water, attackPower,CreatureType.WaterGoblin)
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
