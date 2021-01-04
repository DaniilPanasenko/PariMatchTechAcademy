using System;
namespace Task_2._1.Entities
{
    public class Leftover
    {
        public string ProductId { get; }

        public string Location { get; }

        public int LeftoverCount { get; }

        public Leftover(string productId, string location, int leftoverCount)
        {
            ProductId = productId;
            Location = location;
            LeftoverCount = leftoverCount;
        }
    }
}
