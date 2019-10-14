using Assets.Code.ProcessElements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace VRWorlds.Browser
{
    public class ProcessEmissary: ProcessBase
    {
        public Guid ProcessorRole { get; set; }

        protected override void AugmentArguments(StringBuilder sb)
        {
            sb.Append("--processor-role ");
            sb.Append(ProcessorRole.ToString());
            sb.Append(' ');
        }

        protected override void ProcessorLoop()
        {
           
        }
    }
}
