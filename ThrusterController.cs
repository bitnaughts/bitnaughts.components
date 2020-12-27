using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class ThrusterController : ComponentController {   

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

    public override string GetDescription() 
    {
        return "\n <b>Thrusters</b> burn \n rocket fuel \n for throttleable \n thrust;\n\n Component(input) \n> throttle = input\n> return throttle";
    }
    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }

    public Vector2 GetThrustVector() 
    {
        return -transform.up * thrust / 100f;
    }
    public Vector2 GetPosition() 
    {
        return new Vector2(transform.position.x, transform.position.y);
    }
    public override string ToString()
    {
        return "\n " + this.name + "\n " + thrust;
    }
}