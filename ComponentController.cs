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
    // public ComponentController[] connected_components;
    protected float temperature = .5f;
    protected float hitpoints;
    protected float action_speed = .1f, action_cooldown = 0f;
    protected string prefab_path;
    
    UIController component_panel;
    PlotterController controller;


    void Start()
    {
        controller = GameObject.Find("PlotterOverlay").GetComponent<PlotterController>();
        Action(0);
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

    void OnMouseOver() 
    {
        controller.Focus(this.name, this.GetType());
    }
    void OnMouseExit() 
    {
        controller.Unfocus();
    }

    void OnMouseUp()
    {

    }

}