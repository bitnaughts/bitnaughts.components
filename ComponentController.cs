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
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ComponentController : MonoBehaviour
{
    public Interactor Interactor;
    public StructureController structure;
    // private ClassController classController;
    private GameObject MapScreenPanOverlay;
    public Sprite sprite, inverse;
    public float deleteTime = 10f;
    public bool launched = false;
    Vector3 panOrigin;
    bool panning = false;
    void Start() {
        MapScreenPanOverlay = GameObject.Find("MapScreenPanOverlay");
        Interactor = GameObject.Find("ScreenCanvas").GetComponent<Interactor>();
        Focus();
    }
    public abstract void Design();
    public abstract void Launch();
    
    public Transform GetTransform()
    {
        return this.transform;
    }

    public abstract void Focus();

    public abstract float Action();
    public abstract float Action(float input);
    public abstract void Ping();
    
    public abstract Vector2 GetMinimumSize();

    public abstract float GetCost();

    public void Remove()
    {
        // foreach (var part in parts) Destroy (part.Value);
        Destroy(this.gameObject);
    }
    // bool CheckOutsideOverlay() {
    //     return (Input.mousePosition.x < 3f * Screen.width / 4f - OverlayInteractor.GetComponent<RectTransform>().sizeDelta.x / 2f - 100f || Input.mousePosition.x > 3f * Screen.width / 4f + OverlayInteractor.GetComponent<RectTransform>().sizeDelta.x / 2f + 100f || Input.mousePosition.y > Screen.height / 2f + OverlayInteractor.GetComponent<RectTransform>().sizeDelta.y / 2f + 100f || Input.mousePosition.y < Screen.height / 2f - OverlayInteractor.GetComponent<RectTransform>().sizeDelta.y / 2f - 100f);
    // }
    bool CheckInsideEdge() {
        return (Input.mousePosition.y > 75 && Input.mousePosition.y < Screen.height - 95 && Input.mousePosition.x > 75 && Input.mousePosition.x < Screen.width - 75);
    }
    void OnMouseDown()
    {
        offset = new Vector3(0, 0, 0);
        if (!Interactor.InterpreterPanel.activeSelf && Interactor.OverlayInteractor.gameObject.activeSelf && !this.name.Contains("Printer") && GameObject.Find("Dropdown List") == null) panning = true;
        panOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        // OnMouseUpFx(); if (Interactor.OverlayInteractor.gameObject.activeSelf) Interactor.OverlayInteractor.Resize();
    }
    Vector3 offset = new Vector3(0, 0, 0);

    void OnMouseOver()
    {
        if (panning)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - panOrigin;
            offset += pos * Camera.main.orthographicSize * 2.25f;
            print (offset.x + " " + offset.y);
            panOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if (offset.x > 0.25f) {
                offset.x -= 0.25f;
                transform.localPosition = new Vector3(transform.localPosition.x + 0.25f, transform.localPosition.y);
                Interactor.OverlayInteractor.Resize();
            }
            if (offset.x < -0.25f) {
                offset.x += 0.25f;
                transform.localPosition = new Vector3(transform.localPosition.x - 0.25f, transform.localPosition.y);
                Interactor.OverlayInteractor.Resize();
            }
            if (offset.y > 0.25f) {
                offset.y -= 0.25f;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.25f);
                Interactor.OverlayInteractor.Resize();
            }
            if (offset.y < -0.25f) {
                offset.y += 0.25f;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.25f);
                Interactor.OverlayInteractor.Resize();
            }
        
        }
        // OnMouseUpFx();
    }

    void OnMouseUp()
    {
        OnMouseUpFx();
    }
    public void OnMouseUpFx() 
    {
        panning = false;
        if (Interactor.GetClickDuration() < 0.25) {// && OverlayInteractor.gameObject.activeSelf == false) {//GameObject.Find("Dropdown List") == null && EventSystem.current.currentSelectedGameObject == null) {
            if (CheckInsideEdge()) {
                if (Interactor.printing_stage != "Edit" && Interactor.OverlayInteractor.gameObject.activeSelf) return;//&& !CheckOutsideOverlay()) return;
                for (int i = 0; i < Interactor.OverlayInteractor.OverlayDropdown.options.Count; i++) {
                            //print ("s " + Interactor.OverlayInteractor.OverlayDropdown.options[i].text);
                    if (Interactor.OverlayInteractor.OverlayDropdown.options[i].text.Contains(name)) {
                        if (Interactor.OverlayInteractor.OverlayDropdown.value != i) {
                            Interactor.OverlayInteractor.OverlayDropdown.value = i; 
                            Interactor.Sound("OnMouse");
                            // Interactor.SetBinocular("on");
                        }
                        Interactor.OverlayInteractor.OverlayDropdown.transform.Find("OverlayDropdownLabel").GetComponent<Text>().text = Interactor.OverlayInteractor.OverlayDropdown.options[i].text;

                            //print ("found " + name);
                    }
                }
                
                            print ("didn't found " + name);
                // if (GetType().ToString().Contains("Cannon")) {
                //     Interactor.TargetTutorial();
                // }
                // if (GetType().ToString().Contains("Thruster")) {
                //     Interactor.ThrustTutorial();
                // }
                if (MapScreenPanOverlay != null) MapScreenPanOverlay.gameObject.SetActive(false);
                Interactor.OverlayInteractor.gameObject.SetActive(true);
                GameObject.Find("OverlayDelete").transform.GetChild(0).GetComponent<Text>().color = new Color(1f, 1f, 0f, 1f);

                Interactor.OverlayInteractor.State = "";
                Interactor.OverlayInteractor.OnDropdownChange(0);//this.name); 
                Interactor.OverlayInteractor.CloseAllOverlays();
                Interactor.OverlayInteractor.OverlayCodeInput.GetComponent<InputField>().text = "";
                Interactor.volume_slider.SetActive(true);
                if (GetComponent<SpriteRenderer>() == null) Camera.main.orthographicSize = 25; 
                else Camera.main.orthographicSize = Mathf.Sqrt(GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y); // if (GetTypeClass() == "Printer") 
                Interactor.OverlayInteractor.Resize();
            }
        }
    } 
    public string GetTypeClass() {
        return GetType().ToString().Replace("Controller", "");
    }
    public abstract string GetIcon();
    public static string Neaten(float input) {
        return input.ToString("0.00").TrimEnd('0').TrimEnd('.');
    }
    public float GetLocalRotation() {
        var gimbal = this.transform.parent.gameObject.GetComponentInParent<GimbalController>();
        if (gimbal == null) return this.transform.localEulerAngles.z;
        return gimbal.Action() + this.transform.localEulerAngles.z;
    }
    public virtual string ComponentToString() {
        
        return $"♘{this.GetIcon() + this.name}:[{Neaten(transform.localPosition.x)},{Neaten(transform.localPosition.y)},{Neaten(GetComponent<SpriteRenderer>().size.x)},{Neaten(GetComponent<SpriteRenderer>().size.y)},{Neaten(gameObject.transform.localEulerAngles.z)}]";
    }
    public override string ToString() {
        return $"class {GetTypeClass()} : Component\n{{\n pos = new Vector ({Neaten(transform.localPosition.x)}, {Neaten(transform.localPosition.y)});\n size = new Vector ({Neaten(GetComponent<SpriteRenderer>().size.x)}, {Neaten(GetComponent<SpriteRenderer>().size.y)});\n rot = {Neaten(gameObject.transform.localEulerAngles.z)};";
    }

}