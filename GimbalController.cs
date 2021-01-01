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
        gimbal_grid.GetComponent<SpriteRenderer>().size = GetComponent<SpriteRenderer>().size;
    }
    
    public override string GetDescription() 
    {
        return "\n <b>Gimbals</b> rotate \n fully overlapping components;\n\n Component (Input) \n> Clamp (Input, "+ INPUT_MIN + ", " + INPUT_MAX + ")\n> Rotation += Input\n> Rotation %= 360\n> Return Rotation";
    }
    public override float Action (float input) 
    {
        gimbal_grid = transform.Find("GimbalGrid").gameObject;
        gimbal_grid.GetComponent<SpriteRenderer>().size = GetComponent<SpriteRenderer>().size;
        gimbal_grid.transform.localEulerAngles = new Vector3(
            0, 
            0,
            gimbal_grid.transform.localEulerAngles.z + Mathf.Clamp(input, INPUT_MIN, INPUT_MAX)
        );
        return gimbal_grid.transform.localEulerAngles.z;
    }

    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }

    public override string ToString ()
    {
        return "\n " + this.name + "\n\n Angle: " + gimbal_grid.transform.localEulerAngles.z + "\n\n" + GetDescription();
    }
}