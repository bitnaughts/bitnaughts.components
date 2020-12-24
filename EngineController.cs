using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class EngineController : ComponentController {   

    protected Vector3 thrust_vector;
    private float thrust = 0;


    public override float Action (float input) 
    {
        if (this == null) { Destroy(this.gameObject); return -999; }

        thrust = Mathf.Clamp(thrust + input, 0, 100);

        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = thrust;

        return thrust;
    }

    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(6, 2);
    }

    public Vector2 GetThrustVector() 
    {
        return transform.up * thrust / 100f;
    }
    public Vector2 GetPosition() 
    {
        return new Vector2(transform.position.x, transform.position.y);
    }
    public override string ToString()
    {
        return this.name + "\n│ This component pushes the ship forward";
    }
}