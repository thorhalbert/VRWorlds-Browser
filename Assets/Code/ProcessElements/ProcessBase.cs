using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using UnityEngine;

namespace VRWorlds.Browser
{
    public abstract class ProcessBase
    {
        public Guid ProcessId { get; private set; } = Guid.NewGuid();
        protected string LoadFile { get; set; } = null;
        protected Guid AccessToken { get; set; } = Guid.NewGuid();

        public static ProcessBase LoggerProcessor { get; set; } = null;

        private Thread _threadHandle = null;

        protected ProcessBase()
        {
            _threadHandle = new Thread(new ThreadStart(_processHandler));
            _threadHandle.Start();
        }

        protected abstract void AugmentArguments(StringBuilder sb);
        protected abstract void ProcessorLoop();

        protected virtual void ProcessorStart()
        {

        }

        protected virtual void ProcessorShutdown() { }

        private void _processHandler()
        {
            try
            {
                using (var proc = new Process())
                {
                    var sb = new StringBuilder();

                    sb.Append("--processor-guid ");
                    sb.Append(ProcessId.ToString());
                    sb.Append(' ');

                    AugmentArguments(sb);

                    if (LoggerProcessor != null)
                    {
                        sb.Append("--logger-processor ");
                        sb.Append(LoggerProcessor.ProcessId.ToString());
                        sb.Append(' ');
                    }

                    var startArgs = sb.ToString();

                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.FileName = LoadFile;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.Environment.Add("VRWORLDS_GRPC_ACCESS_TOKEN", AccessToken.ToString());
                    proc.StartInfo.Arguments = startArgs;

                    proc.Start();

                    ProcessorStart();

                    ProcessorLoop();  // Must not let this exit, or we will shutdown

                    ProcessorShutdown();
                }
            }
            catch (Exception ex) { }
        }
    }
}
