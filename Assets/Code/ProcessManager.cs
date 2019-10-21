using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRWorlds.Browser;

public class ProcessManager : MonoBehaviour
{
    private static List<ProcessBase> _processList = new List<ProcessBase>();

    // Start is called before the first frame update
    void Start()
    {
        // We're a master contoller
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
