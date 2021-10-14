using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;
public class GirderController : ComponentController {   
    public override void Focus() {
    }

    public override float Action (float input) 
    {
        return input;
    }

    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }

    public override string ToString()
    {
        return "\n  ▨ <b>" + name + "</b>\n  ┣ ↴ " + new Vector2(transform.localPosition.x, transform.localPosition.y).ToString() + "\n  ┗ ↹ " + GetComponent<SpriteRenderer>().size.ToString();// + "\n  ┗ ↺ " + gameObject.transform.localEulerAngles.z.ToString("0.0") + "°";
    }
}