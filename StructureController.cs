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
    protected Vector2 center_of_mass;
    protected int child_count;
    protected Transform rotator;       
    protected UnityEngine.Color debug_color;
    RaycastHit hit;
    public string default_content = "\n None... \n\n To add, tap\n plotter grid.";

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
        child_count = rotator.childCount;
    }

    public void Move(string component, Vector2 direction) 
    {
        if (!components.ContainsKey(component)) return;
        components[component].transform.Translate(direction);
    }

    public void Upsize(string component, Vector2 direction) 
    {
        if (!components.ContainsKey(component)) return;
        components[component].transform.Translate(direction/2);
        components[component].transform.GetComponent<SpriteRenderer>().size += new Vector2(Mathf.Abs(direction.x), Mathf.Abs(direction.y));
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

    public void Remove(string component) 
    {
        Destroy(components[component].transform.gameObject);
    }

    public Vector2 GetSize(string component) 
    {
        if (!components.ContainsKey(component)) return Vector2.zero;
        return components[component].transform.GetComponent<SpriteRenderer>().size;
    }

    public Vector2 GetLocalPosition(string component) 
    {
        if (!components.ContainsKey(component)) return Vector2.zero;
        return components[component].transform.localPosition;
    }
    public Vector2 GetPosition(string component) 
    {
        if (!components.ContainsKey(component)) return Vector2.zero;
        return components[component].transform.position;
    }

    public Vector2 GetMinimumSize(string component) 
    {
        return components[component].transform.GetComponent<ComponentController>().GetMinimumSize();
    }

    public void DisableColliders() 
    {
        foreach (var component in components.Values)
        {
            component.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void EnableColliders() 
    {
        if (components == null) return;
        foreach (var component in components.Values)
        {
            component.gameObject.GetComponent<BoxCollider2D>().enabled = true;
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
    public string GetEditInstruction(string component)
    {
        if (!components.ContainsKey(component)) return "";
        switch (components[component]) {
            case ProcessorController processor:
                return processor.GetEditInstruction();
            default: //Want all components to be scriptable? Adjust here.
                return "";
        } 
    }
    public string GetEditInstructionCategory(string component)
    {
        if (!components.ContainsKey(component)) return "";
        switch (components[component]) {
            case ProcessorController processor:
                return processor.GetEditInstructionCategory();
            default: //Want all components to be scriptable? Adjust here.
                return "";
        } 
    }
    public string GetEditInstructionText(string component)
    {
        if (!components.ContainsKey(component)) return "";
        switch (components[component]) {
            case ProcessorController processor:
                return processor.GetEditInstructionText();
            default: //Want all components to be scriptable? Adjust here.
                return "";
        } 
    }
    public string GetNextLabel(string component)
    {
        if (!components.ContainsKey(component)) return "";
        switch (components[component]) {
            case ProcessorController processor:
                return processor.GetNextLabel();
            default: //Want all components to be scriptable? Adjust here.
                return "";
        } 
    }
    public string GetNextVariable(string component)
    {        
        if (!components.ContainsKey(component)) return "";
        switch (components[component]) {
            case ProcessorController processor:
                return processor.GetNextVariable();
            default: //Want all components to be scriptable? Adjust here.
                return "";
        } 
    }
    public string[] GetVariables(string component)
    {        
        if (!components.ContainsKey(component)) return null;
        switch (components[component]) {
            case ProcessorController processor:
                return processor.GetVariables();
            default: //Want all components to be scriptable? Adjust here.
                return null;
        } 
    }
    public string[] GetLabels(string component)
    {
        if (!components.ContainsKey(component)) return null;
        switch (components[component]) {
            case ProcessorController processor:
                return processor.GetLabels();
            default: //Want all components to be scriptable? Adjust here.
                return null;
        }
    }
    public bool IsVariable(string component, string variable)
    {        
        if (!components.ContainsKey(component)) return false;
        switch (components[component]) {
            case ProcessorController processor:
                return processor.IsVariable(variable);
            default: //Want all components to be scriptable? Adjust here.
                return false;
        } 
    }
    public bool IsLabel (string component, string label)
    {
        if (!components.ContainsKey(component)) return false;
        switch (components[component]) {
            case ProcessorController processor:
                return processor.IsLabel(label);
            default: //Want all components to be scriptable? Adjust here.
                return false;
        }     
    }

    float gimbal_test = 0f;
    float gimbal_step = 10f;
    float thrust_rotation = 0f;

    void FixedUpdate()
    {
        if (child_count != rotator.childCount) Start();
        if (components == null) return;

        center_of_mass = Vector2.zero;
        float active_component_count = 0;
        foreach (var controller in components.Values) {
           if (controller != null && controller.enabled) {
                center_of_mass += new Vector2(controller.GetTransform().position.x, controller.GetTransform().position.y);
                active_component_count++;
                switch (controller) {
                    case ProcessorController processor:
                        processor.Action(components);
                        break;
                    case CannonController cannon:
                        cannon.Cooldown();
                        break;
                }
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
                    Debug.DrawLine(thruster.GetPosition(), thruster.GetPosition() + thruster.GetThrustVector(), Color.green, debug_duration, false);
                    Debug.DrawLine(thruster.GetPosition(), center_of_mass, Color.green, debug_duration, false);
                    translation -= thruster.GetThrustVector();
                    thrust_rotation = 10 * thruster.GetThrustVector().magnitude * Mathf.Sin(
                        Vector2.SignedAngle(
                            thruster.GetThrustVector(), 
                            thruster.GetPosition() - center_of_mass
                        ) * Mathf.Deg2Rad
                    );
                    rotation += thrust_rotation;
                    break;
                case BoosterController booster:
                    Debug.DrawLine(booster.GetPosition(), booster.GetPosition() + booster.GetThrustVector(), Color.green, debug_duration, false);
                    Debug.DrawLine(booster.GetPosition(), center_of_mass, Color.green, debug_duration, false);
                    translation -= booster.GetThrustVector();
                    thrust_rotation = 20 * booster.GetThrustVector().magnitude * Mathf.Sin(
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
            rotator.Rotate(new Vector3(0, 0, rotation / active_component_count));
            transform.Translate(translation / active_component_count);
        }
        // if (transform.position.x > 420 || transform.position.x < -20 || transform.position.z > 420 || transform.position.z < -20) Destroy(this.gameObject);
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
                case CacheController cache:
                    objects.Add(cache.name);
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
        string output = "Objects:";
        foreach (var component in components.Values)
        {
            output += "\n" + component.ToString() + ";";
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