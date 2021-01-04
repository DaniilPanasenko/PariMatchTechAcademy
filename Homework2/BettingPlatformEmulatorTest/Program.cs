using System;
using Library;

namespace BettingPlatformEmulatorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BettingPlatformEmulator bettingPlatformEmulator = new BettingPlatformEmulator();
            bettingPlatformEmulator.Start();
        }
    }
}
