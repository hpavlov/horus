using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Horus.Model.Interfaces
{
    public interface IHorusContext
    {
        TSettings ReadDriverSettings<TSettings>(Type driver, string deviceId) where TSettings : new();
        void WriteDriverSettings(object settings, Type driver, string deviceId);
    }
}
