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
    Dictionary<string, ComponentController> components;
    protected Vector3 center_of_mass;
    protected int child_count;
    protected Transform rotator;       
    protected UnityEngine.Color debug_color;
    RaycastHit hit;
    void Start()
    {
        debug_color = new UnityEngine.Color(UnityEngine.Random.Range(0.0f,1.0f),UnityEngine.Random.Range(0.0f,1.0f),UnityEngine.Random.Range(0.0f,1.0f));
        // print ("Structure Starting!");
        components = new Dictionary<string, ComponentController>();
        foreach (var controller in GetComponentsInChildren<ComponentController>()) 
        {
            components.Add(controller.name, controller);
        }

        rotator = transform.Find("Rotator");
        child_count = rotator.childCount;

        // foreach (var controller in components) {
        //     controller.Init(components);
        // }
    }

    float gimbal_test = 0f;
    float gimbal_step = 10f;
    void FixedUpdate()
    {
        if (child_count != rotator.childCount) Start();

        center_of_mass = Vector3.zero;
        float active_component_count = 0;
        foreach (var controller in components.Values) {
           if (controller != null && controller.enabled) {
                center_of_mass += new Vector3(controller.GetTransform().position.x, 0, controller.GetTransform().position.z);
                active_component_count++;
                switch (controller) {
                    case ProcessorController processor:
                        processor.Action(components);
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
        Vector3 translation = new Vector3(0, 0, 0);

        foreach (var controller in components.Values)
        {
            if (controller != null && controller.enabled) switch (controller) {
                case ThrusterController thruster:
                    // Debug.DrawLine(thruster.GetPosition(), thruster.GetPosition() + thruster.GetThrustVector(), Color.green, debug_duration, false);
                    // Debug.DrawLine(thruster.GetPosition(), center_of_mass, Color.green, debug_duration, false);
                    translation -= thruster.GetThrustVector();
                    float thrust_rotation = 15 * thruster.GetThrustVector().magnitude * Mathf.Sin(
                        Vector3.SignedAngle(
                            thruster.GetThrustVector(), 
                            thruster.GetPosition() - center_of_mass,
                            Vector3.up
                        ) * Mathf.Deg2Rad
                    );
                    rotation += thrust_rotation;
                    break;
            }
        }
        if (active_component_count > 0) {
            rotator.Rotate(new Vector3(0, rotation * 1.5f / active_component_count, 0));
            transform.Translate(translation * 1.5f / active_component_count);
        }
        if (transform.position.x > 420 || transform.position.x < -20 || transform.position.z > 420 || transform.position.z < -20) Destroy(this.gameObject);
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