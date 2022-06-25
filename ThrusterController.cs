using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class ThrusterController : ComponentController {   

    private float thrust = 0, max_thrust = 99;
    public const int INPUT_MIN = -10, INPUT_MAX = 10, THRUST_MIN = 0, THRUST_MAX = 999;

    public override void Focus() {
        max_thrust = Mathf.Clamp(GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y * 10f, THRUST_MIN, THRUST_MAX);
        
        var sh = GetComponent<ParticleSystem>().shape;
        sh.radius = (GetComponent<SpriteRenderer>().size.x * .5f) - 3;

        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = thrust * GetComponent<SpriteRenderer>().size.x * .1f;
    }    
    public override void Ping() {
    }
    public override float Action (float input) 
    {
        if (this == null) { Destroy(this.gameObject); return -999; }

        thrust = Mathf.Clamp(input, THRUST_MIN, max_thrust);

        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = thrust * GetComponent<SpriteRenderer>().size.x * .1f;

        return thrust;
    }

    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(6, 2);
    }

    public Vector2 GetThrustVector() 
    {
        return transform.forward * thrust / 100f;
    }
    public Vector3 GetPosition() 
    {
        return transform.position;
    }

    public override string GetIcon() { return "◉"; }
    public override string ToString()
    {
        return $"{GetIcon()} {name}\nfinal class {name}{ComponentToString()}\n  double thr = {thrust.ToString("0.000")};\n\n  <i>/*_Thrust_sets_the_throttle_*/</i>\n  public void Thrust (double throttle) {{\n    thr = throttle;\n  }}\n}}\n\n<a>Exit</a>";
    }
}