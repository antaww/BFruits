using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    // Variablen deklarieren
    private Rigidbody rb;
    private SphereCollider sc;
    private TrailRenderer tr;
    private GameManager gm;

    // Variablen zuweisen
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sc = GetComponent<SphereCollider>();
        tr = GetComponent<TrailRenderer>();
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if(gm.gameIsOver == false)
        {
            // Wenn die Linke Maus-Taste geklickt wird, wird der Collider aktiviert damit die Fruits zerschnitten werden können
            // Außerdem wird der TrailRenderer aktiviert, damit man die Blade sehen kann
            if (Input.GetMouseButtonDown(0))
            {
                tr.enabled = true;
                sc.enabled = true;
            }

            // Wenn die Linke Maus-Taste losgelassen wird, wird der Collider deaktiviert damit man keine Fruits mehr zerschneiden kann
            // und der TrailRenderer wird ebenfalls deaktiviert
            if (Input.GetMouseButtonUp(0))
            {
                tr.enabled = false;
                sc.enabled = false;
            }

            // BladeFollowMouse() wird in der Update Funktion aufgerufen, damit unsere Blade durchgehend der Maus folgt
            BladeFollowMouse();
        }
    }

    // Das Objekt Blade folgt unserer Maus
    private void BladeFollowMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 8;
        rb.position = Camera.main.ScreenToWorldPoint(mousePos);
    }
}
