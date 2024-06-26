using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float acceleration;
    public float speed;
    float lifespan = 20;
    public int damage;
    public BulkheadController bulkhead;
    // Start is called before the first frame update
    void Start()
    {
        if (this.name.Contains("Asteroid")) {

        } else {
    
        }
    } 
    //Upon collision with another GameObject, this GameObject will reverse direction
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.name.Contains("Asteroid") || other.gameObject.name.Contains("Scanner")) {
            
    //     }
    //     else {
    //             GameObject.Find("ScreenCanvas").GetComponent<Interactor>().Sound("TryAgain");
    //             print ("triggered " + other.gameObject.name);
    //     GameObject.Find("World").GetComponent<PrefabCache>().PlayExplosion(other.gameObject.transform.position + new Vector3(0, 10, 10), other.gameObject.GetComponent<SpriteRenderer>().size.magnitude * 5f, "Torpedo");
    //     Destroy(other.gameObject);
    //     Destroy(this.gameObject);
    //     }
    // }

    // Update is called once per frame
    void FixedUpdate()
    {
        lifespan -= Time.deltaTime;
        if (lifespan < 10 && GetComponent<ParticleSystem>() != null) 
        {
            Destroy(GetComponent<ParticleSystem>());
        }
        if (lifespan < 0)
        {
            bulkhead.Action(1000);
            Destroy(this.gameObject);
        }
        else {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 1, Color.red);
            speed += Time.deltaTime * acceleration;
            transform.Translate(new Vector3(0f, speed * Time.deltaTime));
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.up, out hit, 5 + speed * 2f * Time.deltaTime * (this.gameObject.layer + 1) / 2 )) {
                if (hit.collider.gameObject.name.Contains("Asteroid") || hit.collider.gameObject.layer == 3) {
                    print ("hit asteroid");
                    bulkhead.Action(hit.collider.gameObject.transform.parent.GetComponent<AsteroidController>().Hit(hit.collider.gameObject.name));
                    bulkhead.Action(1000);
                    Destroy(this.gameObject);
                    GameObject.Find("World").GetComponent<PrefabCache>().PlayExplosion(hit.collider.gameObject.transform.position + new Vector3(0, 10, 10), hit.collider.gameObject.GetComponent<SpriteRenderer>().size.magnitude * 5f, "Torpedo");

                } else if (this.name.Contains("↥")) {
                    // print ("hit " + hit.collider.gameObject.name);
                    GameObject.Find("World").GetComponent<PrefabCache>().PlayExplosion(hit.collider.gameObject.transform.position + new Vector3(0, 10, 10), hit.collider.gameObject.GetComponent<SpriteRenderer>().size.magnitude * 5f, "Torpedo");
                    Destroy(hit.collider.gameObject);//.GetComponent<StructureController>().Hit(damage);
                    // GameObject.Find("ScreenCanvas").GetComponent<Interactor>().HitSfx();
                    
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
                    Destroy(this.gameObject);
                }
                // if (this.gameObject.layer == 0 && hit.collider.gameObject.layer == 3 || this.gameObject.layer == 3 && hit.collider.gameObject.layer == 0)  {
                
                // } 
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * speed * 2f * Time.deltaTime * (this.gameObject.layer + 1) / 2, Color.white);
            } 
        }
    }
}
