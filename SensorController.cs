using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class SensorController : ComponentController {   //RangeFinder == 1D, Scanner == 2D?
    // public float max_step = 25f;
    public bool organicSensor;
    private float distance = 0, distance_min = 0, distance_max = 999, scan_range = 0;

    public override float Action (float input) 
    {
        if (this == null) { Destroy(this); return -999; }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 999))
        {
            distance = hit.distance;
            if (organicSensor == false && hit.collider.gameObject.layer == 9) return distance;
            else if (organicSensor == true){
                return distance;
            }
            // Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward) * distance, debug_color, .2f, false);
        }
        else 
            distance = distance_max;
            // Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward), Color.green, .001f, false);
            return distance_max;
    }
    public override string GetDescription() 
    {
        return "\n <b>Sensors</b> measure \n the range to \n the closest \n object in the \n line of sight;\n\n Component() \n> return Raycast().length";
    }
    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }

    public override string ToString()
    {
        // string output = "";
        // float[] spectrum = new float[64];
        // var listener = GameObject.Find("Main Camera").GetComponent<AudioListener>();
        // AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        // for (int i = 0; i < 64; i++) {
        //     for (float j = 0; j < spectrum[i]; j += .01f)
        //     {
        //         output += "█";
        //     }
        //     output += "\n";
        // }
        return this.name + "\n│ This component measures distances infront of itself";
    }
}