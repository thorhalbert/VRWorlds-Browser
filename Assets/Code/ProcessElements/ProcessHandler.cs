using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRWorlds.Browser;

namespace Assets.Code.ProcessElements
{
    // This is a singleton within out browser, we might as well make it static
    
    public static class ProcessHandler
    {
        private static List<ProcessBase> _processList = new List<ProcessBase>();

        public static void Add(ProcessBase proc)
        {
            _processList.Add(proc);
        }

        internal static void Startup()
        {  
        }
    }
}
