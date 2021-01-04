using System;
using Library;

namespace Task_4._1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                throw new LimitExceededException();
            }
            catch (InsufficientFundsException)
            {
                Console.WriteLine("InsufficientFundsException");
            }
            catch (LimitExceededException)
            {
                Console.WriteLine("LimitExceededException");
            }
            catch (PaymentServiceException)
            {
                Console.WriteLine("PaymentServiceException");
            }
            catch (Exception)
            {
                Console.WriteLine("Exception");
            }
        }
    }
}
