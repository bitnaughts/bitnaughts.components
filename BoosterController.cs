using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class BoosterController : ComponentController {   

    private float thrust = 0, max_thrust = 99;
    public const int INPUT_MIN = -10, INPUT_MAX = 10, THRUST_MIN = 0, THRUST_MAX = 999;

    public override void Focus() {
        max_thrust = Mathf.Clamp(GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y * 10f, THRUST_MIN, THRUST_MAX);
        
        var sh = GetComponent<ParticleSystem>().shape;
        sh.radius = (GetComponent<SpriteRenderer>().size.x * .5f);

        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = thrust * GetComponent<SpriteRenderer>().size.x * .1f;

        var main = GetComponent<ParticleSystem>().main;
        main.startSize = new ParticleSystem.MinMaxCurve(2, GetComponent<SpriteRenderer>().size.x / 2);
    }
    public override float Action (float input) 
    {
        if (this == null) { Destroy(this.gameObject); return -999; }

        thrust = Mathf.Clamp(thrust + Mathf.Clamp(input, INPUT_MIN, INPUT_MAX), THRUST_MIN, max_thrust);

        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = thrust * GetComponent<SpriteRenderer>().size.x * .1f;

        return thrust;
    }

    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }

    public Vector2 GetThrustVector() 
    {
        return transform.forward * thrust / 100f;
    }
    public Vector2 GetPosition() 
    {
        return new Vector2(transform.position.x, transform.position.z);
    }

    public override string ToString()
    {
        return "\n  ◎ <b>" + name + "</b>\n  ┣ ↧ " + new Vector2(transform.localPosition.x, transform.localPosition.y).ToString() + "\n  ┗ ↹ " + GetComponent<SpriteRenderer>().size.ToString() + "\n  <b>⇊ Throttle</b>\n  ┗ " + thrust.ToString("0.0");//\n  ┗ ↺ " + gameObject.transform.localEulerAngles.z.ToString("0.0") + "°
    }
}