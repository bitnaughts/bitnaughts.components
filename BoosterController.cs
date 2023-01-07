using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class BoosterController : ComponentController {   
    public GameObject Torpedo;
    private float thrust = 0;
    public const int THRUST_MIN = 0, THRUST_MAX = 999;
    public float[] reload_timer;
    public const float RELOAD_TIME = 1f;
    private int torpedo_count = 0;

    public override void Focus() {
        var sh = GetComponent<ParticleSystem>().shape;
        sh.radius = (GetComponent<SpriteRenderer>().size.x * .35f);

        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = Mathf.Clamp(thrust * GetComponent<SpriteRenderer>().size.x, 0, 100);
        var main = GetComponent<ParticleSystem>().main;
        main.startSize = new ParticleSystem.MinMaxCurve(GetComponent<SpriteRenderer>().size.x / 2, GetComponent<SpriteRenderer>().size.x);
    }    
    public override void Ping() {
        thrust = Mathf.Clamp(thrust - 1f, THRUST_MIN, THRUST_MAX);
        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = Mathf.Clamp(thrust * GetComponent<SpriteRenderer>().size.x, 0, 100);
        var sh = GetComponent<ParticleSystem>().shape;
        sh.position = new Vector2(0, -(GetComponent<SpriteRenderer>().size.y / 2));

        int num_cannons = Mathf.FloorToInt(GetComponent<SpriteRenderer>().size.x);
        if (reload_timer.Length != num_cannons) reload_timer = new float[num_cannons];
        for (int i = 0; i < reload_timer.Length; i++)
        {
            reload_timer[i] = Mathf.Clamp(reload_timer[i] - .01f, 0, 10);
        }
    }
    public override float Action (float input) 
    {
        if (this == null) { Destroy(this.gameObject); return -999; }
        if (input == -1) {
            Fire();
        }
        thrust = Mathf.Clamp(thrust + input, THRUST_MIN, Mathf.Clamp(GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y * 10f, THRUST_MIN, THRUST_MAX));
        Focus();
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
                torpedo.transform.Translate(new Vector3((i + .5f) - reload_timer.Length / 2f, (-0.5f * GetComponent<SpriteRenderer>().size.y) + 2));
                torpedo.name = "↥" + this.name + torpedo_count++;
                torpedo.transform.SetParent(GameObject.Find("World").transform);
                torpedo.GetComponent<ProjectileController>().speed = GetComponent<SpriteRenderer>().size.y + (GetComponentInParent<StructureController>().translation.magnitude * 14);//7f);
                torpedo.GetComponent<ProjectileController>().acceleration = 1f / GetComponent<SpriteRenderer>().size.y;
                torpedo.GetComponent<ProjectileController>().damage = Mathf.FloorToInt(GetComponent<SpriteRenderer>().size.y - 1) * 2;
                torpedo.GetComponent<SpriteRenderer>().size = new Vector2 (.65f, GetComponent<SpriteRenderer>().size.y);
                var sh = torpedo.GetComponent<ParticleSystem>().shape;
                sh.position = new Vector2(0, -(GetComponent<SpriteRenderer>().size.y / 2));
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
        // Debug.DrawRay(transform.position, transform.TransformDirection(-transform.forward), Color.red);
        // Debug.DrawRay(transform.position, transform.TransformDirection(transform.up), Color.green);
        Vector2 output = new Vector2(transform.up.x, transform.up.z) * thrust / 100f;
        print (output);
        return output;
    }
    public Vector2 GetPosition() 
    {
        return new Vector2(transform.localPosition.x, transform.localPosition.y);
    }
    public string GetReloadString() {
        string output = "";
        for (int i = 0; i < reload_timer.Length; i++) {
            if (i == reload_timer.Length - 1) output += $"{reload_timer[i].ToString("0.00")}";
            else output += $"{reload_timer[i].ToString("0.00")}, ";
        }
        return  $"{{ {output} }}";
    }
    public override string GetIcon() { return "◎"; }
    public override string ToString()
    {
        return $"{name}\nclass {name} : Component {{\n  thrust = {thrust.ToString("0.000")};\n  barrels = {GetReloadString()};\n  /*_Constructor_*/\n  class {name}_() {{\n{base.ToString()}\n  }}\n  /*_Throttle_control_*/\n  void Boost_() {{\n    thrust += 25;\n  }}\n  /*_Torpedo_control_*/\n  void Launch_() {{\n    new Torpedo (size.y);\n  }}\n}}\n☑_Ok\n☒_Cancel\n☒_Delete\n⍰⍰_Help";
    }
}