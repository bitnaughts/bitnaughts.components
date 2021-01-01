using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class ThrusterController : ComponentController {   

    private float thrust = 0;
    public const int INPUT_MIN = -10, INPUT_MAX = 10, THRUST_MIN = 0, THRUST_MAX = 100;

    public override float Action (float input) 
    {
        if (this == null) { Destroy(this.gameObject); return -999; }

        thrust = Mathf.Clamp(thrust + Mathf.Clamp(input, INPUT_MIN, INPUT_MAX), THRUST_MIN, THRUST_MAX);

        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = thrust;

        return thrust;
    }

    public override string GetDescription() 
    {
        return "\n <b>Thrusters</b> burn \n rocket fuel \n for throttleable \n thrust;\n\n Component (Input) \n> Clamp (Input, "+ INPUT_MIN + ", " + INPUT_MAX + ")\n> Throttle += Input \n> Clamp (Throttle, "+ THRUST_MIN + ", " + THRUST_MAX + ")\n> Return Throttle";
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
        return "\n " + this.name + "\n\n Position: " + GetPosition().ToString() + "\n Thrust Vector: " + GetThrustVector().ToString() + "\n Throttle: " + thrust + "\n\n" + GetDescription();
    }
}