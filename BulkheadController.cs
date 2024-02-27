using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class BulkheadController : ComponentController {   
    public GameObject prefab;
    public Sprite CarbonSprite, SiliconSprite, IronSprite, TorpedoSprite;
    public float data, capacity;
    public const int MIN = 0, MAX = 999;

    public List<TerrainType> heap;

    public override float GetCost() {
        return 100; //1 metal
    }
    public override void Focus() {
        if (heap == null) heap = new List<TerrainType>();
        capacity = Mathf.Clamp(GetComponent<SpriteRenderer>().size.x * (GetComponent<SpriteRenderer>().size.y - 1), MIN, MAX);
        // mass = capacity;
        Action(0);
    }
    public override void Ping() {
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
        int count = 0;
        int metal = 0, silicon = 0, carbon = 0, torpedo = 0;
        float x = 0, y = 0;
        if (input == 0) {
            count = 0;
            foreach (var h in heap)
            {
                x = count % (int)GetComponent<SpriteRenderer>().size.x + .5f;
                y = Mathf.Floor(count / GetComponent<SpriteRenderer>().size.x) + 1;
                count++;
                switch (h)
                {
                    case TerrainType.Carbonaceous:
                        InstantiateTile(new Point(x - GetComponent<SpriteRenderer>().size.x/2, y - GetComponent<SpriteRenderer>().size.y/2), CarbonSprite);
                        break;
                    case TerrainType.Siliceous:
                        InstantiateTile(new Point(x - GetComponent<SpriteRenderer>().size.x/2, y - GetComponent<SpriteRenderer>().size.y/2), SiliconSprite);
                        break;
                    case TerrainType.Metallic:
                        InstantiateTile(new Point(x - GetComponent<SpriteRenderer>().size.x/2, y - GetComponent<SpriteRenderer>().size.y/2), IronSprite);
                        break;
                    case TerrainType.Torpedo:
                        InstantiateTile(new Point(x - GetComponent<SpriteRenderer>().size.x/2, y - GetComponent<SpriteRenderer>().size.y/2), TorpedoSprite);
                        break;
                }
            }
            return 0;
        }
        if (input > 0) {
            string input_string = input.ToString().PadLeft(4, '0');
            torpedo = int.Parse(input_string[0].ToString());
            carbon = int.Parse(input_string[3].ToString());
            silicon = int.Parse(input_string[2].ToString());
            metal = int.Parse(input_string[1].ToString());
            // print (carbon + " " + silicon + " " + metal + " " + torpedo);

            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            // x = heap.Count() % (int)GetComponent<SpriteRenderer>().size.x;
            // y = Mathf.Floor(heap.Count() / GetComponent<SpriteRenderer>().size.x);
            for (int i = 0; i < heap.Count; i++)
            {
                switch (heap[i])
                {
                    case TerrainType.Carbonaceous:
                        if (carbon > 0) {
                            carbon--;
                            heap.RemoveAt(i);
                            i--;
                            break;
                        }
                        break;
                    case TerrainType.Siliceous:
                        if (silicon > 0) {
                            silicon--;
                            heap.RemoveAt(i);
                            i--;
                            break;
                        }
                        break;
                    case TerrainType.Metallic:
                        if (metal > 0) {
                            metal--;
                            heap.RemoveAt(i);
                            i--;
                            break;
                        }
                        break;
                    case TerrainType.Torpedo:
                        if (torpedo > 0) {
                            torpedo--;
                            heap.RemoveAt(i);
                            i--;
                            break;
                        }
                        break;
                }
            }
            count = 0;
            foreach (var h in heap)
            {
                x = count % (int)GetComponent<SpriteRenderer>().size.x + .5f;
                y = Mathf.Floor(count / GetComponent<SpriteRenderer>().size.x) + 1;
                count++;
                switch (h)
                {
                    case TerrainType.Carbonaceous:
                        InstantiateTile(new Point(x - GetComponent<SpriteRenderer>().size.x/2, y - GetComponent<SpriteRenderer>().size.y/2), CarbonSprite);
                        break;
                    case TerrainType.Siliceous:
                        InstantiateTile(new Point(x - GetComponent<SpriteRenderer>().size.x/2, y - GetComponent<SpriteRenderer>().size.y/2), SiliconSprite);
                        break;
                    case TerrainType.Metallic:
                        InstantiateTile(new Point(x - GetComponent<SpriteRenderer>().size.x/2, y - GetComponent<SpriteRenderer>().size.y/2), IronSprite);
                        break;
                    case TerrainType.Torpedo:
                        InstantiateTile(new Point(x - GetComponent<SpriteRenderer>().size.x/2, y - GetComponent<SpriteRenderer>().size.y/2), TorpedoSprite);
                        break;
                }
            }
            
        }
        
        if (input < 0) {
            string input_string = input.ToString().Substring(1).PadLeft(4, '0');
            torpedo = int.Parse(input_string[0].ToString());
            carbon = int.Parse(input_string[3].ToString());
            silicon = int.Parse(input_string[2].ToString());
            metal = int.Parse(input_string[1].ToString());
            // print (carbon + " " + silicon + " " + metal + " " + torpedo);

            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            // x = heap.Count() % (int)GetComponent<SpriteRenderer>().size.x;
            // y = Mathf.Floor(heap.Count() / GetComponent<SpriteRenderer>().size.x);
            for (int i = 0; i < torpedo; i++) {
                heap.Add(TerrainType.Torpedo);
            }
            for (int i = 0; i < metal; i++) {
                heap.Add(TerrainType.Metallic);
            }
            for (int i = 0; i < silicon; i++) {
                heap.Add(TerrainType.Siliceous);
            }
            for (int i = 0; i < carbon; i++) {
                heap.Add(TerrainType.Carbonaceous);
            }
            count = 0;
            foreach (var h in heap)
            {
                x = count % (int)GetComponent<SpriteRenderer>().size.x + .5f;
                y = Mathf.Floor(count / GetComponent<SpriteRenderer>().size.x) + 1;
                count++;
                switch (h)
                {
                    case TerrainType.Carbonaceous:
                        InstantiateTile(new Point(x - GetComponent<SpriteRenderer>().size.x/2, y - GetComponent<SpriteRenderer>().size.y/2), CarbonSprite);
                        break;
                    case TerrainType.Siliceous:
                        InstantiateTile(new Point(x - GetComponent<SpriteRenderer>().size.x/2, y - GetComponent<SpriteRenderer>().size.y/2), SiliconSprite);
                        break;
                    case TerrainType.Metallic:
                        InstantiateTile(new Point(x - GetComponent<SpriteRenderer>().size.x/2, y - GetComponent<SpriteRenderer>().size.y/2), IronSprite);
                        break;
                    case TerrainType.Torpedo:
                        InstantiateTile(new Point(x - GetComponent<SpriteRenderer>().size.x/2, y - GetComponent<SpriteRenderer>().size.y/2), TorpedoSprite);
                        break;
                }
            }
            
        }
// 14 Si 3s²3p²
// Silicon
// 28.086

// 6 C 2s²2p²
// Carbon
// 12.011

// 26 Fe 3d⁶4s²
// Iron
// 55.847

        // var object_instance = Instantiate(prefab, new Vector3(point.x, 0, point.y), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        // object_instance.GetComponent<SpriteRenderer>().sprite = sprite;
        // object_instance.name = point.ToString();
        // print ("Instantiated " + object_instance.name);
        // // object_instance.GetComponent<SpriteRenderer>().size = new Vector2(float.Parse(size.Split(',')[0]), float.Parse(size.Split(',')[1]));
        // object_instance.transform.SetParent(this.transform);//.parent.GameObject);//structure.transform.Find("Rotator"));
        // object_instance.transform.localPosition = new Vector2(
        //     point.x,
        //     point.y
        // );
        // object_instance.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        // if (mass == 0) mass = GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y * 10f;
        // if (this == null) { Destroy(this.gameObject); return -999; }

        // capacity = Mathf.Clamp(capacity, MIN, GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y * 10f);
        // // - input
        // return mass;
        return 10;
    }

    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 6);
    }

    
    void InstantiateTile(Point p, Sprite sprite)
    {
        if (this.transform.Find("Bulkhead" + p.ToString()) != null) {
            this.transform.Find("Bulkhead" + p.ToString()).gameObject.SetActive(true);
            this.transform.Find("Bulkhead" + p.ToString()).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;
            return;
        }
        var object_instance = Instantiate(prefab, new Vector3(p.x, 0, p.y), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        object_instance.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;
        object_instance.name = "Bulkhead" + p.ToString();
        // print ("Instantiated " + object_instance.name);
        // object_instance.GetComponent<SpriteRenderer>().size = new Vector2(float.Parse(size.Split(',')[0]), float.Parse(size.Split(',')[1]));
        object_instance.transform.SetParent(this.transform);//.parent.GameObject);//structure.transform.Find("Rotator"));
        object_instance.transform.localPosition = new Vector2(p.x, p.y);
        object_instance.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
    public override string GetIcon() { return "▥"; }
    public override string ToString()
    { //▤<b>≣ Data</b>
        string heap_string = "";
        int count = 0;
        foreach (var h in heap)
        {
            if (count % GetComponent<SpriteRenderer>().size.x == 0) heap_string += "\n  ";
            switch (h)
            {
                case TerrainType.Carbonaceous:
                    heap_string += "C";
                    break;
                case TerrainType.Siliceous:
                    heap_string += "S";
                    break;
                case TerrainType.Metallic:
                    heap_string += "F";                    
                    break;
                case TerrainType.Torpedo:
                    heap_string += "T";    
                    break;
            }
            if (++count != heap.Count) heap_string += ", ";

            
            // x = count % (int)GetComponent<SpriteRenderer>().size.x + .5f;
            // y = Mathf.Floor(count / GetComponent<SpriteRenderer>().size.x) + 1;
        }
        if (count != 0) heap_string += "\n ";
        return $"{GetIcon()} {this.name}\n{base.ToString()}\n heap = new Heap ({capacity}) {{{heap_string}}};\n}}\n☑_Ok\n☒_Cancel\n☒_Delete\n⍰⍰_Help";
            //  ┣ ↹ " + GetComponent<SpriteRenderer>().size.ToString() + "\n  ┗ ↺ " + gameObject.transform.localEulerAngles.z.ToString("0.0") + "°\n  ";
    }
    // {
        // return "\n   <b>" + name + "</b>\n  ┣ ↧ " + new Vector2(transform.localPosition.x, transform.localPosition.y).ToString() + "\n  ┗ ↹ " + GetComponent<SpriteRenderer>().size.ToString() + "\n  <b>♨ Fuel</b>\n  ┗ " + mass.ToString("0.0");//"\n  ┗ ↺ " + gameObject.transform.localEulerAngles.z.ToString("0.0") + "°\n  <b>♨ Fuel</b>\n  ┗ " + mass.ToString("0.0");
    // }
}