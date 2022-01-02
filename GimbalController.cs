using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class GimbalController : ComponentController 
{   
    GameObject gimbal_grid;

    public const int INPUT_MIN = -3600, INPUT_MAX = 360;

    private void Awake () {
        gimbal_grid = transform.GetChild(0).gameObject;
    }
    // public override void Design() {
    //     GetComponent<SpriteRenderer>().sprite = inverse;
    // }
    // public override void Launch() {
    //     GetComponent<SpriteRenderer>().sprite = sprite;
    // }
    public override void Focus() {
        gimbal_grid.GetComponent<SpriteRenderer>().size = GetComponent<SpriteRenderer>().size;
    }
    public override float Action (float input) 
    {
        gimbal_grid.transform.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(gimbal_grid.transform.localEulerAngles.z + input % 360, INPUT_MIN, INPUT_MAX)); //gimbal_grid.transform.localEulerAngles.z + 
        // gimbal_grid.GetComponent<SpriteRenderer>().enabled = true;
        return gimbal_grid.transform.localEulerAngles.z;
    }

    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }

    public override string ToString()
    {
        return "\n  ▣ <b>" + name + "</b>\n  ┣ ↧ " + new Vector2(transform.localPosition.x, transform.localPosition.y).ToString() + "\n  ┗ ↹ " + GetComponent<SpriteRenderer>().size.ToString() + "\n  <b>↺ Rotation</b>\n  ┗ " + gimbal_grid.transform.localEulerAngles.z.ToString("0.0") + "°";//\n  ┗ ↺ " + transform.localEulerAngles.z.ToString("0.0") + "°
    }
}