using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class BulkheadController : ComponentController {   
    
    public override float Action (float input) {
        return input;
    }
    
    public override string ToString()
    {
        string output = "╠╕ Bulkhead Component: " + this.name + "\n╟┘";
        return "";
    }
}