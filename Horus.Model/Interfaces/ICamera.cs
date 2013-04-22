using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Horus.Model.Interfaces
{
	public interface ICamera : IHorusDriver
	{
		string Method1(int arg1);
        int Property1 { get; set; }
	}
}
