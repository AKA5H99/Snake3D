using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerS : MonoBehaviour
{
    public Transform[] SpawnPoint;
    public GameObject ItemToSpawn;
    public bool UseSpawnPoints;

    public float MinX, MaxX, MinY, MaxY;

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Without Spawn Points
        if (other.gameObject.tag == "Fruit" && !UseSpawnPoints)
        {
            Instantiate(ItemToSpawn, new Vector3(Random.Range(MinX, MaxX), 0, Random.Range(MinY, MaxY)), Quaternion.identity);
        }

        //With Spawn Points
        if (other.gameObject.tag == "Fruit" && UseSpawnPoints)
        {
            Instantiate(ItemToSpawn, SpawnPoint[Random.Range(0, SpawnPoint.Length)].position, Quaternion.identity);
        }
    }
}
