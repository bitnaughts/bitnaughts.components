using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class CannonController : ComponentController {   
    public GameObject Shell;
    public float[] reload_timer;
    public const float RELOAD_TIME = 1f;
    
    public override void Focus() {

    }
    public override void Ping() {
        int num_cannons = Mathf.FloorToInt(GetComponent<SpriteRenderer>().size.x - 1);
        if (reload_timer.Length != num_cannons) reload_timer = new float[num_cannons];
        for (int i = 0; i < reload_timer.Length; i++)
        {
            reload_timer[i] = Mathf.Clamp(reload_timer[i] - .01f, 0, 10);
        }
    }
    public override float Action (float input) 
    {
        if (this == null) { Destroy(this); return -999; }
        // if (input >= 1 && input <= reload_timer.Length)
        for (int i = 0; i < reload_timer.Length; i++) {
            if (reload_timer[i] <= 0)
            {
                Interactor.Sound("Cannon" + ((i % 3) + 1));
                reload_timer[i] = Mathf.FloorToInt(GetComponent<SpriteRenderer>().size.y - 1);
                GameObject shell = Instantiate(
                    Shell,
                    this.transform.position,
                    this.transform.rotation,      
                    this.transform
                ) as GameObject;
                shell.transform.Translate(new Vector2((i + .5f) - reload_timer.Length / 2f, GetComponent<SpriteRenderer>().size.y / 2f));
                shell.transform.SetParent(GameObject.Find("World").transform);
                // shell.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                return 1;
            }
        }
        return 0;
    }
  
    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }
    public string GetReloadString() {
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
        return $"{name}\nclass {name} : Component {{\n  double [] barrels = {GetReloadString()};\n  /*_Constructor_*/\n  public class {name} () {{\n{ComponentToString()}\n    barrels = new double [{reload_timer.Length}];\n  }}\n  /*_Use_weapon_control_*/\n  public void Fire () {{\n    int b = getBarrel();\n    if (b != -1) {{\n      Shell s = new Shell (b);\n      barrels[b] = 5;\n    }}\n  }}\n  /*_Get_loaded_barrel_*/\n  public int GetBarrel () {{\n    for (int b = 0; b < barrels.length; b++) {{\n     if (barrels[b] == 0) {{ \n       return b;\n     }}\n    }}\n    return -1;\n  }}\n}}\n\n☑_Ok\n☒_Cancel\n☒_Delete\n⍰⍰_Help";
        //
    }
}