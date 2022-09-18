using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float Acceleration = 0f;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed += Time.deltaTime * Acceleration;
        transform.Translate(new Vector3(0f, speed));
        // if (transform.position.x > 410 || transform.position.x < -10 || transform.position.z > 410 || transform.position.z < -10) Destroy(this.gameObject);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f))
        {
            if (hit.collider.gameObject.layer == 9) Destroy(hit.collider.gameObject);
            Destroy(this.gameObject);
        }
        if (GetComponent<AudioSource>() != null) {
            GetComponent<AudioSource>().volume -= .0001f;
        }
    }
}
