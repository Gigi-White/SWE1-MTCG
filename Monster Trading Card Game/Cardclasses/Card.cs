using Monster_Trading_Card_Game.Enums;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Text;

namespace Monster_Trading_Card_Game
{
    public abstract class Card
    {
        public abstract Element Element { get; }
        public abstract CardType Type { get;}
        public abstract CreatureType Creature { get; }

        public abstract float AttackPower { get; }

        public abstract float Damage { get; set; }

        public abstract float Attack(Card card);

        public abstract void SetDamage(Card card, float damage);

    }


    
}
