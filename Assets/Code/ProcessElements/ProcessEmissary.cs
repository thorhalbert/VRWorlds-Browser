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
using System.Threading;

namespace VRWorlds.Browser
{
    public class ProcessEmissary : ProcessBase
    {
        public Guid ProcessorRole { get; set; }
        public string GrpcIngressUri { get; private set; }

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

            //GrpcIngressUri = new Uri( "http://127.0.0.1:" + port.ToString());
        }



        public override void ProcessorStart()
        {

         
       
            base.ProcessorStart();  // Launch thread and subprocess
        }

        protected override void CreateAndStartService()
        {
            base.CreateAndStartService();

            var conPort = new ServerPort("127.0.0.1", ServerPort.PickUnused, ServerCredentials.Insecure);

            var server = new Server
            {
                Services = { VRWorlds.Schemas.Browser.Common.Ping.BindService(new PingImpl(ProcessId)) },
                Ports = { conPort, }
            };

            // I don't think we're running in our service thread here -- have to start farther down
            server.Start();

            var bound = -1;
            foreach (var p in server.Ports)
            {
                if (p.BoundPort > 0)
                    bound = p.BoundPort;
                //  Debug.Log("Port: " + p.Port.ToString() + ", bound: "+p.BoundPort.ToString());
            }

            var host = conPort.Host + ":" + bound.ToString();
            Debug.Log("Binding to: " + host);
            GrpcIngressUri = host;
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
            Thread.Sleep(10000);
        }
    }
}
