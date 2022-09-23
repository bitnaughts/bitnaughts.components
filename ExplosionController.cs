using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float timer = 0;
    void FixedUpdate()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
        } else {
            this.gameObject.SetActive(false);
        }
    }
}
