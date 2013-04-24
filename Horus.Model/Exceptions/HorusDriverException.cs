using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Horus.Model.Exceptions
{
    public class HorusDriverException : Exception
    {
        public HorusDriverException()
        { }

        public HorusDriverException(string message)
            : base(message)
        { }

        public HorusDriverException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
