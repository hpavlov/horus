using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Horus.Model.Interfaces
{
    public interface IHorusContext
    {
        object ReadDriverSettings(Type driverSettings, Type driver, string deviceId);
        void WriteDriverSettings(object settings, Type driver, string deviceId);
    }
}
