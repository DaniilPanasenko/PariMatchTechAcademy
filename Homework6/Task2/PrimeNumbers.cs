using System;
using System.Collections.Generic;
using System.Threading;
using Task2.JsonObjects;

namespace Task2
{
    public class PrimeNumbers
    {
        private ThreadSafePrimesHashSet _result;

        public PrimeNumbers()
        {
            _result = new ThreadSafePrimesHashSet();
        }

        private void GetPrimesFromOneSettings(Settings settings)
        {
            int min = settings.PrimesFrom;
            int max = settings.PrimesTo;
            if (min < 2) min = 2;
            for (int i = min; i < max; i++)
            {
                bool isPrime = true;
                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    _result.TryAdd(i);
                }
            }
        }

        public HashSet<int> GetPrimes(List<Settings> settingsList)
        {
            CountdownEvent countdownEvent = new CountdownEvent(settingsList.Count);
            foreach (var settings in settingsList)
            {
                Thread thread = new Thread(() => {
                    GetPrimesFromOneSettings(settings);
                    countdownEvent.Signal();
                });
                thread.Start();
                
            }
            countdownEvent.Wait();
            return _result.GetHashSet();
        }
    }
}
