using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    public class NormalSpell : SpellCard
    {
        public NormalSpell(float attackPower) : base(Element.Normal, attackPower, Enums.CreatureType.NormalSpell)
        {

        }
    }
}
