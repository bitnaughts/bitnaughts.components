using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{    
    public GameObject AsteroidPrefab, StructurePrefab;
    public int num_prefabs, max_prefabs, max_prefab_range, min_prefab_range;
    void Start() 
    {

    }
    public bool active = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject prefab;
        if (active && GameObject.FindGameObjectsWithTag("Structure").Length < max_prefabs)
        {
            if (num_prefabs < max_prefabs)
            {
                float distance = Random.Range(min_prefab_range, max_prefab_range);
                float angle = Random.Range(0f, 2f * Mathf.PI);
                float x = distance * Mathf.Cos(angle);
                float y = distance * Mathf.Sin(angle);
                Vector3 pos = new Vector3(x, y, 0);
                // Vector3 pos = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0);
                prefab = Instantiate(StructurePrefab, pos, Quaternion.identity, this.transform) as GameObject;
                prefab.transform.localPosition = pos;
                prefab.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                prefab.name = "Structure" + num_prefabs;
                num_prefabs++;
            }
        }
    }
    public void Spawn()
    {
        active = true;
        // if (GameObject.FindGameObjectsWithTag("Asteroid").Length < max_prefabs)
        // {
        //     if (num_prefabs < max_prefabs)
        //     {
        //         float distance = Random.Range(min_prefab_range, max_prefab_range);
        //         float angle = Random.Range(0f, 2f * Mathf.PI);
        //         float x = distance * Mathf.Cos(angle);
        //         float y = distance * Mathf.Sin(angle);
        //         Vector3 pos = new Vector3(x, y, 0);
        //         // Vector3 pos = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0);
        //         prefab = Instantiate(AsteroidPrefab, pos, Quaternion.identity, this.transform) as GameObject;
        //         prefab.transform.localPosition = pos;
        //         prefab.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        //         prefab.name = "Asteroid" + num_prefabs;
        //         num_prefabs++;
        //     }
        // }
    }
}
