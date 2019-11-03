using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using UnityEngine;
using Assets.Code.ProcessElements;

namespace VRWorlds.Browser
{
    public abstract class ProcessBase
    {
        public Guid ProcessId { get; private set; } = Guid.NewGuid();
        protected string LoadFile { get; set; } = null;
        protected Guid AccessToken { get; set; } = Guid.NewGuid();
        public bool Started { get; protected set; } = false;

        public static ProcessBase LoggerProcessor { get; set; } = null;

        private Thread _threadHandle = null;

        protected ProcessBase()
        {
            _threadHandle = new Thread(new ThreadStart(_processHandler));
         
        }

        protected abstract void AugmentArguments(StringBuilder sb);
        protected abstract void ProcessorLoop();

        public virtual void ProcessorStart()
        {
            if (Started) return;

            _threadHandle.Start();
            Started = true;
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
                    proc.StartInfo.Environment.Add("VRWORLDS_BROWSER_SESSION", ProcessHandler.BrowserSession.ToString());
                    proc.StartInfo.Arguments = startArgs;

                    proc.Start();

                    UnityEngine.Debug.Log("Process Started: "+LoadFile);

                    ProcessorStart();

                    ProcessorLoop();  // Must not let this exit, or we will shutdown

                    ProcessorShutdown();
                }
            }
            catch (Exception ex) {
                UnityEngine.Debug.Log("Error: " + ex.Message);
            }
        }
    }
}
