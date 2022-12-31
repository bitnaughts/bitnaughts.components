using System;
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
<<<<<<< Updated upstream
    public List<ClassObj> classes;
    public InterpreterV3 interpreter;
=======
    // public Dictionary<string, ClassController> classes;
>>>>>>> Stashed changes
    protected Vector2 center_of_mass;
    protected int child_count;
    protected Transform rotator;       
    public Vector3 translation;
    public float average_rotation;
    protected UnityEngine.Color debug_color;
    RaycastHit hit;
    public GameObject Explosion;
    public bool Launched = false;
    public float explosion_timer = 0;
    float random_initial_speed = 0, random_rotation = 0;
    public int Hitpoints;
    public void Hit(int damage) {
        Hitpoints -= damage;
        if (Hitpoints < 0) Explode();
    }
    public void Start()
    {
<<<<<<< Updated upstream
        random_initial_speed = UnityEngine.Random.Range(0.05f, 0.1f);
        classes = new List<ClassObj>();
        classes.Add(new ClassObj("▩")); //Processor
        classes.Add(new ClassObj("◎")); //Booster
        classes.Add(new ClassObj("◉")); //Thruster
        classes.Add(new ClassObj("◍")); //Cannon
        classes.Add(new ClassObj("▥")); //Bulkhead
        classes.Add(new ClassObj("▣")); //Gimbal
        classes.Add(new ClassObj("◌")); //Sensor
        // classes.Add("▦", new ClassController("▦")); //Printer
        /* Load interpreter */
        interpreter = new InterpreterV3(classes);
=======
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
>>>>>>> Stashed changes
        components = new Dictionary<string, ComponentController>();
        foreach (var controller in GetComponentsInChildren<ComponentController>()) 
        {
            // print (controller.name);
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
        components[component].GetComponent<BoxCollider2D>().size = components[component].GetComponent<SpriteRenderer>().size;

    }

    public void Downsize(string component, Vector2 direction) 
    {
        if (!components.ContainsKey(component)) return;
        components[component].transform.Translate(-direction/2);
        components[component].GetComponent<SpriteRenderer>().size -= new Vector2(Mathf.Abs(direction.x), Mathf.Abs(direction.y));
        components[component].GetComponent<BoxCollider2D>().size = components[component].GetComponent<SpriteRenderer>().size;
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
    float active_component_count = 0f;

    void FixedUpdate()
    {
        if (child_count != GetComponentsInChildren<ComponentController>().Length) Start();
        if (components == null) return;
        active_component_count = 0;
        foreach (var controller in components.Values) {
            if (controller != null && controller.enabled) {
                    active_component_count++;
            }
        }
        if (!isAi) {
            center_of_mass = Vector2.zero;
            foreach (var controller in components.Values) {
            if (controller != null && controller.enabled) {
                    center_of_mass += new Vector2(controller.GetTransform().localPosition.x, controller.GetTransform().localPosition.y);
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
            

            // center_of_mass /= active_component_count;

            // Testing Center of Mass:
            // Debug.DrawLine(center_of_mass, center_of_mass + Vector2.up, Color.yellow, debug_duration, false);

            //Checking surrounding components
            // if (Physics.Raycast(transform.position, -Vector3.up, out hit))
                // print("Found an object - distance: " + hit.distance);


            float rotation = 0f;
            translation = new Vector3(0, 0, 0);
            float angle = 0;
            foreach (var controller in components.Values)
            {
                if (controller != null && controller.enabled) switch (controller) {
                    case ThrusterController thruster:
                        angle = Vector3.SignedAngle(
                            thruster.GetThrustVector(), 
                            thruster.GetPosition() - center_of_mass,
                            Vector3.up
                        ) + this.rotator.localEulerAngles.z;
                        translation += thruster.GetThrustVector() * 2f;
                        thrust_rotation = thruster.GetThrustVector().magnitude * Mathf.Sin(angle * Mathf.Deg2Rad);
                        rotation += thrust_rotation / 2f;
                        break;
                    case BoosterController booster:
                        angle = Vector3.SignedAngle(
                            booster.GetThrustVector(), 
                            booster.GetPosition() - center_of_mass,
                            Vector3.up
                        ) + this.rotator.localEulerAngles.z;
                        translation += booster.GetThrustVector();
                        thrust_rotation = booster.GetThrustVector().magnitude * Mathf.Sin(angle * Mathf.Deg2Rad);
                        rotation += thrust_rotation * 4f;
                        break;
                }
            }
            rotator.Rotate(new Vector3(0, 0, rotation));
            // average_rotation += rotation / Time.deltaTime;
        }
        else {
            //set ai speed later UnityEngine.Random.Range(-8f, 8f)
            this.transform.Translate(new Vector2(0, random_initial_speed));
            random_rotation += UnityEngine.Random.Range(-.025f, .025f);
            this.transform.Rotate(new Vector3(0, 0, random_rotation));
            random_rotation = random_rotation / 2f;
        }
        if (active_component_count > 0) {
            transform.Translate(new Vector2(translation.x / active_component_count, translation.y / active_component_count));
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
            if (UnityEngine.Random.Range(0f, GetComponent<SpriteRenderer>().size.magnitude * 2f) > explosion_timer) 
            {
                random_initial_speed += Time.deltaTime / (10 * explosion_timer);
                random_rotation += UnityEngine.Random.Range(-.25f, .25f);
                // print (transform.position);
                // print (transform.localPosition);
                GameObject.Find("World").GetComponent<PrefabCache>().PlayExplosion(
                    transform.position + new Vector3(
                        UnityEngine.Random.Range(-GetComponent<SpriteRenderer>().size.x, GetComponent<SpriteRenderer>().size.x),
                        UnityEngine.Random.Range(-GetComponent<SpriteRenderer>().size.x, GetComponent<SpriteRenderer>().size.x),
                        UnityEngine.Random.Range(-GetComponent<SpriteRenderer>().size.y * 2, GetComponent<SpriteRenderer>().size.y * 8)),
                    UnityEngine.Random.Range(GetComponent<SpriteRenderer>().size.magnitude, GetComponent<SpriteRenderer>().size.magnitude * 5));
            }
            if (explosion_timer + 1 > GetComponent<SpriteRenderer>().size.magnitude * 2f + 2) {
                Destroy(this.gameObject);
            } else if (explosion_timer + 1 > GetComponent<SpriteRenderer>().size.magnitude * 1f + 2) {
                if (GetComponent<ParticleSystem>()) 
                    GetComponent<ParticleSystem>().Stop();
                Destroy(GetComponent<BoxCollider2D>());
            }
        }
    }
    // + new Vector3(
                            // UnityEngine.Random.Range(-GetComponent<SpriteRenderer>().size.x, GetComponent<SpriteRenderer>().size.x), 
                            // UnityEngine.Random.Range(-GetComponent<SpriteRenderer>().size.y, GetComponent<SpriteRenderer>().size.y)
                        // )
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
        string output = $"♘position:[{Neaten(this.transform.position.x)},{Neaten(this.transform.position.y)},{Neaten(this.rotator.localEulerAngles.z)}]♘translation:[{Neaten(this.translation.x)},{Neaten(this.translation.y)}]";//,{Neaten(average_rotation)}]";
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
                output += $"♘{entity.name}:[{Neaten(entity.transform.position.x)},{Neaten(entity.transform.position.y)},{Neaten(entity.speed)},{Neaten(entity.transform.localEulerAngles.z)}]";
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