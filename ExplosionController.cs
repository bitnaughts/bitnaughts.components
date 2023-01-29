using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public AudioClip TorpedoHitSFX1, TorpedoHitSFX2;
    public AudioClip SmallShipHitSFX1, SmallShipHitSFX2;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float timer = 0;
    public void Explode(float time, string type) {
        timer = time;
        float random_chance = UnityEngine.Random.Range(0f, 1);
        if (!GetComponent<AudioSource>().isPlaying) {            
            switch (type) {
                case "Torpedo":
                    if (random_chance > 0.5f) 
                    {
                        GetComponent<AudioSource>().clip = TorpedoHitSFX1;
                    }
                    else
                    {
                        GetComponent<AudioSource>().clip = TorpedoHitSFX2;
                    }
                    break;
                case "Small Ship":
                    if (random_chance < 0.05f) 
                    {
                        GetComponent<AudioSource>().clip = SmallShipHitSFX1;
                    }
                    else if (random_chance < 0.1f) 
                    {
                        GetComponent<AudioSource>().clip = SmallShipHitSFX2;
                    }
                    break;
            }
            GetComponent<AudioSource>().volume = .1f;
            GetComponent<AudioSource>().Play();
        }
    }
    void FixedUpdate()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
        } else {
            this.gameObject.SetActive(false);
        }
    }
}
