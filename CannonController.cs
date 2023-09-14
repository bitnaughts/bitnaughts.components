using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class CannonController : ComponentController {   
    public bool automatic;
    public GameObject Shell;
    public AudioClip ShellFireSfx;
    float[] reload_timer;
    public const float RELOAD_TIME = 1f;
    private int shell_count = 0;
    public override void Focus() {

    }
    public override void Ping() {
        int num_cannons = Mathf.FloorToInt(GetComponent<SpriteRenderer>().size.x - 1);
        if (reload_timer == null || reload_timer.Length != num_cannons) reload_timer = new float[num_cannons];
        for (int i = 0; i < reload_timer.Length; i++)
        {
            reload_timer[i] = Mathf.Clamp(reload_timer[i] - .01f, 0, 10);
        }
        if (automatic) Action(0);
    }
    public override float Action () 
    {
        return Action(0);
    }
    public override float Action (float input) 
    {
        if (this == null) { Destroy(this); return -999; }
        if (reload_timer == null) Ping();
        for (int i = 0; i < reload_timer.Length; i++) {
            if (reload_timer[i] <= 0)
            {
                if (automatic && i == 0 || !automatic) {
                    GetComponent<AudioSource>().clip = ShellFireSfx;
                    GetComponent<AudioSource>().volume = .1f;
                    GetComponent<AudioSource>().Play();
                }
                // Interactor.Sound("Cannon" + ((i % 3) + 1));
                reload_timer[i] = Mathf.FloorToInt(GetComponent<SpriteRenderer>().size.y) * this.transform.localScale.y / 2.5f;
                GameObject shell = Instantiate(
                    Shell,
                    this.transform.position,
                    this.transform.rotation,      
                    this.transform
                ) as GameObject;
                shell.transform.Translate(new Vector2(this.transform.localScale.x * ((i + .5f) - reload_timer.Length / 2f), this.transform.localScale.y * GetComponent<SpriteRenderer>().size.y / 2f));
                shell.name = "⤒" + this.name + shell_count++;
                shell.transform.SetParent(GameObject.Find("World").transform);
                shell.GetComponent<ProjectileController>().speed = GetComponent<SpriteRenderer>().size.y * 5f  + (GetComponentInParent<StructureController>().translation.magnitude * 55f);
                shell.GetComponent<ProjectileController>().damage = Mathf.FloorToInt((GetComponent<SpriteRenderer>().size.y + 1) / 2f);
                if (this.name == "HeatRay") shell.GetComponent<ProjectileController>().speed = 100;
                return 1;
            }
        }
        return 0;
    }
  
    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }
    public override void Launch() {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public string GetReloadString() {
        if (reload_timer == null) Ping();
        string output = "";
        for (int i = 0; i < reload_timer.Length; i++) {
            if (i == reload_timer.Length - 1) output += $"{reload_timer[i].ToString("0.00")}";
            else output += $"{reload_timer[i].ToString("0.00")}, ";
        }
        return  $"{{ {output} }}";
    }
    public override string GetIcon() { return "◍"; }
    public override string ToString()
    {
        return $"{GetIcon()} {this.name}\n{base.ToString()}\n barrels = {GetReloadString()};\n  /*_Weapon_control_*/\n  void Fire_() {{\n    int b = getBarrel();\n    if (b != -1) {{\n      Shell s = new Shell (b, size.y);\n      barrels[b] = size.y;\n    }}\n  }}\n  /*_Get_loaded_barrel_*/\n  int GetBarrel_() {{\n    for (int b = 0; b < barrels.length; b++) {{\n     if (barrels[b] == 0) {{ \n       return b;\n     }}\n    }}\n    return -1;\n  }}\n}}\n☑_Ok\n☒_Cancel\n☒_Delete\n⍰⍰_Help";
    }
}