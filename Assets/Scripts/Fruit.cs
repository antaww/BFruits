using UnityEngine;

public class Fruit : MonoBehaviour
{
    private const float RotationForce = 200;

    public GameObject explosionVFX;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!Input.GetMouseButton(0)) return; // Security check
        Destroy(gameObject);
        var explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(explosion, 1f);
        //todo: add score
        Score.score++;
    }
    
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        //todo: remove 1 life
    }
    
    void Update()
    {
        transform.Rotate(Vector2.right * (Time.deltaTime * RotationForce));
    }
}
