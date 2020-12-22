using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class ThrusterController : ComponentController {   

    public float max_step = 25f;
    protected Vector3 thrust_vector;
    private float thrust = 1, thrust_min = 0, thrust_max = 0;


    public override float Action (float input) 
    {
        if (this == null) { Destroy(this.gameObject); return -999; }

        thrust = Mathf.Clamp(thrust + Mathf.Clamp(input, -max_step, max_step), 0, 100);

        // Updating local max and min for graphing purposes
        if (thrust < thrust_min) thrust_min = thrust;
        if (thrust > thrust_max) thrust_max = thrust;

        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = thrust / 5;

        return thrust;
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