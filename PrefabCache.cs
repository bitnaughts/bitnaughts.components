using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCache : MonoBehaviour
{
    public List<GameObject> explosions;
    public GameObject explosion_prefab;
    public AudioClip[] explosion_clips;
    public int explosion_cache_size = 50;
    // Start is called before the first frame update
    void Start()
    {
        explosions = new List<GameObject>();
        for (int i = 0; i < explosion_cache_size; i++) {
            explosions.Add(Instantiate(explosion_prefab, this.transform) as GameObject);
        } 
    }
    public void PlayExplosion(Vector3 position, float size, string type) {
        for (int i = 0; i < explosion_cache_size; i++) {
            if (explosions[i].activeSelf == false) {
                explosions[i].SetActive(true);
                explosions[i].transform.position = position;
                explosions[i].GetComponent<ExplosionController>().Explode(size, type);
                // explosions[i].GetComponent<AudioSource>().clip = explosion_clips[i % explosion_clips.Length];
                // explosions[i].GetComponent<AudioSource>().Play();
                var main = explosions[i].GetComponent<ParticleSystem>().main;
                main.startSize = new ParticleSystem.MinMaxCurve(size * 1, size * 2);
                return;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
