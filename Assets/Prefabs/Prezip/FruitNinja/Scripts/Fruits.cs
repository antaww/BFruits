using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    // Variablen deklarieren
    private GameManager gm;
    public GameObject slicedFruit;
    public GameObject fruitJuice;
    private float rotationForce = 200;
    private Rigidbody rb;
    public int scorePoints;

    // Rigidbody der ganzen Fruit und GameManager in den Variablen 'rb' und 'gm' speichern
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gm = FindObjectOfType<GameManager>();
    }

    // Fruits durchgehend um die X-Achse rotieren
    void Update()
    {
        transform.Rotate(Vector2.right * Time.deltaTime * rotationForce);
    }

    private void InstantiateSlicedFruit()
    {
        // Geslicete Fruits und Fruit Juice instantiaten
        GameObject instantiatedFruit = Instantiate(slicedFruit, transform.position, transform.rotation);
        GameObject instantiatedJuice = Instantiate(fruitJuice, new Vector3(transform.position.x, transform.position.y, 0), fruitJuice.transform.rotation);

        // Rigigdbodies der gesliceten Fruit Elemente 'holen'
        Rigidbody[] slicedRb = instantiatedFruit.transform.GetComponentsInChildren<Rigidbody>();

        // Den Rigidbodies eine explosive Kraft hinzufügen + gleiche Velocity wie die der ganzen Fruit geben (bisschen mehr)
        foreach(Rigidbody srb in slicedRb)
        {
            srb.AddExplosionForce(130f, transform.position, 10);
            srb.velocity = rb.velocity * 1.2f;
        }

        // Geslicete Fruits und Fruit Juice nach 5sek aus dem Spiel löschen
        Destroy(instantiatedFruit, 5);
        Destroy(instantiatedJuice, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Fruit zerstören, geslicete Fruits spawnen und Score erhöhen wenn man eine Fruit mit der Blade getroffen hat
        if(other.tag == "Blade")
        {
            gm.UpdateTheScore(scorePoints);
            Destroy(gameObject);
            InstantiateSlicedFruit();
        }

        // Ein Leben abziehen, wenn eine Fruit verpasst wurde
        if(other.tag == "BottomTrigger")
        {
            gm.UpdateLives();
        }
    }
}
