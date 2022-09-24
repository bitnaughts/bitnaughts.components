using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    public float acceleration = 0f;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 30f);
        Destroy(GetComponent<ParticleSystem>(), 15f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed += Time.deltaTime * acceleration;
        transform.Translate(new Vector3(0f, speed * Time.deltaTime));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, speed * Time.deltaTime * (this.gameObject.layer + 1) / 2 );
        if (hit.collider != null)
        {
            if (this.gameObject.layer == 0 && hit.collider.gameObject.layer == 3 || this.gameObject.layer == 3 && hit.collider.gameObject.layer == 0)  {
                this.transform.parent.GetComponent<PrefabCache>().PlayExplosion(hit.collider.gameObject.transform.position);
                Destroy(hit.collider.gameObject);
                Destroy(this.gameObject);
                // this.SetActive(false); //TODO: Enable projectile caching (see PrefabCache)
            }
        }
        // if (GetComponent<AudioSource>() != null) {
        //     GetComponent<AudioSource>().volume -= .0001f;
        // }
    }
}
