using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class GimbalController : ComponentController 
{   
    GameObject gimbal_grid;

    public const int INPUT_MIN = -10, INPUT_MAX = 10;

    private void Awake () {
        gimbal_grid = transform.Find("GimbalGrid").gameObject;
    }
    public override void Focus() {
        gimbal_grid.GetComponent<SpriteRenderer>().size = GetComponent<SpriteRenderer>().size;
    }
    public override float Action (float input) 
    {
        gimbal_grid.transform.localEulerAngles = new Vector3(0, 0, gimbal_grid.transform.localEulerAngles.z + Mathf.Clamp(input, INPUT_MIN, INPUT_MAX));
        return gimbal_grid.transform.localEulerAngles.z;
    }

    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }

    public override string ToString()
    {
        return "\n ▣ <b>" + name + "</b>\n ┣ " + new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y).ToString() + "\n ┣ " + GetComponent<SpriteRenderer>().size.ToString() + "\n ┗ " + gameObject.transform.localEulerAngles.z.ToString("0.0") + "°\n ┏ <b>Gimbal</b>\n ┗ " + gimbal_grid.transform.localEulerAngles.z.ToString("0.0");
    }
}