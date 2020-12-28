using Monster_Trading_Card_Game.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    public class SpellCard : Card
    {

        override public Element Element { get; }
        override public CardType Type { get; }
        override public CreatureType Creature { get; }

        override public float AttackPower { get; }
        override public float Damage { get; set; }

        public SpellCard(Element element, float attackPower)
        {
            Element = element;
            Type = CardType.Spell;
            Creature = CreatureType.None;
            AttackPower = attackPower;
            Damage = 0;

        }
        // Hier wird einfach Schaden ausgeteilt.
        override public float Attack(Card card)
        {
            float realdamage = AttackPower;
            
            
            return realdamage;
        }

        // Hier wird der erlittene Schaden berechnet, je nachdem welches Element der Angreifer hat. 

        public override void SetDamage(Card card, float damage)
        {
            
            float realdamage = damage;

            if (this.Element == Element.Fire && card.Element == Element.Water || this.Element == Element.Water && card.Element == Element.Normal || this.Element == Element.Normal && card.Element == Element.Fire)
            {
                realdamage = realdamage * 2;
            }
            else if (this.Element == Element.Fire && card.Element == Element.Normal || this.Element == Element.Water && card.Element == Element.Fire || this.Element == Element.Normal && card.Element == Element.Water)
            {
                realdamage = realdamage / 2;
            }
            Damage = realdamage;
        }
    }
}
