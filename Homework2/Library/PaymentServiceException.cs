using System;
namespace Library
{
    public class PaymentServiceException : Exception
    {
        public PaymentServiceException() : base() { }

        public PaymentServiceException(string message) : base(message) { }
    }
}
