using UnityEngine;

public class Bomb : MonoBehaviour
{
    private const float RotationForce = 200;
    public ParticleSystem explosionParticle;
    
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        transform.Rotate(Vector2.right * (Time.deltaTime * RotationForce));
    }
    
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        //todo: add score
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!Input.GetMouseButton(0)) return; // Security check
        Destroy(gameObject);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        _mainCamera.GetComponent<Shake>().isShaking = true;
        //todo: remove 1 life
    }
}
