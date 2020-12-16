using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {
    
    Camera camera;
    GameObject focus;
    void Start() {
        camera = this.GetComponent<Camera>();
        focus = GameObject.Find("Ship");
    }

    // Visualized via marching cubes... On update, update cubes to any granular damage
    void Update() {
        camera.orthographicSize += Input.mouseScrollDelta.y;

        if (focus.transform.position.x > 410 || focus.transform.position.x < -10 || focus.transform.position.z > 410 || focus.transform.position.z < -10) ReloadScene();
    }

    public void ZoomIn() {
        
        if (camera.orthographicSize > 20) camera.orthographicSize -= 20;
    }
    public void ZoomOut() {
        camera.orthographicSize += 20;
    }
    public void ReloadScene() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}