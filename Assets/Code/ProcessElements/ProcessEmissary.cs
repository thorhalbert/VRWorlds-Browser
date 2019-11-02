using Assets.Code.ProcessElements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using VRWorlds.Schemas.Browser.Standards;
using Grpc.Core;
using Grpc.Core.Utils;
using VRWorlds.Schemas.Browser.Common;
using Assets.Code.Lib3DOMImplementation;

namespace VRWorlds.Browser
{
    public class ProcessEmissary : ProcessBase
    {
        public Guid ProcessorRole { get; set; }
        public Uri GrpcIngressUri { get; private set; }

        private static int _port = 10000;
        private static object _portLock = new object();

        private const string EmissaryExecutable = @"C:\Users\Thor\Source\Repos\Lib3DOM\BrowserEmissaryProcess\bin\Debug\BrowserEmissaryProcess.exe";

        public int port { get; private set; } = 0;

        public ProcessEmissary(): base()
        {
            base.LoadFile = EmissaryExecutable;

            lock (_portLock)
            {
                port = _port++;
            }

            GrpcIngressUri = new Uri( "http://localhost:" + port.ToString());
        }

        protected override void ProcessorStart()
        {
            base.ProcessorStart();

            var server = new Server { 
                Services = { VRWorlds.Schemas.Browser.Common.Ping.BindService(new PingImpl()) },
                Ports = { new ServerPort("loalhost", port, ServerCredentials.Insecure), }
            };
        }

        protected override void ProcessorShutdown()
        {
            base.ProcessorShutdown();
        }

        protected override void AugmentArguments(StringBuilder sb)
        {
            sb.Append("--processor-role ");
            sb.Append(ProcessorRole.ToString());
            sb.Append(' ');

            sb.Append("--grpc-ingress-uri ");
            sb.Append(GrpcIngressUri.ToString());
            sb.Append(' ');
        }

        protected override void ProcessorLoop()
        {
           
        }
    }
}
