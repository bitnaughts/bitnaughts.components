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
    public override void Focus() {}

    public override float Action (float input) 
    {
        if (this == null) { Destroy(this); return -999; }
        
        LayerMask mask = LayerMask.GetMask("Default"); //Larger sensors can detect more layers of detail (see individual components, shells, etc.)
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * 2, Vector2.up * 999,999, mask, -999, 999);
        if (hit != null && hit.collider != null)
        {
            distance = hit.distance;
            Debug.DrawRay(transform.position, Vector2.up * distance, Color.green, .02f, false);
            print (hit.collider.gameObject.name);
            return distance;
        }
        else 
            distance = distance_max;
            // Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward), Color.green, .001f, false);
            return distance_max;
    }
    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }

    public override string ToString()
    {
        return "\n ◌ <b>" + name + "</b>\n ┣ " + new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y).ToString() + "\n ┣ " + GetComponent<SpriteRenderer>().size.ToString() + "\n ┗ " + gameObject.transform.localEulerAngles.z.ToString("0.0") + "°\n ┏ <b>Range</b>\n ┗ " + distance.ToString("0.0");
    }
}