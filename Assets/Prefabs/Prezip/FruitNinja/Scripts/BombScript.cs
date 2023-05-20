using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    // Variablen deklarieren
    private float rotationForce = 200;
    public ParticleSystem explosionParticle;

    // Bomben werden durchgehend um die X-Achse rotiert
    void Update()
    {
        transform.Rotate(Vector2.right * Time.deltaTime * rotationForce);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Wenn die Blade eine Bombe trifft, wird die Bombe gelöscht, der Bomben-Particle Effekt instantiiert und die GameOver Funktion aufgerufen
        if(other.tag == "Blade")
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}
