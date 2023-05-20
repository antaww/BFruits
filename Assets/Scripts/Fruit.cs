using UnityEngine;

public class Fruit : MonoBehaviour
{
    private const float RotationForce = 200;

    public GameObject fruitJuice;
    public GameObject explosionVFX;
    private GameManager _gameManager;
    
    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!Input.GetMouseButton(0)) return; // Security check
        Destroy(gameObject);
        InstantiateSlicedFruit();
        var explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(explosion, 1f);
        _gameManager.AddScore(1);
    }
    
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        //todo: remove 1 life
    }

    private void Update()
    {
        transform.Rotate(Vector2.right * (Time.deltaTime * RotationForce));
    }
    
    private void InstantiateSlicedFruit()
     {
         // GameObject instantiatedFruit = Instantiate(slicedFruit, transform.position, transform.rotation);
         var instantiatedJuice = Instantiate(fruitJuice, new Vector3(transform.position.x, transform.position.y, 0), fruitJuice.transform.rotation);

         // Rigidbody[] slicedRb = instantiatedFruit.transform.GetComponentsInChildren<Rigidbody>();

         // foreach(Rigidbody srb in slicedRb)
         // {
         //     srb.AddExplosionForce(130f, transform.position, 10);
         //     srb.velocity = rb.velocity * 1.2f;
         // }

         // Destroy(instantiatedFruit, 5);
         Destroy(instantiatedJuice, 5);
     }
}
