using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    class SpellCard : Card
    {

        override public element Element { get; }
        override public cardType Type { get; }
        override public creatureType Creature { get; }

        override public int AttackPower { get; }
        override public int Damage { get; set; }

        public SpellCard(element element, int attackPower)
        {
            Element = element;
            Type = cardType.spell;
            Creature = creatureType.none;
            AttackPower = attackPower;
            Damage = 0;

        }
        // Hier wird einfach Schaden ausgeteilt.
        override public int Attack(Card card)
        {
            int realdamage = AttackPower;
            
            
            return realdamage;
        }

        // Hier wird der erlittene Schaden berechnet, je nachdem welches Element der Angreifer hat. 

        public override void SetDamage(Card card, int damage)
        {
            
            int realdamage = damage;

            if (this.Element == element.fire && card.Element == element.water || this.Element == element.water && card.Element == element.normal || this.Element == element.normal && card.Element == element.fire)
            {
                realdamage = realdamage * 2;
            }
            else if (this.Element == element.fire && card.Element == element.normal || this.Element == element.water && card.Element == element.fire || this.Element == element.normal && card.Element == element.water)
            {
                realdamage = realdamage / 2;
            }
            Damage = realdamage;
        }
    }
}
