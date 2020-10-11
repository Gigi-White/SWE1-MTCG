using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Text;

namespace Monster_Trading_Card_Game
{
    public abstract class Card
    {
        public abstract element Element { get; }
        public abstract cardType Type { get;}
        public abstract creatureType Creature { get; }

        public abstract int AttackPower { get; }

        public abstract int Damage { get; set; }

        public abstract int Attack(Card card);

        public abstract void SetDamage(Card card, int damage);

    }


    
}
