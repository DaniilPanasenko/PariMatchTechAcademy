using System;
using System.Collections.Generic;

namespace Task2
{
    public class ThreadSafePrimesHashSet
    {
        private HashSet<int> internalHashSet;

        private readonly object lockHashSet = new object();

        public ThreadSafePrimesHashSet()
        {
            internalHashSet = new HashSet<int>();
        }

        public void TryAdd(int item)
        {
            lock (lockHashSet)
            {
                internalHashSet.Add(item);
            }
        }

        public HashSet<int> GetHashSet()
        {
            return internalHashSet;
        }
    }
}
