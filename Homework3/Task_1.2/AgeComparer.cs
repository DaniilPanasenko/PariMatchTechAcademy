using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Task_1._2
{
    public class AgeComparer : IComparer<Player>
    {
        public int Compare(Player player1, Player player2)
        {
            if (player1.Age > player2.Age)
            {
                return 1;
            }
            if (player1.Age < player2.Age)
            {
                return -1;
            }
            return 0;
        }
    }
}
