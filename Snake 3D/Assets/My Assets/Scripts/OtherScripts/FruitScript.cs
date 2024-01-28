using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    public CollisionS CollisionScript;
    bool PlayerShowTheTarget;


    private void Update()
    {
        Collider[] Colliders = Physics.OverlapSphere(transform.position, 1);
        foreach (Collider NearbyObjects in Colliders)
        {
            // For Boarder And Obstecles
            if (NearbyObjects.CompareTag("Border") || NearbyObjects.CompareTag("Obstecle"))
            {
                transform.position = new Vector3(Random.Range(-13.5f, 13.5f), 0, Random.Range(-13.5f, 13.5f));
            }

            // For Body
            if(NearbyObjects.CompareTag("Player"))
            {
                PlayerShowTheTarget = true;
            }

            if(!PlayerShowTheTarget && NearbyObjects.CompareTag("Body"))
            {
                transform.position = new Vector3(Random.Range(-13.5f, 13.5f), 0, Random.Range(-13.5f, 13.5f));
                PlayerShowTheTarget = false;
            }
            
        }
    }
}
