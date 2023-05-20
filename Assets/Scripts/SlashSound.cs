using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashSound : MonoBehaviour
{
    public AudioSource slashSound;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fruit")
        {
            slashSound.Play();
        }
    }
}
