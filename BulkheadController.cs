using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class BulkheadController : ComponentController {   
    float mass, capacity;
    public const int MASS_MIN = 0, MASS_MAX = 999;

    public override void Focus() {
        // capacity = Mathf.Clamp(GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y * 10f, MASS_MIN, MASS_MAX);
        // mass = capacity;
        Action(0);
    }

    public override float Action (float input) 
    {
        if (mass == 0) mass = GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y * 10f;
        if (this == null) { Destroy(this.gameObject); return -999; }

        mass = Mathf.Clamp(mass - input, MASS_MIN, GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y * 10f);
        return mass;
    }

    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 6);
    }

    public override string ToString()
    {
        return "\n  ▩ <b>" + name + "</b>\n  ┣ ↧ " + new Vector2(transform.localPosition.x, transform.localPosition.y).ToString() + "\n  ┗ ↹ " + GetComponent<SpriteRenderer>().size.ToString() + "\n  <b>♨ Fuel</b>\n  ┗ " + mass.ToString("0.0");//"\n  ┗ ↺ " + gameObject.transform.localEulerAngles.z.ToString("0.0") + "°\n  <b>♨ Fuel</b>\n  ┗ " + mass.ToString("0.0");
    }
}