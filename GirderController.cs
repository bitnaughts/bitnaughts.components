using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;
public class GirderController : ComponentController {   
    public override float GetCost() {
        return 001; //1 carbon
    }
    public override void Design() {
        launched = false;
        GetComponent<SpriteRenderer>().sprite = inverse;
    }
    public override void Focus() {
    }
    public override void Ping() {
    }    
    public override float Action () 
    {
        return 0;
    }
    public override float Action (float input) 
    {
        return input;
    }

    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }
    public override void Launch() {
        launched = true;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public override string GetIcon() { return "▨"; }
    public override string ToString()
    {
        return $"{GetIcon()} {this.name}\n{base.ToString()}\n}}\n<b>Exit</b>\n\n<b>Delete</b>";
    }
}