using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Horus.Model.Exceptions
{
    public class HorusDriverNotConnectedException : Exception
    {
        public HorusDriverNotConnectedException()
        { }

        public HorusDriverNotConnectedException(string message)
            : base(message)
        { }

        public HorusDriverNotConnectedException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
