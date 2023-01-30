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

public abstract class ComponentController : MonoBehaviour
{
    public Interactor Interactor;
    // private ClassController classController;
    private GameObject MapScreenPanOverlay;
    public Sprite sprite, inverse;
    public float deleteTime = 10f;
    void Start() {
        MapScreenPanOverlay = GameObject.Find("MapScreenPanOverlay");
        Interactor = GameObject.Find("ScreenCanvas").GetComponent<Interactor>();
        Focus();
    }
    public void Design() {
        GetComponent<SpriteRenderer>().sprite = inverse;
    }
    public void Launch() {
        GetComponent<SpriteRenderer>().sprite = sprite;
        // GetComponent<SpriteRenderer>().enabled = true;
    }
    
    public Transform GetTransform()
    {
        return this.transform;
    }

    public abstract void Focus();

    public abstract float Action(float input);
    public abstract void Ping();
    
    public abstract Vector2 GetMinimumSize();

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
    void OnMouseUp()
    {
        print (Interactor.GetClickDuration());
        print (CheckInsideEdge());
        if (Interactor.GetClickDuration() < 0.25) {// && OverlayInteractor.gameObject.activeSelf == false) {//GameObject.Find("Dropdown List") == null && EventSystem.current.currentSelectedGameObject == null) {
            if (CheckInsideEdge()) {
                if (Interactor.OverlayInteractor.gameObject.activeSelf) return;//&& !CheckOutsideOverlay()) return;
                for (int i = 0; i < Interactor.OverlayInteractor.OverlayDropdown.options.Count; i++) {
                    if (Interactor.OverlayInteractor.OverlayDropdown.options[i].text == name) {
                        if (Interactor.OverlayInteractor.OverlayDropdown.value != i) {
                            Interactor.OverlayInteractor.OverlayDropdown.value = i; 
                            Interactor.Sound("OnMouse");
                        }
                    }
                }
                if (GetType().ToString().Contains("Cannon")) {
                    Interactor.TargetTutorial();
                }
                if (GetType().ToString().Contains("Thruster")) {
                    Interactor.ThrustTutorial();
                }
                if (MapScreenPanOverlay != null) MapScreenPanOverlay.gameObject.SetActive(false);
                Interactor.OverlayInteractor.gameObject.SetActive(true);
                Interactor.OverlayInteractor.OnDropdownChange(this.name); 
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
    public string ComponentToString() {
        
        return $"♘{this.GetIcon() + this.name}:[{Neaten(transform.localPosition.x)},{Neaten(transform.localPosition.y)},{Neaten(GetComponent<SpriteRenderer>().size.x)},{Neaten(GetComponent<SpriteRenderer>().size.y)},{Neaten(gameObject.transform.localEulerAngles.z)}]";
    }
    public override string ToString() {
        return $"    pos = new Vector ({Neaten(transform.localPosition.x)}, {Neaten(transform.localPosition.y)});\n    size = new Vector ({Neaten(GetComponent<SpriteRenderer>().size.x)}, {Neaten(GetComponent<SpriteRenderer>().size.y)});\n    rot = {Neaten(gameObject.transform.localEulerAngles.z)};";
    }

}