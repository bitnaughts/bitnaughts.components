﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StructureController : MonoBehaviour
{
    public bool isAi = false;
    const float debug_duration = .01f;
    public Dictionary<string, ComponentController> components;
    // public Dictionary<string, ClassController> classes;
    protected Vector2 center_of_mass;
    protected int child_count;
    protected Transform rotator;       
    public Vector2 translation;
    public float average_rotation;
    public float rotation;
    protected UnityEngine.Color debug_color;
    RaycastHit hit;
    public GameObject Explosion;
    public bool Launched = false;
    public float explosion_timer = 0, delete_timer = 0;
    public float random_initial_speed = 0, random_rotation = 0;
    public int Hitpoints;
    public void Hit(int damage) {
        Hitpoints -= damage;
        if (Hitpoints < 0) Explode();
    }
    public void Start()
    {
        // if (classes == null) {
            // classes = new Dictionary<string, ClassController>();
            // classes.Add("◎", new ClassController("◎")); //Booster
            // classes.Add("◉", new ClassController("◉")); //Thruster
            // classes.Add("◍", new ClassController("◍")); //Cannon
            // classes.Add("▥", new ClassController("▥")); //Bulkhead
            // classes.Add("▩", new ClassController("▩")); //Processor
            // classes.Add("▣", new ClassController("▣")); //Gimbal
            // classes.Add("◌", new ClassController("◌"));  //Sensor
            // classes.Add("▦", new ClassController("▦")); //Printer
        // }
        components = new Dictionary<string, ComponentController>();
        foreach (var controller in GetComponentsInChildren<ComponentController>()) 
        {
            // print (controller.name + " -> " + this.name);
            components.Add(controller.name, controller);
            controller.structure = this;
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

        // Design();   
        if (this.transform.position.x == 0 && this.transform.position.y == 0) {
            // float x = UnityEngine.Random.Range(-8f, 8f) * 30f, y = UnityEngine.Random.Range(-8f, 8f) * 30f;
            // this.transform.position = new Vector2(x, y);
            // GameObject.Find("Debris").transform.position = new Vector2(x, y);
        }
    }

    public void Explode() {
        explosion_timer++;
    }

    public void Move(string component, Vector2 direction) 
    {
        if (components == null) return;
        // if (component[1] == ' ') 
        component = component.Substring(2);
        if (!components.ContainsKey(component)) return;
        components[component].transform.Translate(direction * .5f);
    }
    public void SetPosition(string component, Vector2 position) 
    {
        if (components == null) return;
        // if (component[1] == ' ') 
        component = component.Substring(2);
        if (!components.ContainsKey(component)) return;
        components[component].transform.position = position;
    }
    
    public void SetSize(string component, Vector2 size) 
    {
        if (components == null) return;
        // if (component[1] == ' ') 
        component = component.Substring(2);
        if (!components.ContainsKey(component)) return;
        components[component].GetComponent<SpriteRenderer>().size = size;
    }
    
    public void UpdateCollider(string component) 
    {
        if (components == null) return;
        component = component.Substring(2);
        if (!components.ContainsKey(component)) return;
        if (components[component].GetComponent<BoxCollider>() == null) return;
        if (components[component].GetComponent<PrinterController>() != null) return;
        foreach (var c in components.Values)
        {
            if (c.gameObject.GetComponent<BoxCollider>() != null) c.GetComponent<BoxCollider>().size = new Vector3(c.GetComponent<BoxCollider>().size.x, c.GetComponent<BoxCollider>().size.y, 1f);
        }
        components[component].GetComponent<BoxCollider>().size = new Vector3(components[component].GetComponent<BoxCollider>().size.x, components[component].GetComponent<BoxCollider>().size.y, 10f);

    }

    public void Upsize(string component, Vector2 direction) 
    {
        if (components == null) return;
        // if (component[1] == ' ') 
        component = component.Substring(2);
        if (!components.ContainsKey(component)) return;
        components[component].transform.Translate(direction/2);
        components[component].GetComponent<SpriteRenderer>().size += new Vector2(Mathf.Abs(direction.x), Mathf.Abs(direction.y));
        components[component].GetComponent<BoxCollider>().size = components[component].GetComponent<SpriteRenderer>().size;

    }

    public void Downsize(string component, Vector2 direction) 
    {
        if (components == null) return;
        // if (component[1] == ' ') 
        component = component.Substring(2);
        if (!components.ContainsKey(component)) return;
        components[component].transform.Translate(-direction/2);
        components[component].GetComponent<SpriteRenderer>().size -= new Vector2(Mathf.Abs(direction.x), Mathf.Abs(direction.y));
        components[component].GetComponent<BoxCollider>().size = components[component].GetComponent<SpriteRenderer>().size;
    }

    public void Rotate90(string component)
    {
        if (components == null) return;
        // if (component[1] == ' ') 
        component = component.Substring(2);
        if (!components.ContainsKey(component)) return;
        components[component].transform.Rotate(new Vector3(0,0,-90));
    }
    public void RotateM90(string component)
    {
        if (components == null) return;
        // if (component[1] == ' ') 
        component = component.Substring(2);
        if (!components.ContainsKey(component)) return;
        components[component].transform.Rotate(new Vector3(0,0,90));
    }
    public float GetRotation(string component) {
        if (components == null) return 0;
        // if (component[1] == ' ') 
        component = component.Substring(2);
        if (!components.ContainsKey(component)) return 0;
        // print (components[component].transform.eulerAngles);
        return components[component].transform.eulerAngles.y;
    }
    public float GetLocalRotation(string component) {
        if (components == null) return 0;
        // if (component[1] == ' ') 
        component = component.Substring(2);
        if (!components.ContainsKey(component)) return 0;
        // print (components[component].transform.localEulerAngles);
        return components[component].GetLocalRotation();
    }

    public void Remove(string component) 
    {
        if (components == null) return;
        component = component.Substring(2);
        if (component.Contains("_")) component = component.Split('_')[1];
        Destroy(components[component].transform.gameObject);
    }

    public Vector2 GetSize(string component) 
    {
        if (components == null) return Vector2.zero;
        component = component.Substring(2);
        if (!components.ContainsKey(component)) return Vector2.zero;
        if (components[component].transform.GetComponent<SpriteRenderer>() == null) return new Vector2(14,28); // TODO calculate size of asteroid, Dict -> Array .Lengths
        return components[component].transform.GetComponent<SpriteRenderer>().size;
    }

    public Vector3 GetLocalPosition(string component) 
    {
        if (components == null) return Vector2.zero;
        component = component.Substring(2);
        if (!components.ContainsKey(component)) return Vector2.zero;
        return components[component].transform.localPosition;
    }
    public Vector3 GetPosition(string component) 
    {
        if (components == null) return Vector2.zero;
        component = component.Substring(2);
        if (!components.ContainsKey(component)) Start();
        if (!components.ContainsKey(component)) return Vector2.zero;
        return components[component].transform.position;
    }

    public Vector2 GetMinimumSize(string component) 
    {
        if (components == null) return Vector2.zero;
        component = component.Substring(2);
        if (!components.ContainsKey(component)) return Vector2.zero;
        return components[component].transform.GetComponent<ComponentController>().GetMinimumSize();
    }

    public void DisableColliders() 
    {
        if (components == null) return;
        foreach (var component in components.Values)
        {
            if (component.gameObject.GetComponent<BoxCollider>() != null) component.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void EnableColliders() 
    {
        if (components == null) return;
        foreach (var component in components.Values)
        {
            if (component.gameObject.GetComponent<BoxCollider>() != null) component.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public ProcessorController GetProcessorController(string component)
    {
        if (components == null) return null;
        component = component.Substring(2);
        if (!components.ContainsKey(component)) return null;
        switch (components[component]) {
            case ProcessorController processor:
                return processor;
            default:
                return null;
        }
    }
    public BulkheadController GetBulkheadController()
    {
        if (components == null) return null;
        foreach (var component in components.Values) {
            switch (component) {
                case BulkheadController bulkhead:
                    if (bulkhead.heap.Count() < bulkhead.capacity) 
                        return bulkhead;
                    break;
            }
        }
        return null;
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
        if (components == null) return null;
        List<string> controllers = new List<string>();
        foreach (var component in components.Values) {
            if (component.launched) controllers.Add(component.GetIcon() + " " + component.name);
        }
        return controllers.ToArray();
    }
    public Dictionary<string, ComponentController> GetComponentControllers()
    {
        return components;
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

    // public void SetInstructions(string component, string instructions)
    // {
    //     if (!components.ContainsKey(component)) return;
    //     switch (components[component]) {
    //         case ProcessorController processor:
    //             processor.SetInstructions(instructions);
    //             break;
    //         default: //Want all components to be scriptable? Adjust here.
    //             break;
    //     }
    // }
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
    float active_component_count = 0f;

    void FixedUpdate()
    {
        delete_timer -= Time.deltaTime;
        if (child_count != GetComponentsInChildren<ComponentController>().Length) Start();
        if (components == null) return;
        active_component_count = 0;
        foreach (var controller in components.Values) {
            if (controller != null && controller.enabled) {
                    active_component_count++;
            }
        }
        if (true) {//!isAi) {
            center_of_mass = Vector2.zero;
            foreach (var controller in components.Values) {
                if (controller != null && controller.enabled) {
                    center_of_mass += new Vector2(controller.GetTransform().localPosition.x, controller.GetTransform().localPosition.y);
                    controller.Ping();
                    switch (controller) {
                        case ProcessorController processor:
                            processor.Action(components);
                            break;
                        // case CannonController cannon:
                        //     cannon.Cooldown();
                        //     break;
                    }
                }
            }
            center_of_mass /= active_component_count;
            translation = translation * .95f;
            rotation = rotation * .95f;
            float angle = 0;
            foreach (var controller in components.Values)
            {
                if (controller != null && controller.enabled) switch (controller) {
                    case ThrusterController thruster:                        
                        angle = Vector2.SignedAngle(
                            thruster.GetThrustVector(), 
                            thruster.GetPosition() - center_of_mass
                        ) + this.rotator.localEulerAngles.z;
                        translation += thruster.GetThrustVector() / (active_component_count * 10f);// * Mathf.Cos(angle * Mathf.Deg2Rad);
                        thrust_rotation = thruster.GetThrustVector().magnitude / active_component_count * Mathf.Sin(angle * Mathf.Deg2Rad); //* -thruster.GetThrustVector().magnitude * 2; //(thruster.GetPosition().x - center_of_mass.x) 
                        rotation -= thrust_rotation / 2f;
                        break;
                    case BoosterController booster:
                        angle = Vector2.SignedAngle(
                            booster.GetThrustVector(), 
                            booster.GetPosition() - center_of_mass
                        ) + this.rotator.localEulerAngles.z;
                        translation += booster.GetThrustVector() / (active_component_count * 10f);// * Mathf.Cos(angle * Mathf.Deg2Rad);
                        thrust_rotation = booster.GetThrustVector().magnitude / active_component_count * Mathf.Sin(angle * Mathf.Deg2Rad); //(booster.GetPosition().x - center_of_mass.x) 
                        rotation -= thrust_rotation;
                        break;
                }
            }
            rotator?.Rotate(new Vector3(0, 0, rotation));
            // average_rotation += rotation / Time.deltaTime;
        }
        else {
            switch (this.name) {
                case "battleship":
                    
                    break;
                case "carrier":

                    break;
                default:
                    break;
            }




            //set ai speed later UnityEngine.Random.Range(-8f, 8f)
            // this.transform.Translate(new Vector2(0, random_initial_speed));
            // random_rotation += UnityEngine.Random.Range(-.025f, .025f);
            // this.transform.Rotate(new Vector3(0, 0, random_rotation));
            // random_rotation = random_rotation / 2f;
        }
        if (active_component_count > 0) {
            transform.Translate(translation);
            // if (isAi) rotator.Rotate(new Vector3(0,0,-average_rotation * Time.deltaTime));
        }
        if (explosion_timer > 0) {
            explosion_timer += Time.deltaTime;
            // if (GetComponent<ParticleSystem>()) GetComponent<ParticleSystem>().Stop();
            if (GetComponent<ParticleSystem>()) {
                var exhaust_emission = GetComponent<ParticleSystem>().emission;
                exhaust_emission.rate = Mathf.Clamp(1 - explosion_timer / GetComponent<SpriteRenderer>().size.magnitude * 1.5f, 0, 1);
            }
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,Mathf.Clamp(1 - (explosion_timer/(GetComponent<SpriteRenderer>().size.magnitude*2f)), 0, 1));
            if (UnityEngine.Random.Range(0f, GetComponent<SpriteRenderer>().size.magnitude + 1) > explosion_timer) 
            {
                random_initial_speed += Time.deltaTime / (10 * explosion_timer);
                random_rotation += UnityEngine.Random.Range(-.25f, .25f);
                // print (transform.position);
                // print (transform.localPosition);
                GameObject.Find("World").GetComponent<PrefabCache>().PlayExplosion(
                    transform.position + new Vector3(
                        UnityEngine.Random.Range(-GetComponent<SpriteRenderer>().size.x, GetComponent<SpriteRenderer>().size.x),
                        UnityEngine.Random.Range(-GetComponent<SpriteRenderer>().size.x, GetComponent<SpriteRenderer>().size.x),
                        UnityEngine.Random.Range(-GetComponent<SpriteRenderer>().size.y * 2, GetComponent<SpriteRenderer>().size.y * 4)),
                    UnityEngine.Random.Range(GetComponent<SpriteRenderer>().size.magnitude, GetComponent<SpriteRenderer>().size.magnitude * 2),
                    "Small Ship");
            }
            if (explosion_timer + 1 > GetComponent<SpriteRenderer>().size.magnitude + 2) {
                if (this.gameObject.transform.GetComponentsInChildren<CameraController>().Length == 1) {
                    this.gameObject.transform.GetComponentsInChildren<CameraController>()[0].CycleView();
                    this.gameObject.transform.GetComponentsInChildren<CameraController>()[0].CycleView();
                }
                Destroy(this.gameObject); 
            } else if (explosion_timer + 1 > GetComponent<SpriteRenderer>().size.magnitude + 1) {
                if (GetComponent<ParticleSystem>()) 
                    GetComponent<ParticleSystem>().Stop();
                Destroy(GetComponent<BoxCollider>());
            }
            if (delete_timer < 0) {
                Destroy(this.gameObject); 
            }
        }
    }
    // + new Vector3(
                            // UnityEngine.Random.Range(-GetComponent<SpriteRenderer>().size.x, GetComponent<SpriteRenderer>().size.x), 
                            // UnityEngine.Random.Range(-GetComponent<SpriteRenderer>().size.y, GetComponent<SpriteRenderer>().size.y)
                        // )
    public bool IsComponent(string component)
    {
        component = component.Substring(2);
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
                case AsteroidController printer:
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
                case AsteroidController printer:
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
    public string GetComponentToString(string component)
    {
        if (components == null) return "Nil";
        component = component.Substring(2);
        if (!components.ContainsKey(component)) return "Nil";
        components[component].Focus();
        return components[component].ToString();
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
        // print (translation);
        // print ($"♘position:[{Neaten(this.transform.localPosition.x)},{Neaten(this.transform.localPosition.y)}");
        string output = $"♘position:[{Neaten(this.transform.localPosition.x)},{Neaten(this.transform.localPosition.y)},{Neaten(this.rotator.localEulerAngles.z)}]♘translation:[{Neaten(this.translation.x)},{Neaten(this.translation.y)}]";//,{Neaten(average_rotation)}]";
        output += "♘classes:";
        // if (classes != null) {
        //     foreach (ClassController c in classes.Values) {
        //         output += c.ToString();
        //     }
        // }
        output += "♘components:";
        foreach (var component in components.Values)
        {
            if (component != null && component.gameObject != null) {
                output += component.ComponentToString();
            }
        }
        output += "♘entities:";

        foreach (var entity in GameObject.Find("World").GetComponentsInChildren<ProjectileController>())
        {
            if (entity.gameObject.activeSelf) {
                output += $"♘{entity.name}:[{Neaten(entity.transform.localPosition.x)},{Neaten(entity.transform.localPosition.y)},{Neaten(entity.speed)},{Neaten(entity.transform.localEulerAngles.z)}]";
            }
        }
        return output;
    }
    public static string Neaten(float input) {
        return input.ToString("0.00").TrimEnd('0').TrimEnd('.');
    }
}

// public static class ExtensionMethods {
//     public static string Neaten(float input) {
//         return input.ToString("0.00").TrimEnd('0').TrimEnd('.');
//     }
//     public static float Remap (this float value, float from1, float to1, float from2, float to2) {
//         return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
//     }
//     // Or IsNanOrInfinity
//     public static bool HasValue(this float value)
//     {
//         return !float.IsNaN(value) && !float.IsInfinity(value);
//     }
//     public static string Repeat(this string s, int n)
//     {
//         return String.Concat(Enumerable.Repeat(s, n));
//     }
// }