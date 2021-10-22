using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
    class CabeInvoiceException:Exception
    {
        public enum ExceptionType
        {
            INVALID_RIDE_TYPE,
            INVALID_DISTANCE,
            INVALID_TIME,
            NULL_RIDES,
            INVALID_USER_ID
        }
        ExceptionType type;
        public CabeInvoiceException(ExceptionType type, string message) : base(message)
        {
            this.type = type;
        }
    }
}
