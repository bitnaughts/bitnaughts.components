using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class GimbalController : ComponentController 
{   
    GameObject gimbal_grid;

    private void Awake () {
        gimbal_grid = transform.Find("GimbalGrid").gameObject;
    }
    
    public override float Action (float input) 
    {
        gimbal_grid.transform.localEulerAngles = new Vector3(
            0, 
            gimbal_grid.transform.localEulerAngles.y + Mathf.Clamp(input, -10, 10),
            0
        );
        return gimbal_grid.transform.localEulerAngles.y;
    }

    public override string ToString ()
    {
        return this.name + "\n│ This component rotates other connected components";
    }
}