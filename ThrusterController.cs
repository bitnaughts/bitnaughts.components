using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class ThrusterController : ComponentController {   

    private float thrust = 0;
    public const int INPUT_MIN = -100, INPUT_MAX = 100, THRUST_MIN = 0, THRUST_MAX = 999;

    public override void Focus() {
        var sh = GetComponent<ParticleSystem>().shape;
        sh.radius = (GetComponent<SpriteRenderer>().size.x * .5f) - 3;
        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = Mathf.Clamp(thrust * GetComponent<SpriteRenderer>().size.x, 0, 100);
        var main = GetComponent<ParticleSystem>().main;
        main.startLifetime = thrust / 25f;
        main.startSize = new ParticleSystem.MinMaxCurve((GetComponent<SpriteRenderer>().size.x - 2) / 2, GetComponent<SpriteRenderer>().size.x - 2);
    }    
    public override void Ping() {
    }
    public override float Action (float input) 
    {
        if (this == null) { Destroy(this.gameObject); return -999; }
        thrust = Mathf.Clamp(thrust + input, THRUST_MIN, Mathf.Clamp(GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y * 10f, THRUST_MIN, THRUST_MAX));
        Focus();
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
        return $"{name}\nclass {name} : Component {{\n  thrust = {thrust.ToString("0.000")};\n  /*_Constructor_*/\n  class {name}_() {{\n{base.ToString()}\n  }}\n  /*_Thrust_control_(+)_*/\n  void ThrustUp_() {{\n    thrust += 25;\n  }}\n  /*_Thrust_control_(-)_*/\n  void ThrustDown_() {{\n    thrust -= 25;\n  }}\n}}\n☑_Ok\n☒_Cancel\n☒_Delete\n⍰⍰_Help";
    }
}