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
    public OverlayInteractor OverlayInteractor;
    public Interactor Interactor;
    private ClassController classController;
    public Sprite sprite, inverse;
    void Start()
    {
        OverlayInteractor = GameObject.Find("OverlayBorder").GetComponent<OverlayInteractor>();
        Interactor = GameObject.Find("Content").GetComponent<Interactor>();
        Action(0);
        Focus();
    }
    public void Design() {
        GetComponent<SpriteRenderer>().sprite = inverse;
    }
    public void Launch() {
        GetComponent<SpriteRenderer>().sprite = sprite;
        GetComponent<SpriteRenderer>().enabled = true;
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

    void OnMouseOver() 
    {
    }
    void OnMouseExit() 
    {
    }
    bool CheckOutsideOverlay() {
        return (Input.mousePosition.x < 3f * Screen.width / 4f - OverlayInteractor.GetComponent<RectTransform>().sizeDelta.x / 2f || Input.mousePosition.x > 3f * Screen.width / 4f + OverlayInteractor.GetComponent<RectTransform>().sizeDelta.x / 2f || Input.mousePosition.y > Screen.height / 2f + OverlayInteractor.GetComponent<RectTransform>().sizeDelta.y / 2f || Input.mousePosition.y < Screen.height / 2f - OverlayInteractor.GetComponent<RectTransform>().sizeDelta.y / 2f);
    }
    bool CheckInsideEdge() {
        return (Input.mousePosition.y > 60 && Input.mousePosition.y < Screen.height - 60 && Input.mousePosition.x > Screen.width / 2 && Input.mousePosition.x < Screen.width - 60);
    }
    void OnMouseUp()
    {
        if (Interactor.GetClickDuration() < 0.25) {// && OverlayInteractor.gameObject.activeSelf == false) {//GameObject.Find("Dropdown List") == null && EventSystem.current.currentSelectedGameObject == null) {
            if (CheckInsideEdge()) {
                if (OverlayInteractor.gameObject.activeSelf && !CheckOutsideOverlay()) return;
                for (int i = 0; i < OverlayInteractor.OverlayDropdown.options.Count; i++) {
                    if (OverlayInteractor.OverlayDropdown.options[i].text == name) {
                        if (OverlayInteractor.OverlayDropdown.value != i) {
                            OverlayInteractor.OverlayDropdown.value = i; 
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
                OverlayInteractor.gameObject.SetActive(true);
                OverlayInteractor.OnDropdownChange(); 
            }
        }
    }
    public string GetTypeClass() {
        return GetType().ToString().Replace("Controller", "");
    }
    public abstract string GetIcon();
    public string ComponentToString() {
        
        return $"\n{'"' + this.name + '"'}: [{transform.localPosition.x}, {transform.localPosition.y}, {GetComponent<SpriteRenderer>().size.x}, {GetComponent<SpriteRenderer>().size.y}, {gameObject.transform.localEulerAngles.z.ToString("0.00")}],";
    }

}