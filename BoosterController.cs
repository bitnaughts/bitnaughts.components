using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class BoosterController : ComponentController {   
    public GameObject Torpedo;
    private float thrust = 0, max_thrust = 999;
    public const int INPUT_MIN = -10, INPUT_MAX = 10, THRUST_MIN = 0, THRUST_MAX = 999;
    public float[] reload_timer;
    public const float RELOAD_TIME = 1f;
    private int torpedo_count = 0;

    public override void Focus() {
        max_thrust = Mathf.Clamp(GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y * 10f, THRUST_MIN, THRUST_MAX);
        
        var sh = GetComponent<ParticleSystem>().shape;
        sh.radius = (GetComponent<SpriteRenderer>().size.x * .35f);

        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = thrust * GetComponent<SpriteRenderer>().size.x * 5f;

        var main = GetComponent<ParticleSystem>().main;
        main.startSize = new ParticleSystem.MinMaxCurve(2, GetComponent<SpriteRenderer>().size.x / 2);
    }    
    public override void Ping() {
        thrust = Mathf.Clamp(thrust - 1, THRUST_MIN, THRUST_MAX);
        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = thrust * GetComponent<SpriteRenderer>().size.x * 5f;

        int num_cannons = Mathf.FloorToInt(GetComponent<SpriteRenderer>().size.x);
        if (reload_timer.Length != num_cannons) reload_timer = new float[num_cannons];
        for (int i = 0; i < reload_timer.Length; i++)
        {
            reload_timer[i] = Mathf.Clamp(reload_timer[i] - .01f, 0, 10);
        }
        // GetComponent<ConstantForce2D>().force = new Vector2(0, -thrust);
    }
    public override float Action (float input) 
    {
        if (this == null) { Destroy(this.gameObject); return -999; }
        if (input == -1) {
            Fire();
        }

        thrust = Mathf.Clamp(thrust + Mathf.Clamp(input, INPUT_MIN, INPUT_MAX), THRUST_MIN, max_thrust);

        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = thrust * GetComponent<SpriteRenderer>().size.x * 5f;
        

        return thrust;
    }
    public void Fire () {
        for (int i = 0; i < reload_timer.Length; i++) {
            if (reload_timer[i] <= 0)
            {
                Interactor.Sound("Torpedo" + ((i % 2) + 1));
                reload_timer[i] = Mathf.FloorToInt(GetComponent<SpriteRenderer>().size.y - 1);
                GameObject torpedo = Instantiate(
                    Torpedo,
                    this.transform.position,
                    this.transform.rotation,      
                    this.transform
                ) as GameObject;
                torpedo.transform.Translate(new Vector3((i + .5f) - reload_timer.Length / 2f, GetComponent<SpriteRenderer>().size.y / 2f));
                torpedo.name = "↥" + this.name + torpedo_count++;
                torpedo.transform.SetParent(GameObject.Find("World").transform);
                // torpedo.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                return;
            }
        }
        
    }
    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }

    public Vector2 GetThrustVector() 
    {
        // return new Vector2(0, -.1f);
        return transform.up * thrust / 10f;
    }
    public Vector2 GetPosition() 
    {
        return new Vector2(transform.localPosition.x, transform.localPosition.y);
    }
    public override string GetIcon() { return "◎"; }
    public override string ToString()
    {
        return $"{name}\nclass {name} : Component {{\n  double thr = {thrust.ToString("0.000")};\n  /*_Constructor_*/\n  public class {name} () {{\n{base.ToString()}\n    thr = 0;\n  }}\n  /*_Boost_control_*/\n  public void Boost () {{\n    thr = 100;\n  }}\n  /*_Launch_torpedo_control_*/\n  public void Launch () {{\n    Torpedo t = new Torpedo ();\n  }}\n}}\n\n☑_Ok\n☒_Cancel\n☒_Delete\n⍰⍰_Help";
    }
}