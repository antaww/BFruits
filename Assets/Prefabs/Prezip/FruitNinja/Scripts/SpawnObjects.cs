using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    // Variablen deklarieren
    public GameObject[] objects;
    public float left;
    public float right;

    // Die Coroutine SpawnRandomObject() wird in der Start Funktion aufgerufen, damit das Spiel startet
    void Start()
    {
        StartCoroutine(SpawnRandomObject());
    }

    private IEnumerator SpawnRandomObject()
    {
        // Bevor die ersten Fruits spawnen eine Sekunde warten
        yield return new WaitForSeconds(1);

        // Solange das Spiel noch nicht GameOver ist, nach einer zuf�lligen Zeit eine neue Fruit (oder Bombe) spawnen
        while (FindObjectOfType<GameManager>().gameIsOver == false)
        {
            InstantiateRandomObject();
            yield return new WaitForSeconds(RandomRepeatrate());
        }
    }

    private void InstantiateRandomObject()
    {
        // Zuf�lliger Integer zwischen 0 und 5
        int objectIndex = Random.Range(0, objects.Length);

        // Spawnt eine zuf�llige Fruit (oder Bombe) mit 'objectIndex' und speichert sie in 'obj'
        GameObject obj = Instantiate(objects[objectIndex], transform.position, objects[objectIndex].transform.rotation);

        // Dem gespawnten Objekt wird eine Kraft hinzugef�gt (damit sie ins Spiel reinfliegt)
        obj.GetComponent<Rigidbody>().AddForce(RandomVector() * RandomForce(), ForceMode.Impulse);

        // Das Objekt bekommt eine zuf�llige Rotation
        obj.transform.rotation = Random.rotation;
    }

    // Berechnet eine zuf�llige Kraft, wodurch die Objekte unterschiedlich hoch fliegen
    private float RandomForce()
    {
        float force = Random.Range(14f, 16f);
        return force;
    }

    // Berechnet eine zuf�llige Wiederholungsrate, damit die Objekte nicht immer im gleichen Abstand spawnen
    private float RandomRepeatrate()
    {
        float repeatrate = Random.Range(0.5f, 3f);
        return repeatrate;
    }

    // Berechnet einen zuf�lligen Vektor, der angibt wie weit die Fruits (Bomben) nach links oder rechts fliegen sollen
    private Vector2 RandomVector()
    {
        Vector2 moveDirection = new Vector2(Random.Range(left, right), 1).normalized;
        return moveDirection;
    }
}
