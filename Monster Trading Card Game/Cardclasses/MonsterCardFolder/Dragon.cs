﻿using Monster_Trading_Card_Game.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game.Cardclasses.MonsterCardFolder
{
    public class Dragon : MonsterCard
    {

        public Dragon(float attackPower) : base(Element.Fire, attackPower, CreatureType.Dragon)
        {
 
        }
        
    }
}
