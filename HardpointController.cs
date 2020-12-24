using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;
public class HardpointController : ComponentController {   
    
    public override float Action (float input) {
        return input;
    }
  
    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }

    public override string ToString()
    {
        string output = "╠╕ Hardpoint Component: " + this.name + "\n╟┘";
        return "";
    }
}