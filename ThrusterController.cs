using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class ThrusterController : ComponentController {   
    public AudioClip ThrustFireSfx;
    private float thrust = 0;
    public const int INPUT_MIN = -100, INPUT_MAX = 100, THRUST_MIN = 0, THRUST_MAX = 999;
    public override float GetCost() {
        return 104; //1 metal 4 carbon
    }

    public override void Focus() {
        var sh = GetComponent<ParticleSystem>().shape;
        sh.radius = (GetComponent<SpriteRenderer>().size.x * .5f) - 3;
        var exhaust_emission = GetComponent<ParticleSystem>().emission;
        exhaust_emission.rate = Mathf.Clamp(thrust * GetComponent<SpriteRenderer>().size.x, 0, 100);
        var main = GetComponent<ParticleSystem>().main;
        main.startLifetime = thrust / 100f;
        main.startSize = new ParticleSystem.MinMaxCurve((GetComponent<SpriteRenderer>().size.x - 2) / 2, GetComponent<SpriteRenderer>().size.x - 2);
    }    
    public override void Ping() {
    }
    public override void Launch() {
        launched = true;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public override float Action () 
    {
        return 0;
    }
    public override float Action (float input) 
    {
        if (!GetComponent<AudioSource>().isPlaying) {
            GetComponent<AudioSource>().clip = ThrustFireSfx;
            GetComponent<AudioSource>().volume = .01f;
            GetComponent<AudioSource>().Play();
            Interactor.PlayMusic();
        }
        else {
            GetComponent<AudioSource>().volume = Mathf.Clamp(thrust, 0, 100) / 250f * Interactor.GetVolume();
        }
        if (this == null) { Destroy(this.gameObject); return -999; }
        thrust = Mathf.Clamp(thrust + (input/1), THRUST_MIN, Mathf.Clamp(GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y * 2.5f, THRUST_MIN, THRUST_MAX));
        Focus();
        // print (thrust);
        return thrust;
    }

    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(6, 2);
    }

    public Vector2 GetThrustVector() 
    {
        // Debug.DrawRay(transform.position, transform.TransformDirection(-transform.up), Color.red);
        // Debug.DrawRay(transform.position, transform.up, Color.green);
        Vector2 output = new Vector2(transform.up.x, transform.up.z) * thrust / 100f;
        return output;
    }
    public Vector2 GetPosition() 
    {
        return new Vector2(transform.localPosition.x, transform.localPosition.y);
    }

    public override string GetIcon() { return "◉"; }
    public override string ToString()
    {
        return $"{GetIcon()} {this.name}\n{base.ToString()}\n thrust = {thrust.ToString("0.00").TrimEnd('0').TrimEnd('.')};\n /*_Throttle_Control_*/\n void Throttle (double delta) {{\n  thrust += delta;\n }}\n}}\n☑_Ok\n☒_Cancel\n☒_Delete\n⍰⍰_Help";
    }
}