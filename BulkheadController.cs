using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class BulkheadController : ComponentController {   
    float data, capacity;
    public const int MIN = 0, MAX = 999;

    public override void Focus() {
        // capacity = Mathf.Clamp(GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y * 10f, MIN, MAX);
        // mass = capacity;
        Action(0);
    }
    public override void Ping() {
    }
    public override float Action (float input) 
    {
        // if (mass == 0) mass = GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y * 10f;
        // if (this == null) { Destroy(this.gameObject); return -999; }

        // capacity = Mathf.Clamp(capacity, MIN, GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y * 10f);
        // // - input
        // return mass;
        return 10;
    }

    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 6);
    }
    public override string GetIcon() { return "▥"; }
    public override string ToString()
    { //▤<b>≣ Data</b>
        return $"{name}\nclass {name} : Component {{\n  /*_Constructor_*/\n  class {name}_() {{\n{base.ToString()}\n    heap = new Heap ({(int)(GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y)});\n  }}  \n}}\n☑_Ok\n☒_Cancel\n☒_Delete\n⍰⍰_Help";
            //  ┣ ↹ " + GetComponent<SpriteRenderer>().size.ToString() + "\n  ┗ ↺ " + gameObject.transform.localEulerAngles.z.ToString("0.0") + "°\n  ";
    }
    // {
        // return "\n   <b>" + name + "</b>\n  ┣ ↧ " + new Vector2(transform.localPosition.x, transform.localPosition.y).ToString() + "\n  ┗ ↹ " + GetComponent<SpriteRenderer>().size.ToString() + "\n  <b>♨ Fuel</b>\n  ┗ " + mass.ToString("0.0");//"\n  ┗ ↺ " + gameObject.transform.localEulerAngles.z.ToString("0.0") + "°\n  <b>♨ Fuel</b>\n  ┗ " + mass.ToString("0.0");
    // }
}