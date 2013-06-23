/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

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
