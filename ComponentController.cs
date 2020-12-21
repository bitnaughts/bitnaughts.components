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
    public Color material_color, debug_color;
    
    UIController component_panel;

    public int seed = 0;

    List<GameObject> meshes = new List<GameObject>();

    void Start()
    {
        debug_color = new UnityEngine.Color(0,1,0);//UnityEngine.Random.Range(0.0f,1.0f),UnityEngine.Random.Range(0.0f,1.0f),UnityEngine.Random.Range(0.0f,1.0f));
        // var panel = GameObject.Find("ComponentPanel");
        // component_panel = GameObject.Find("ComponentPanel")?.GetComponent<UIController>();
    }
    
    public Transform GetTransform()
    {
        return this.transform;
    }

    public abstract float Action(float input);

    public void Remove()
    {
        // foreach (var part in parts) Destroy (part.Value);
        Destroy(this.gameObject);
    }

    void OnMouseUp()
    {
        // component_panel.Set(this);
        // Destroy (gameObject); 
        // if (componentPanel == null) 
        // var structure_controller = GetComponentInParent(typeof(StructureController)) as StructureController;

    }

}