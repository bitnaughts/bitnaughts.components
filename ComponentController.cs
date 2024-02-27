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
    public void Design() {
        GetComponent<SpriteRenderer>().sprite = inverse;
    }
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
        return (Input.mousePosition.y > 114 && Input.mousePosition.y < Screen.height - 150 && Input.mousePosition.x > 114 && Input.mousePosition.x < Screen.width - 114);
    }
    void OnMouseDown()
    {
        offset = new Vector3(0, 0, 0);
        if (Interactor.OverlayInteractor.gameObject.activeSelf && !this.name.Contains("Printer")) panning = true;
        panOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        // OnMouseUpFx(); if (Interactor.OverlayInteractor.gameObject.activeSelf) Interactor.OverlayInteractor.Resize();
    }
    Vector3 offset = new Vector3(0, 0, 0);

    void OnMouseOver()
    {
        if (panning)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - panOrigin;
            offset += pos * Camera.main.orthographicSize * 2f;
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
                    if (Interactor.OverlayInteractor.OverlayDropdown.options[i].text.Contains(name)) {
                        if (Interactor.OverlayInteractor.OverlayDropdown.value != i) {
                            Interactor.OverlayInteractor.OverlayDropdown.value = i; 
                            Interactor.Sound("OnMouse");
                            // Interactor.SetBinocular("on");
                        }
                    }
                }
                // if (GetType().ToString().Contains("Cannon")) {
                //     Interactor.TargetTutorial();
                // }
                // if (GetType().ToString().Contains("Thruster")) {
                //     Interactor.ThrustTutorial();
                // }
                if (MapScreenPanOverlay != null) MapScreenPanOverlay.gameObject.SetActive(false);
                Interactor.OverlayInteractor.gameObject.SetActive(true);
                Interactor.OverlayInteractor.State = "";
                Interactor.OverlayInteractor.OnDropdownChange(0);//this.name); 
                Interactor.OverlayInteractor.CloseAllOverlays();
                Interactor.OverlayInteractor.OverlayCodeInput.GetComponent<InputField>().text = "";

                if (GetComponent<SpriteRenderer>() == null) Camera.main.orthographicSize = 25; 
                else Camera.main.orthographicSize = Mathf.Sqrt(GetComponent<SpriteRenderer>().size.x * GetComponent<SpriteRenderer>().size.y);
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
    public string ComponentToString() {
        
        return $"♘{this.GetIcon() + this.name}:[{Neaten(transform.localPosition.x)},{Neaten(transform.localPosition.y)},{Neaten(GetComponent<SpriteRenderer>().size.x)},{Neaten(GetComponent<SpriteRenderer>().size.y)},{Neaten(gameObject.transform.localEulerAngles.z)}]";
    }
    public override string ToString() {
        return $"class {GetTypeClass()} : Component\n{{\n pos = new Vector ({Neaten(transform.localPosition.x)}, {Neaten(transform.localPosition.y)});\n size = new Vector ({Neaten(GetComponent<SpriteRenderer>().size.x)}, {Neaten(GetComponent<SpriteRenderer>().size.y)});\n rot = {Neaten(gameObject.transform.localEulerAngles.z)};";
    }

}