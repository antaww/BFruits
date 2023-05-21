using UnityEngine;

public class Bomb : MonoBehaviour
{
    private const float RotationForce = 200;
    public ParticleSystem explosionParticle;
    
    private Camera _mainCamera;
    private GameManager _gameManager;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        transform.Rotate(Vector2.right * (Time.deltaTime * RotationForce));
    }
    
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        _gameManager.AddScore(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!Input.GetMouseButton(0)) return; // Security check
        Destroy(gameObject);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        _mainCamera.GetComponent<Shake>().isShaking = true;
        _gameManager.PlayExplosionSound();
        //todo: game over
    }
}
