﻿using Monster_Trading_Card_Game.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game.Cardclasses.MonsterCardFolder
{
    public class MonsterCard : Card
    {
        override public Element Element { get; }
        override public CardType Type { get; }
        override public CreatureType Creature { get; }
        override public float AttackPower { get; }
        override public float Damage { get; set; }



        public MonsterCard(Element element, float attackPower, CreatureType creatureType)
        {
            Type = CardType.Monster;
            Element = element;
            Creature = creatureType;
            AttackPower = attackPower;
            Damage = 0;
        }
        //Standardfunktion für Angriff. AttackPower wird ausgegeben
        override public float Attack(Card card)
        {
            return AttackPower;
        }
        //Standardfunktion  erlittener Schaden. Bei Kampf gegen einen Spell wird das Element miteinberechnet
        public override void SetDamage(Card card, float damage)
        {
            
            float realDamage = damage;

            if (card.Type == CardType.Spell)
            {
                if (this.Element == Element.Fire && card.Element == Element.Water || this.Element == Element.Water && card.Element == Element.Normal || this.Element == Element.Normal && card.Element == Element.Fire)
                {
                    realDamage = realDamage * 2;
                }
                else if (this.Element == Element.Fire && card.Element == Element.Normal || this.Element == Element.Water && card.Element == Element.Fire || this.Element == Element.Normal && card.Element == Element.Water)
                {
                    realDamage = realDamage / 2;
                }
            }

            Damage = realDamage;
        }


    }
}
