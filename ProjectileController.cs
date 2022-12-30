using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    public float acceleration = 0f;
    public float speed = 1f;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 30f);
        Destroy(GetComponent<ParticleSystem>(), 15f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        speed += Time.deltaTime * acceleration;
        transform.Translate(new Vector3(0f, speed * Time.deltaTime));
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, speed * 2f * Time.deltaTime * (this.gameObject.layer + 1) / 2 )) {
            if (this.gameObject.layer == 0 && hit.collider.gameObject.layer == 3 || this.gameObject.layer == 3 && hit.collider.gameObject.layer == 0)  {
                this.transform.parent.GetComponent<PrefabCache>().PlayExplosion(hit.collider.gameObject.transform.position, hit.collider.gameObject.GetComponent<SpriteRenderer>().size.magnitude);
                hit.collider.gameObject.GetComponent<StructureController>().Hit(damage);
                Destroy(this.gameObject);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
            } 
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 1000, Color.white);
        }
    }
}
