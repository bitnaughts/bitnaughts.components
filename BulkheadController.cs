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
        capacity = Mathf.Clamp(GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y * 10f, MASS_MIN, MASS_MAX);
    }

    public override float Action (float input) 
    {
        if (this == null) { Destroy(this.gameObject); return -999; }

        mass = Mathf.Clamp(mass + input, MASS_MIN, capacity);
        return mass;
    }

    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 6);
    }

    // public override string ToString()
    // {
    //     return "\n> Mass: " + mass + "\n\n> Capacity: " + capacity;
    // }
}