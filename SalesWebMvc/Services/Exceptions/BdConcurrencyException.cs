using System;

namespace SalesWebMvc.Services.Exceptions
{
    public class BdConcurrencyException : ApplicationException
    {
        public BdConcurrencyException(string message) : base(message)
        {

        }
    }
}
