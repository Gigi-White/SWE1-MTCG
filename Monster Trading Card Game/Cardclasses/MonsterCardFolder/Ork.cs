using Monster_Trading_Card_Game.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game.Cardclasses.MonsterCardFolder
{
    public class Ork : MonsterCard
    {
     
        public Ork(float attackPower) :base(Element.Normal, attackPower, CreatureType.Ork)
        {
        }
        //Ork hat von sich aus keine besonderen Eigenschaften
    }
}
