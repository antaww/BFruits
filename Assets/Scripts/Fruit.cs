using UnityEngine;

public class Fruit : MonoBehaviour
{
    public const float RotationForce = 200;

    public GameObject fruitJuice;
    public GameObject slicedFruit;
    public GameObject explosionVFX;
    
    private GameManager _gameManager;

    private Rigidbody _rb;

    private int _points;

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rb = GetComponent<Rigidbody>();
        _points = gameObject.CompareTag("Fruit") ? 1 : 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjectDestroyer"))
        {
            Destroy(gameObject);
            _gameManager.RemoveLife();
        }

        if (!other.CompareTag("Player")) return;
        if (!Input.GetMouseButton(0)) return; // Security check
        Destroy(gameObject);
        if (slicedFruit != null)
        {
            InstantiateSlicedFruit();
        }
        InstantiateFruitJuice();
        var explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(explosion, 1f);
        _gameManager.AddScore(_points, false);
    }

    private void Update()
    {
        transform.Rotate(Vector2.right * (Time.deltaTime * RotationForce));
    }

    private void InstantiateSlicedFruit()
    {
        var transformVar = transform; // Optimization
        var position = transformVar.position; // Optimization

        var instantiatedSlicedFruit = Instantiate(slicedFruit, position, transformVar.rotation);

        var slicedRb = instantiatedSlicedFruit.transform.GetComponentsInChildren<Rigidbody>();

        foreach (var srb in slicedRb)
        {
            srb.AddExplosionForce(Random.Range(20f, 70f), transform.position, 10, 0f, ForceMode.Impulse);
            srb.velocity = _rb.velocity * 1.2f;
            srb.mass = 8f;
        }

        Destroy(instantiatedSlicedFruit, 5);
    }
    
    private void InstantiateFruitJuice()
    {
        var position = transform.position; // Optimization
        var instantiatedJuice =
            Instantiate(fruitJuice, new Vector3(position.x, position.y, 0), fruitJuice.transform.rotation);
        
        Destroy(instantiatedJuice, 5);
    }
}