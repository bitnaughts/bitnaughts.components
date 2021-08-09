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
    
    public override void Focus() {}
    public void Cooldown()
    {
        int num_cannons = Mathf.FloorToInt(GetComponent<SpriteRenderer>().size.x - 1);
        if (reload_timer.Length != num_cannons)
        {
            reload_timer = new float[num_cannons];
        }
        for (int i = 0; i < reload_timer.Length; i++)
        {
            reload_timer[i] -= .02f;
        }
    }

    public override float Action (float input) 
    {
        if (this == null) { Destroy(this); return -999; }
        if (input >= 1 && input <= reload_timer.Length)
        {
            int barrel = Mathf.FloorToInt(input - 1);
            if (reload_timer[barrel] < 0)
            {
                reload_timer[barrel] = Mathf.FloorToInt(GetComponent<SpriteRenderer>().size.y - 1);
                GameObject shell = Instantiate(
                    Shell,
                    this.transform.position,
                    this.transform.rotation,      
                    this.transform
                ) as GameObject;
                shell.transform.Translate(new Vector2((barrel + .5f) - reload_timer.Length / 2f, GetComponent<SpriteRenderer>().size.y / 2f));
                shell.transform.SetParent(GameObject.Find("World").transform);
                return 1;
            }
        }
        return 0;
    }
  
    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }

    public override string ToString()
    {
        string output = "\n ◍ <b>" + name + "</b>\n ┣ " + new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y).ToString() + "\n ┣ " + GetComponent<SpriteRenderer>().size.ToString() + "\n ┗ " + gameObject.transform.localEulerAngles.z.ToString("0.0") + "°";
        output += "\n ┏ <b>Barrels</b>";
        for (int i = 0; i < reload_timer.Length - 1; i++) { 
            if (reload_timer[i] <= 0) output += "\n ┣ READY";
            else output += "\n ┠ " + reload_timer[i].ToString("0.0");
        } 
        var last = reload_timer.Length - 1;
        if (reload_timer[last] <= 0) 
            output += "\n ┗ READY";
        // else if (last == edit_line)//active_line)
            // output += "\n ┖ " + instructions[last];
        // else if (last == active_line)//edit_line)
            // output += "\n ┕ " + instructions[last];
        else 
            output += "\n ┖ " + reload_timer[last].ToString("0.0");
        return output;

    }
}