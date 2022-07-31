using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;
public class GirderController : ComponentController {   
    public override void Focus() {
    }
    public override void Ping() {
    }    
    public override float Action (float input) 
    {
        return input;
    }

    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }
    public override string GetIcon() { return "▨"; }
    public override string ToString()
    {
        return $"{GetIcon()} {name}\nfinal class{name} {{\n{ComponentToString()}\n}}\n<a>Exit</a>";
    }
}