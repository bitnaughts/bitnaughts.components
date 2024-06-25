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
    public GameObject Laser;
    private int laser_count = 0;    
    public override float GetCost() {
        return 110; //1 metal 1 silicon
    }
    public override void Design() {
        launched = false;
        GetComponent<SpriteRenderer>().sprite = inverse;
    }
    public override void Focus() {}
    public override void Ping() {
        // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 1000, Color.green);
    }
    public override void Launch() {
        launched = true;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public override float Action () 
    {
        return Action(0);
    }
    public override float Action (float input) 
    {
        if (this == null) { Destroy(this); return -999; }

        // laser addition

        if (input == -1 ){ 

            Fire();


            return -1;
        } else {
            // print ("Raycasting");
            
            // var random = new System.Random();
            // return (float)(random.NextDouble() * 1998f - 999f); 

            // Bit shift the index of the layer (8) to get a bit mask
            int asteroidMask = 1 << 3, enemyMask = 1 << 6; 

            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            // asteroidMask = ~asteroidMask;
            // enemyMask = ~enemyMask;
            Fire();
            RaycastHit hit;
            float asteroidDistance = 0f, enemyDistance = 0f;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity, asteroidMask))
            {
                // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
                // Debug.Log("Did Hit asteroidMask" + hit.collider.gameObject.name + hit.distance);
                asteroidDistance = hit.distance;
                
                // Fire();
                return hit.distance;
            }
            else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity, enemyMask))
            {
                // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
                // Debug.Log("Did Hit enemyMask" + hit.collider.gameObject.name + hit.distance);
                enemyDistance = hit.distance;
                if (hit.collider.gameObject.name.StartsWith("HeatRay")) {
                    Interactor.ProgressCampaign();
                }
                // Fire();
                return hit.distance;
            }
            // print (hit.distance);
            // if (asteroidDistance < enemyDistance) return asteroidDistance;
            // else return -enemyDistance;
        }// negative denotes "enemy" to sensor
        return 0;
        // LayerMask mask = LayerMask.GetMask("Default"); //Larger sensors can detect more layers of detail (see individual components, shells, etc.)
        // RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * 2, Vector2.up * 999,999, mask, -999, 999);
        // if (hit != null && hit.collider != null)
        // {
        //     distance = hit.distance;
        //     Debug.DrawRay(transform.position, Vector2.up * distance, Color.green, .02f, false);
        //     print (hit.collider.gameObject.name);
        //     return distance;
        // }
        // else 
        //     distance = distance_max;
        //     // Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward), Color.green, .001f, false);
        //     return distance_max;
    }
    public void Fire () {
        // for (int i = 0; i < reload_timer.Length; i++) {
            // if (reload_timer[i] <= 0)
            // {
                // Interactor.Sound("Laser");// + ((i % 2) + 1));
                // reload_timer[i] = Mathf.FloorToInt(GetComponent<SpriteRenderer>().size.y - 1);
                GameObject laser = Instantiate(
                    Laser,
                    this.transform.position,
                    this.transform.rotation,      
                    this.transform
                ) as GameObject;
                laser.transform.Translate(new Vector3(0, (-0.5f * GetComponent<SpriteRenderer>().size.y) + 2)); //new Vector3((i + .5f) - reload_timer.Length / 2f
                laser.name = "l" + this.name + laser_count++;
                laser.transform.SetParent(GameObject.Find("World").transform);
                // laser.GetComponent<ProjectileController>().speed = 1000;//GetComponent<SpriteRenderer>().size.y + (GetComponentInParent<StructureController>().translation.magnitude * 7f);
                // laser.GetComponent<ProjectileController>().acceleration = 1f / GetComponent<SpriteRenderer>().size.y;
                // laser.GetComponent<ProjectileController>().damage = Mathf.FloorToInt(GetComponent<SpriteRenderer>().size.y - 1) * 2;
                // laser.GetComponent<SpriteRenderer>().size = new Vector2 (.65f, GetComponent<SpriteRenderer>().size.y);
                // var sh = laser.GetComponent<ParticleSystem>().shape;
                // sh.position = new Vector2(0, -(GetComponent<SpriteRenderer>().size.y / 2));
                return;
            // }
        // }
        
    }
    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }
    public override string GetIcon() { return "◌"; }
    public override string ToString() //double dis = {distance.ToString("0.0")};
    {
        return $"{GetIcon()} {this.name}\n{base.ToString()}\n /*_Cast_Ray_*/\n double Scan_() {{\n  return new Ray ().GetLength ();\n }}\n}}\n\n☑_Ok\n☒_Cancel\n☒_Delete\n⍰⍰_Help";
            //  ┣ ↹ " + GetComponent<SpriteRenderer>().size.ToString() + "\n  ┗ ↺ " + gameObject.transform.localEulerAngles.z.ToString("0.0") + "°\n  ";
    }
//     {
//         return "\n   <b>" + name + "</b>\n  ┣ ↧ " + new Vector2(transform.localPosition.x, transform.localPosition.y).ToString() + "\n  ┗ ↹ " + GetComponent<SpriteRenderer>().size.ToString() + "\n  <b>⇢ Range</b>\n  ┗ " + //\n  ┗ ↺ " + gameObject.transform.localEulerAngles.z.ToString("0.0") + "°
//     }
}