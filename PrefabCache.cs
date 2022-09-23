using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCache : MonoBehaviour
{
    public List<GameObject> explosions;
    public GameObject explosion_prefab;
    public int explosion_cache_size = 50;
    // Start is called before the first frame update
    void Start()
    {
        explosions = new List<GameObject>();
        for (int i = 0; i < explosion_cache_size; i++) {
            explosions.Add(Instantiate(explosion_prefab, this.transform) as GameObject);
        } 
    }
    public void PlayExplosion(Vector2 position) {
        for (int i = 0; i < explosion_cache_size; i++) {
            if (explosions[i].activeSelf == false) {
                explosions[i].SetActive(true);
                explosions[i].transform.position = position;
                explosions[i].GetComponent<ExplosionController>().timer = 2;
                return;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
