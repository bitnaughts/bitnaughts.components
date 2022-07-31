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
        sh.radius = (GetComponent<SpriteRenderer>().size.x * .35f);

        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = thrust * GetComponent<SpriteRenderer>().size.x * .5f;

        var main = GetComponent<ParticleSystem>().main;
        main.startSize = new ParticleSystem.MinMaxCurve(2, GetComponent<SpriteRenderer>().size.x / 2);
    }    
    public override void Ping() {
    }
    public override float Action (float input) 
    {
        if (this == null) { Destroy(this.gameObject); return -999; }

        thrust = Mathf.Clamp(thrust + Mathf.Clamp(input, INPUT_MIN, INPUT_MAX), THRUST_MIN, max_thrust);

        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = thrust * GetComponent<SpriteRenderer>().size.x * .1f;

        return thrust;
    }
    // public override void Ping ()
    // {

    // }

    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }

    public Vector2 GetThrustVector() 
    {
        // return new Vector2(0, -.1f);
        return transform.forward * thrust / 100f;
    }
    public Vector2 GetPosition() 
    {
        return new Vector2(transform.position.x, transform.position.z);
    }
    public override string GetIcon() { return "◎"; }
    public override string ToString()
    {
        return $"{GetIcon()} {name}\nfinal class {name} : Component {{\n  double thr = {thrust.ToString("0.000")};\n  <i>/*_Constructor_*/</i>\n  public class {name} () {{\n{ComponentToString()}\n    thr = 0;\n  }}\n  <i>/*_Thrust_sets_the_throttle_*/</i>\n  public void Thrust (double throttle) {{\n    thr = throttle;\n  }}\n  <i>/*_Fire_launches_torpedos_*/</i>\n  public void Fire () {{\n    Fire(100);\n  }}\n  public void Fire (double throttle) {{\n    Torpedo t = new Torpedo (throttle);\n  }}\n}}\n<b>Exit</b>";
    }
}