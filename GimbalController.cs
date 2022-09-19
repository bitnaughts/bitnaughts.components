using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class GimbalController : ComponentController 
{   
    public float speed;
    GameObject gimbal_grid;

    public const int INPUT_MIN = -3600, INPUT_MAX = 360;

    private void Awake () {
        gimbal_grid = transform.GetChild(0).gameObject;
    }
    void FixedUpdate () {
        // if (speed != 0) {
        //     gimbal_grid.transform.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(gimbal_grid.transform.localEulerAngles.z - speed % 360, INPUT_MIN, INPUT_MAX)); //gimbal_grid.transform.localEulerAngles.z + 
        // }
    }
    // public override void Design() {
    //     GetComponent<SpriteRenderer>().sprite = inverse;
    // }
    // public override void Launch() {
    //     GetComponent<SpriteRenderer>().sprite = sprite;
    // }
    public override void Focus() {
        // gimbal_grid.GetComponent<SpriteRenderer>().size = GetComponent<SpriteRenderer>().size;
    }
    public override void Ping() {
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
    public override string GetIcon() { return "▣"; }
    public override string ToString()
    { //▤<b>≣ Data</b>
        return $"{name}\nclass {name} : Component {{\n  /*_Constructor_*/\n  public class {name} () {{\n{ComponentToString()}\n  }}\n  /*_Rotates_units_(CW)_*/\n  public void RotateCW () {{\n    Rotate(-15);\n  }}\n  /*_Rotates_units_(CCW)_*/\n  public void RotateCCW () {{\n    Rotate(15);\n  }}\n  public void Rotate (double r) {{\n    rot += r;\n  }}\n}}\n\n☑_Ok\n☒_Cancel\n☒_Delete\n⍰⍰_Help";
            // "⋅ double angle = " + gimbal_grid.transform.localEulerAngles.z.ToString("0.0") + ";\n" +  
            // "⋅ double speed = " + speed.ToString("0.0") + ";\n" +  
            // "⋅ \n" +
            //  ┣ ↹ " + GetComponent<SpriteRenderer>().size.ToString() + "\n  ┗ ↺ " + gameObject.transform.localEulerAngles.z.ToString("0.0") + "°\n  ";
    }
        // return "\n   <b>" + name + "</b>\n  ┣ ↧ " + new Vector2(transform.localPosition.x, transform.localPosition.y).ToString() + "\n  ┗ ↹ " + GetComponent<SpriteRenderer>().size.ToString() + "\n  <b>↺ Rotation</b>\n  ┗ " +  + "°";//\n  ┗ ↺ " + transform.localEulerAngles.z.ToString("0.0") + "°
}