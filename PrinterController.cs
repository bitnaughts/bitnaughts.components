using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;

public class PrinterController : ComponentController 
{   
    Vector2 print_size;
    Vector2 print_head_size;
    Vector2 print_position;

    GameObject beam_h, beam_v, print_head;
    private void Awake() {
        beam_h = transform.Find("BeamH").gameObject;
        beam_v = transform.Find("BeamV").gameObject;
        print_head = transform.Find("PrintHead").gameObject;
    }
    public override void Focus() {
        beam_h.GetComponent<SpriteRenderer>().size = new Vector2(1, GetComponent<SpriteRenderer>().size.x);
        beam_v.GetComponent<SpriteRenderer>().size = new Vector2(1, GetComponent<SpriteRenderer>().size.y);
    }
    // To print objects, the data must be serialized into float array of commands, then sent to printer one-by-one?
    // ComponentId, sizeX, sizeY?
    public override float Action (float input) 
    {
        return 0;
    }

    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }


    public override string ToString()
    {
        return "";
    }
    
}