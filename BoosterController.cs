using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class BoosterController : ComponentController {   
    public GameObject Torpedo;
    public AudioClip TorpedoFireSfx, BoosterFireSfx;
    private float thrust = 0;
    public const int THRUST_MIN = 0, THRUST_MAX = 999;
    public float[] reload_timer;
    public const float RELOAD_TIME = 1f;
    private int torpedo_count = 0;

    public override float GetCost() {
        return 102; //1 metal 2 carbon
    }

    public override void Design() {
        launched = false;
        GetComponent<SpriteRenderer>().sprite = inverse;
        thrust = 0;
    }
    public override void Focus() {
        if (structure == null) structure = this.GetComponentInParent<StructureController>();
        var sh = GetComponent<ParticleSystem>().shape;
        sh.radius = (GetComponent<SpriteRenderer>().size.x * .35f);

        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = Mathf.Clamp(thrust * GetComponent<SpriteRenderer>().size.x, 0, 100);
        var main = GetComponent<ParticleSystem>().main;
        main.startSize = new ParticleSystem.MinMaxCurve(GetComponent<SpriteRenderer>().size.x / 2, GetComponent<SpriteRenderer>().size.x);
    }    
    public override void Launch() {
        launched = true;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public override void Ping() {
        thrust = Mathf.Clamp(thrust * .96f, THRUST_MIN, THRUST_MAX);
        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = Mathf.Clamp(thrust * GetComponent<SpriteRenderer>().size.x, 0, 100);
        var sh = GetComponent<ParticleSystem>().shape;
        sh.position = new Vector2(0, -(GetComponent<SpriteRenderer>().size.y / 2));
        // GetComponent<AudioSource>().volume = Mathf.Clamp(thrust, 0, 100) / 150f * Interactor.GetVolume();

        int num_cannons = Mathf.FloorToInt(GetComponent<SpriteRenderer>().size.x);
        if (reload_timer.Length != num_cannons) reload_timer = new float[num_cannons];
        for (int i = 0; i < reload_timer.Length; i++)
        {
            reload_timer[i] = Mathf.Clamp(reload_timer[i] - .01f, 0, 10);
        }
    }
    public override float Action () {
        Fire ();
        return 0;
    }
    public override float Action (float input) 
    {
        if (this == null) { Destroy(this.gameObject); return -999; }
        // if (input == -1) {
        //     Fire();
        // }
        // else {
            thrust = Mathf.Clamp(thrust + input / 1, THRUST_MIN, Mathf.Clamp(GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y * 10f, THRUST_MIN, THRUST_MAX));
            if (thrust > 0) {
                if (!GetComponent<AudioSource>().isPlaying) {
                    GetComponent<AudioSource>().clip = BoosterFireSfx;
                    GetComponent<AudioSource>().volume = .05f;
                    GetComponent<AudioSource>().Play();
                }
            }
        // }
        Focus();
        return thrust;
    }
    public void Fire () {
        // foreach (var comp in structure.components)
        // {
        //     if (comp.Value.GetType().Name == "BulkheadController") { comp.Value.Action(-1000); break; }
        // }
        for (int i = 0; i < reload_timer.Length; i++) {
            var bulkhead = structure.GetBulkheadController();
            if (reload_timer[i] <= 0 && bulkhead != null)
            {
                bulkhead.Action(-1000);
                GetComponent<AudioSource>().clip = TorpedoFireSfx;
                GetComponent<AudioSource>().volume = .05f;//Interactor.GetVolume() / 8;
                GetComponent<AudioSource>().Play();
                // Interactor.Sound("Torpedo" + ((i % 2) + 1));
                reload_timer[i] = Mathf.FloorToInt(GetComponent<SpriteRenderer>().size.y - 1);
                GameObject torpedo = Instantiate(
                    Torpedo,
                    this.transform.position,
                    this.transform.rotation,      
                    this.transform
                ) as GameObject;
                torpedo.transform.Translate(new Vector3((i + .5f) - reload_timer.Length / 2f, (0.5f * GetComponent<SpriteRenderer>().size.y) - 0.5f));
                torpedo.name = "↥" + this.name + torpedo_count++;
                torpedo.transform.SetParent(GameObject.Find("World").transform);
                torpedo.transform.localPosition = new Vector3(torpedo.transform.localPosition.x, torpedo.transform.localPosition.y, 0);
                torpedo.GetComponent<ProjectileController>().speed = GetComponent<SpriteRenderer>().size.y + 1 + (GetComponentInParent<StructureController>().translation.magnitude * 55);//7f);
                torpedo.GetComponent<ProjectileController>().acceleration = 1f / GetComponent<SpriteRenderer>().size.y;
                torpedo.GetComponent<ProjectileController>().damage = Mathf.FloorToInt(GetComponent<SpriteRenderer>().size.y - 1) * 2;
                torpedo.GetComponent<ProjectileController>().bulkhead = bulkhead;
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
        return output;
    }
    public Vector2 GetPosition() 
    {
        return new Vector2(transform.localPosition.x, transform.localPosition.y);
    }
    public string GetReloadString() {
        string output = "";
        for (int i = 0; i < reload_timer.Length; i++) {
            if (i == reload_timer.Length - 1) output += $"{reload_timer[i].ToString("0.00").TrimEnd('0').TrimEnd('.')}";
            else output += $"{reload_timer[i].ToString("0.00").TrimEnd('0').TrimEnd('.')}, ";
        }
        return  $"{{ {output} }}";
    }
    public override string GetIcon() { return "◎"; }
    public override string ToString()
    {
        return $"{GetIcon()} {this.name}\n{base.ToString()}\n thrust = {thrust.ToString("0.00").TrimEnd('0').TrimEnd('.')};\n barrels = {GetReloadString()};\n /*_Boost_Control_*/\n void Boost (int delta) {{\n  thrust += delta;\n }}\n /*_Torpedo_Control_*/\n void Launch_() {{\n  new Torpedo (size.y);\n }}\n}}\n☑_Ok\n☒_Cancel\n☒_Delete\n⍰⍰_Help";
    }
}