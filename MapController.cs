using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {
    public Interactor Interactor;
    void Start() {
        Interactor = GameObject.Find("ScreenCanvas").GetComponent<Interactor>();
    }
    bool CheckInsideEdge() {
        return (Input.mousePosition.y > 114 && Input.mousePosition.y < Screen.height - 150 && Input.mousePosition.x > 114 && Input.mousePosition.x < Screen.width - 114);
    }
    void OnMouseUp()
    {
        if (Interactor.GetClickDuration() < 0.25) {// && OverlayInteractor.gameObject.activeSelf == false) {//GameObject.Find("Dropdown List") == null && EventSystem.current.currentSelectedGameObject == null) {
            if (CheckInsideEdge()) {
                // Interactor.Sound("OnMouse");
                // Camera.main.transform.localPosition = new Vector3(transform.position.x, transform.position.z, -100);
                if (Interactor.Stage == "MapInterface") {
                    Interactor.MapInteractor(this.name);
                }
            }
        }
    }
}