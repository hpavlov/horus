using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using Kayak;
using Kayak.Framework;

namespace Horus.Server
{
    public partial class HorusServer : ServiceBase
    {
        private KayakServer server;

        public HorusServer()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
			server = new KayakServer();

            server.UseFramework();

            server.Start(new IPEndPoint(IPAddress.Any, 8777));
        }

        protected override void OnStop()
        {
            server.Stop();
        }

    }
}
