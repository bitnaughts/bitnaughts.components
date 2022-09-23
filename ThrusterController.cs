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
        return transform.up * thrust / 100f;
    }
    public Vector2 GetPosition() 
    {
        return new Vector2(transform.localPosition.x, transform.localPosition.y);
    }

    public override string GetIcon() { return "◉"; }
    public override string ToString()
    {
        return $"{name}\nclass {name} : Component {{\n  double thr = {thrust.ToString("0.000")};\n  /*_Constructor_*/\n  class {name} () {{\n{base.ToString()}\n  }}\n  /*_Throttle_control_(max)_*/\n  void ThrottleMax () {{ Thrust(999); }}\n  /*_Throttle_control_(min)_*/\n  void ThrottleMin () {{ Thrust(0); }}\n  /*_Thrust_Modifier_*/\n  void Thrust (double throttle) {{ thr = throttle; }}\n}}\n\n☑_Ok\n☒_Cancel\n☒_Delete\n⍰⍰_Help";
    }
}