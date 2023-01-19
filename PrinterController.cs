using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;
public class PrinterController : ComponentController {   
    public GameObject Head, Beam1, Beam2;
    public override void Focus() {
            Beam1.transform.localPosition = new Vector2(Head.transform.localPosition.x, 0);
            Beam1.GetComponent<SpriteRenderer>().size = new Vector2(1, GetComponent<SpriteRenderer>().size.y);
            Beam2.transform.localPosition = new Vector2(0, Head.transform.localPosition.y);
            Beam2.GetComponent<SpriteRenderer>().size = new Vector2(1, GetComponent<SpriteRenderer>().size.x);
        // Head.GetComponent<SpriteRenderer>().size = GetComponent<SpriteRenderer>().size;
    }
    public bool GoTo(Vector2 pos) {
        Head.transform.Translate(
            new Vector2(
                Mathf.Clamp(pos.x - Head.transform.localPosition.x, -.1f, .1f),
                Mathf.Clamp(pos.y - Head.transform.localPosition.y, -.1f, .1f)
            )
        );
        if (Mathf.Abs(Head.transform.localPosition.x - pos.x) < .1f && Mathf.Abs(Head.transform.localPosition.y - pos.y) < .1f) {
            Head.transform.localPosition = pos;
            Focus();
            return true;
        }
        Focus();
        return false;
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