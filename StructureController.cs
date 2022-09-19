using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StructureController : MonoBehaviour
{
    const float debug_duration = .001f;
    public Dictionary<string, ComponentController> components;
    protected Vector3 center_of_mass;
    protected int child_count;
    protected Transform rotator;       
    protected UnityEngine.Color debug_color;
    RaycastHit hit;
    public GameObject Explosion;
    public bool Launched = false;
    public float explosion_timer = 0;
    public void Start()
    {
        components = new Dictionary<string, ComponentController>();
        foreach (var controller in GetComponentsInChildren<ComponentController>()) 
        {
            print (controller.name);
            components.Add(controller.name, controller);
            // switch (controller) {
            //     case GimbalController gimbal:
            //         // foreach (var gimbal_controller in gimbal.transform.GetChild(0).GetComponentsInChildren<ComponentController>()) {
            //         //     components.Add(gimbal_controller.name, gimbal_controller);      
            //         // }
            //         // break;
            // }
        }
        rotator = transform.Find("Rotator");
        child_count = GetComponentsInChildren<ComponentController>().Length;

        Design();
    }

    public void Explode() {
        explosion_timer = 1;
    }

    public void Move(string component, Vector2 direction) 
    {
        if (!components.ContainsKey(component)) return;
        components[component].transform.Translate(direction * .5f);
    }
    public void SetPosition(string component, Vector2 position) 
    {
        if (!components.ContainsKey(component)) return;
        components[component].transform.position = position;
    }
    
    public void SetSize(string component, Vector2 size) 
    {
        if (!components.ContainsKey(component)) return;
        components[component].GetComponent<SpriteRenderer>().size = size;
    }
    

    public void Upsize(string component, Vector2 direction) 
    {
        if (!components.ContainsKey(component)) return;
        components[component].transform.Translate(direction/2);
        components[component].GetComponent<SpriteRenderer>().size += new Vector2(Mathf.Abs(direction.x), Mathf.Abs(direction.y));
    }

    public void Downsize(string component, Vector2 direction) 
    {
        if (!components.ContainsKey(component)) return;
        components[component].transform.Translate(-direction/2);
        components[component].transform.GetComponent<SpriteRenderer>().size -= new Vector2(Mathf.Abs(direction.x), Mathf.Abs(direction.y));
    }

    public void Rotate90(string component)
    {
        if (!components.ContainsKey(component)) return;
        components[component].transform.Rotate(new Vector3(0,0,-90));
    }
    public void RotateM90(string component)
    {
        if (!components.ContainsKey(component)) return;
        components[component].transform.Rotate(new Vector3(0,0,90));
    }
    public float GetRotation(string component) {
        if (!components.ContainsKey(component)) return 0;
        return components[component].transform.rotation.z;
    }

    public void Remove(string component) 
    {
        if (component.Contains("_")) component = component.Split('_')[1];
        print (component);
        Destroy(components[component].transform.gameObject);
    }

    public Vector2 GetSize(string component) 
    {
        if (!components.ContainsKey(component)) {
            return Vector2.zero;
    }
        return components[component].transform.GetComponent<SpriteRenderer>().size;
    }

    public Vector3 GetLocalPosition(string component) 
    {
        if (!components.ContainsKey(component)) return Vector2.zero;
        return components[component].transform.localPosition;
    }
    public Vector3 GetPosition(string component) 
    {
        if (!components.ContainsKey(component)) Start();
        if (!components.ContainsKey(component)) return Vector2.zero;
        return components[component].transform.position;
    }

    public Vector2 GetMinimumSize(string component) 
    {
        if (!components.ContainsKey(component)) return Vector2.zero;
        return components[component].transform.GetComponent<ComponentController>().GetMinimumSize();
    }

    public void DisableColliders() 
    {
        foreach (var component in components.Values)
        {
            component.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void EnableColliders() 
    {
        if (components == null) return;
        foreach (var component in components.Values)
        {
            component.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public ProcessorController GetProcessorController(string component)
    {
        if (!components.ContainsKey(component)) return null;
        switch (components[component]) {
            case ProcessorController processor:
                return processor;
            default:
                return null;
        }
    }
    public string[] GetProcessorControllers()
    {
        if (components == null) return null;
        List<string> processors = new List<string>();
        foreach (var component in components.Values) {
            switch (component) {
                case ProcessorController processor:
                    processors.Add(processor.name);
                    break;
            }
        }
        return processors.ToArray();
    }

    public string[] GetControllers()
    {
        List<string> controllers = new List<string>();
        foreach (var component in components.Values) {
                controllers.Add(component.GetIcon() + "_" + component.name);
        }
        return controllers.ToArray();
    }
    public void Design()
    {
        if (components == null) return;
        foreach (var component in components.Values) {
            component.Design();
        }
    }
    public void Launch()
    {
        if (components == null) return;
        foreach (var component in components.Values) {
            component.Launch();
        }
    }

    public void SetInstructions(string component, string instructions)
    {
        if (!components.ContainsKey(component)) return;
        switch (components[component]) {
            case ProcessorController processor:
                processor.SetInstructions(instructions);
                break;
            default: //Want all components to be scriptable? Adjust here.
                break;
        }
    }
    public void DeleteLine(string component)
    {
        if (!components.ContainsKey(component)) return;
        switch (components[component]) {
            case ProcessorController processor:
                processor.DeleteLine();
                break;
            default: //Want all components to be scriptable? Adjust here.
                break;
        } 
    }
    public void SetOperand(string component, string op)
    {
        if (!components.ContainsKey(component)) return;
        switch (components[component]) {
            case ProcessorController processor:
                processor.SetOperand(op);
                break;
            default: //Want all components to be scriptable? Adjust here.
                break;
        } 
    }
    public void AddOperand(string component, string op)
    {
        if (!components.ContainsKey(component)) return;
        switch (components[component]) {
            case ProcessorController processor:
                processor.AddOperand(op);
                break;
            default: //Want all components to be scriptable? Adjust here.
                break;
        } 
    }
    public void ModifyConstantOperand(string component, float delta)
    {
        if (!components.ContainsKey(component)) return;
        switch (components[component]) {
            case ProcessorController processor:
                processor.ModifyConstantOperand(delta);
                break;
            default: //Want all components to be scriptable? Adjust here.
                break;
        } 
    }
    public void AddLine(string component, int direction)
    {
        if (!components.ContainsKey(component)) return;
        switch (components[component]) {
            case ProcessorController processor:
                processor.AddLine(direction);
                break;
            default: //Want all components to be scriptable? Adjust here.
                break;
        } 
    }
    public void Scroll(string component, int direction)
    {
        if (!components.ContainsKey(component)) return;
        switch (components[component]) {
            case ProcessorController processor:
                processor.Scroll(direction);
                break;
            default: //Want all components to be scriptable? Adjust here.
                break;
        } 
    }
    float gimbal_test = 0f;
    float gimbal_step = 10f;
    float thrust_rotation = 0f;

    void FixedUpdate()
    {
        if (child_count != GetComponentsInChildren<ComponentController>().Length) Start();
        if (components == null) return;

        center_of_mass = Vector3.zero;
        float active_component_count = 0;
        foreach (var controller in components.Values) {
           if (controller != null && controller.enabled) {
                center_of_mass += new Vector3(controller.GetTransform().position.x, controller.GetTransform().position.y);
                active_component_count++;
                controller.Ping();
                // switch (controller) {
                    // case ProcessorController processor:
                    //     processor.Action(components);
                    //     break;
                    // case CannonController cannon:
                    //     cannon.Cooldown();
                    //     break;
                // }
           }
        }
        

        center_of_mass /= active_component_count;

        // Testing Center of Mass:
        // Debug.DrawLine(center_of_mass, center_of_mass + Vector3.up, Color.green, debug_duration, false);

        //Checking surrounding components
        // if (Physics.Raycast(transform.position, -Vector3.up, out hit))
            // print("Found an object - distance: " + hit.distance);


        float rotation = 0f;
        Vector2 translation = new Vector2(0, 0);

        foreach (var controller in components.Values)
        {
            if (controller != null && controller.enabled) switch (controller) {
                case ThrusterController thruster:
                    // Debug.DrawLine(thruster.GetPosition(), thruster.GetPosition() + thruster.GetThrustVector(), Color.green, debug_duration, false);
                    // Debug.DrawLine(thruster.GetPosition(), center_of_mass, Color.green, debug_duration, false);
                    translation += thruster.GetThrustVector();
                    thrust_rotation = thruster.GetThrustVector().magnitude * Mathf.Sin(
                        Vector2.SignedAngle(
                            thruster.GetThrustVector(), 
                            thruster.GetPosition() - center_of_mass
                        ) * Mathf.Deg2Rad
                    );
                    rotation += thrust_rotation;
                    break;
                case BoosterController booster:
                    // Debug.DrawLine(thruster.GetPosition(), thruster.GetPosition() + thruster.GetThrustVector(), Color.green, debug_duration, false);
                    // Debug.DrawLine(booster.GetPosition(), center_of_mass, Color.green, debug_duration, false);
                    print (booster.GetThrustVector());
                    translation += booster.GetThrustVector();
                    // Debug.DrawLine(booster.GetPosition(), booster.GetPosition() + booster.GetThrustVector(), Color.green, debug_duration, false);
                    // Debug.DrawLine(booster.GetPosition(), center_of_mass, Color.green, debug_duration, false);
                    thrust_rotation = booster.GetThrustVector().magnitude * Mathf.Sin(
                        Vector2.SignedAngle(
                            booster.GetThrustVector(), 
                            booster.GetPosition() - center_of_mass
                        ) * Mathf.Deg2Rad
                    );
                    rotation += thrust_rotation;
                    break;
            }
        }
        if (active_component_count > 0) {
            rotator.Rotate(new Vector3(0, 0, -rotation));
            transform.Translate(translation / active_component_count);
        }
        if (explosion_timer > 0) {
            explosion_timer++;
            GameObject explosion = Instantiate(
                Explosion,
                this.transform.position,
                this.transform.rotation,      
                this.transform
            ) as GameObject;
            explosion.transform.localScale = new Vector2(explosion_timer, explosion_timer);
            explosion.transform.SetParent(GameObject.Find("World").transform);
            if (explosion_timer == 30) {
                Destroy(this.gameObject);
            }
        }
    }
    public bool IsComponent(string component)
    {
        if (component.Contains("\t")) component = component.Substring(2);
        return components.ContainsKey(component);
    }
    public string[] GetInteractiveComponents() {
        List<string> objects = new List<string>();
        foreach (var controller in components.Values)
        {
            switch (controller) {
                case ProcessorController processor:
                    objects.Add(processor.name);
                    break;
                case ThrusterController thruster:
                    objects.Add(thruster.name);
                    break;
                case GimbalController gimbal:
                    objects.Add(gimbal.name);
                    break;
                case BoosterController booster:
                    objects.Add(booster.name);
                    break;
                case CannonController cannon:
                    objects.Add(cannon.name);
                    break;
                case SensorController sensor:
                    objects.Add(sensor.name);
                    break;
                case BulkheadController bulkhead:
                    objects.Add(bulkhead.name);
                    break;
                case PrinterController printer:
                    objects.Add(printer.name);
                    break;
            }
        }
        foreach (var controller in components.Values)
        {
            switch (controller) {
                case ProcessorController processor:
                    objects.Add(processor.name);
                    break;
                case ThrusterController thruster:
                    objects.Add(thruster.name);
                    break;
                case GimbalController gimbal:
                    objects.Add(gimbal.name);
                    break;
                case BoosterController booster:
                    objects.Add(booster.name);
                    break;
                case CannonController cannon:
                    objects.Add(cannon.name);
                    break;
                case SensorController sensor:
                    objects.Add(sensor.name);
                    break;
                case BulkheadController bulkhead:
                    objects.Add(bulkhead.name);
                    break;
                case PrinterController printer:
                    objects.Add(printer.name);
                    break;
            }
        }
        return objects.ToArray();
    }
    public string[] GetOtherComponents(string selected) 
    {
        if (!components.ContainsKey(selected)) return null;
        string[] others = new string[components.Count - 1];
        int i = 0;
        foreach (var key in components.Keys) {
            if (key != selected) others[i++] = key;
        }
        return others;
    }
    public string GetComponentToString(string selected)
    {
        if (components == null) return "";
        if (!components.ContainsKey(selected)) return "";
        components[selected].Focus();
        return components[selected].ToString();
    }

    public string ToString(string selected)
    {
        string output = "\n " + this.name;
        foreach (var controller_name in components.Keys)
        {
            if (selected == controller_name) output += "<b>\n> " + controller_name + "</b>";
            else output += "\n> " + controller_name;
        }
        return output;
    }
    public override string ToString()
    {
        string output = "";
        foreach (var component in components.Values)
        {
            output += component.ToString() + "\n";
        }
        return output;
    }
}

public static class ExtensionMethods {
    public static float Remap (this float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    // Or IsNanOrInfinity
    public static bool HasValue(this float value)
    {
        return !float.IsNaN(value) && !float.IsInfinity(value);
    }
    public static string Repeat(this string s, int n)
    {
        return String.Concat(Enumerable.Repeat(s, n));
    }
}