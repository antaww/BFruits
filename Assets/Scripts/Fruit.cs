using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject explosionVFX;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameObject explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
            Destroy(explosion, 1f);
            //todo: add score
        }
    }
}
