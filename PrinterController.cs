using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;
public class PrinterController : ComponentController {   
    public GameObject Head, Beam1, Beam2;
    public override void Focus() {
        Head.transform.localPosition = Vector2.zero;
        Beam1.transform.localPosition = Vector2.zero;
        Beam1.GetComponent<SpriteRenderer>().size = new Vector2(1, GetComponent<SpriteRenderer>().size.y);
        Beam2.transform.localPosition = Vector2.zero;
        Beam2.GetComponent<SpriteRenderer>().size = new Vector2(1, GetComponent<SpriteRenderer>().size.x);
        // Head.GetComponent<SpriteRenderer>().size = GetComponent<SpriteRenderer>().size;
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
    public override string GetIcon() { return "▦"; }
    public override string ToString()
    { //▤<b>≣ Data</b>
        return $"{name}\nclass {name}_(){base.ToString()}\n void Print_() {{ }}\n}}\n\n<b>Exit</b>\n\n<b>Delete</b>";
            // "⋅ Nozzle noz = new N?
            //  ┣ ↹ " + GetComponent<SpriteRenderer>().size.ToString() + "\n  ┗ ↺ " + gameObject.transform.localEulerAngles.z.ToString("0.0") + "°\n  ";
    }
}