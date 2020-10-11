using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game.Cardclasses.MonsterCardFolder
{
    class MonsterCard : Card
    {
        override public element Element { get; }
        override public cardType Type { get; }
        override public creatureType Creature { get; }
        override public int AttackPower { get; }
        override public int Damage { get; set; }

        public MonsterCard(element element, int attackPower, creatureType creatureType)
        {
            Type = cardType.monster;
            Element = element;
            AttackPower = attackPower;
            Creature = creatureType;
            AttackPower = attackPower;
            Damage = 0;
        }
        //Standardfunktion für Angriff. AttackPower wird ausgegeben
        override public int Attack(Card card)
        {
            return AttackPower;
        }
        //Standardfunktion  erlittener Schaden. Bei Kampf gegen einen Spell wird das Element miteinberechnet
        public override void SetDamage(Card card, int damage)
        {
            int realDamage = damage;

            if (card.Type == cardType.spell)
            {
                if (this.Element == element.fire && card.Element == element.water || this.Element == element.water && card.Element == element.normal || this.Element == element.normal && card.Element == element.fire)
                {
                    realDamage = realDamage * 2;
                }
                else if (this.Element == element.fire && card.Element == element.normal || this.Element == element.water && card.Element == element.fire || this.Element == element.normal && card.Element == element.water)
                {
                    realDamage = realDamage / 2;
                }
            }

            Damage = realDamage;
        }


    }
}
