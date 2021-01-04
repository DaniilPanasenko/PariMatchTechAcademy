using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Task_1._2
{
    public class PlayerEqualityComparer : IEqualityComparer<Player>
    {
        public bool Equals(Player player1, Player player2)
        {
            return player1.Age == player2.Age &&
                   player1.FirstName == player2.FirstName &&
                   player1.LastName == player2.LastName &&
                   player1.Rank == player2.Rank;
        }

        public int GetHashCode(Player obj)
        {
            return obj.FirstName.GetHashCode() * 13 ^
                   obj.LastName.GetHashCode() * 17 ^
                   obj.Age.GetHashCode() * 19 ^
                   obj.LastName.GetHashCode() * 23;
        }
    }
}
