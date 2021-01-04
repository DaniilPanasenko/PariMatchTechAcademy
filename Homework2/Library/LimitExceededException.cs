using System;
namespace Library
{
    public class LimitExceededException : PaymentServiceException
    {
        public LimitExceededException() : base() { }

        public LimitExceededException(string message) : base(message) { }
    }
}
