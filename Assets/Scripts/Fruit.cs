using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject explosionVFX;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Destroy(gameObject);
        var explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(explosion, 1f);
        //todo: add score
    }
    
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        //todo: subtract score
    }
}
