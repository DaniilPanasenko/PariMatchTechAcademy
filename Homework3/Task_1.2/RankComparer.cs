using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Task_1._2
{
    public class RankComparer : IComparer<Player>
    {
        public int Compare(Player player1, Player player2)
        {
            if (player1.Rank > player2.Rank)
            {
                return 1;
            }
            if (player1.Rank < player2.Rank)
            {
                return -1;
            }
            return 0;
        }
    }
}
