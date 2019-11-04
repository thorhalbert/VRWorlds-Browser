using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRWorlds.Browser;
using Assets.Code.ProcessElements;

public class ProcessManager : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {
        // We're a master contoller
        DontDestroyOnLoad(this);

        ProcessHandler.Add(new ProcessEmissary() { ProcessorRole = ProcessorRoles.DedicatedAvatar });
        ProcessHandler.Add(new ProcessEmissary() { ProcessorRole = ProcessorRoles.DedicatedWorld });
        ProcessHandler.Add(new ProcessEmissary() { ProcessorRole = ProcessorRoles.DedicatedEntity });
        ProcessHandler.Add(new ProcessEmissary() { ProcessorRole = ProcessorRoles.MeldedEntity });

        ProcessHandler.Startup();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
