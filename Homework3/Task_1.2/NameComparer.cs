using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Task_1._2
{
    public class NameComparer : IComparer<Player>
    { 
        public int Compare(Player player1, Player player2)
        {
            string name1 = player1.FirstName + " " + player1.LastName;
            string name2 = player2.FirstName + " " + player2.LastName;
            for (int i = 0; i < name1.Length && i < name2.Length; i++)
            {
                if (name1[i] > name2[i])
                {
                    return 1;
                }
                if (name1[i] < name2[i])
                {
                    return -1;
                }
            }
            if (name1.Length > name2.Length)
            {
                return 1;
            }
            if (name1.Length < name2.Length)
            {
                return -1;
            }
            return 0;
        }
    }
}
