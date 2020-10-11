using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    public enum element
    {
        water,
        fire,
        normal
    }

    public enum cardType
    {
        monster,
        spell
    }

    public enum creatureType
    {
        Goblin,
        Dragon,
        Wizzard,
        Ork,
        Knight,
        Kraken,
        FireElve,
        none,
    }

    public enum winner
    {
        FirstPlayer,
        SekondPlayer,
        Draw,
    }
}
