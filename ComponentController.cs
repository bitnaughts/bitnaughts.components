/*
|| Components:
||   Building blocks of ships and stations
|| = = = = = = = 
|| - Processor, runs code to control all other components (one open direction, all others sides can mount subcomponents)
|| - Scaffold, can control item flow between connected Bulkheads (all open directions, or can mount subcomponents)
|| - Bulkhead, holds items (two open sides, other sides can mount subcomponents)
|| - Factory, converts items into other items (two open sides [input, output], other sides can mount subcomponents)
|| - Constructor, materializes items into concrete shapes
|| - Gimbal, gives rotational control to subcomponents
|| - Gun, breaks apart asteroids and ships
|| - Engine, provides thrust
|| - Dock, links ships together
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;

public abstract class ComponentController : MonoBehaviour
{
    PlotterController controller;
    public Sprite sprite, inverse;
    void Start()
    {
        if (GameObject.Find("PlotterOverlay") != null) controller = GameObject.Find("PlotterOverlay").GetComponent<PlotterController>();
        Action(0);
        Focus();
    }
    public void Design() {
        GetComponent<SpriteRenderer>().sprite = inverse;
    }
    public void Launch() {
        GetComponent<SpriteRenderer>().sprite = sprite;
        GetComponent<SpriteRenderer>().enabled = true;
    }
    
    public Transform GetTransform()
    {
        return this.transform;
    }

    public abstract void Focus();

    public abstract float Action(float input);
    
    public abstract Vector2 GetMinimumSize();

    public void Remove()
    {
        // foreach (var part in parts) Destroy (part.Value);
        Destroy(this.gameObject);
    }

    // Calculate this manually instead of event-driven
    void OnMouseOver() 
    {
        if (controller != null) controller.Focus(this.name, this.GetType());
    }
    void OnMouseExit() 
    {
        if (controller != null) controller.Unfocus();
    }

    void OnMouseUp()
    {

    }
    public override string ToString() {
        return "\n " + GetType().ToString().Replace("Controller", "") + "(" + name + ")\n╟ ⌖ " + transform.localPosition.ToString() + "\n╟ Size: ⮽ " + GetComponent<SpriteRenderer>().size.ToString() + "\n╟ Rote: " + gameObject.transform.localEulerAngles.z;
    }

}