using System;
namespace Library
{
    public class InsufficientFundsException : PaymentServiceException
    {
        public InsufficientFundsException() : base() { }

        public InsufficientFundsException(string message) : base(message) { }
    }
}
