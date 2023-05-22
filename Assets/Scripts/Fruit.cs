using System.Linq.Expressions;
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
        _points = gameObject.CompareTag("Fruit") ? 1 : 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjectDestroyer"))
        {
            Destroy(gameObject);
            _gameManager.RemoveLife();
        }

        if (!other.CompareTag("Player")) return;
        if (!_gameManager.isGameRunning) return;
        if (!Input.GetMouseButton(0)) return; // Security check
        SliceFruit(gameObject, slicedFruit, _rb, fruitJuice, explosionVFX, _gameManager, _points);
    }

    public static void SliceFruit(GameObject fruit, GameObject slicedFruit, Rigidbody rb, GameObject fruitJuice, GameObject explosionVFX, GameManager gameManager, int points)
    {
        fruit.GetComponent<Collider>().enabled = false;
        Destroy(fruit);
        if (fruit.CompareTag("Bonus"))
        {
            Bonus.ApplyBonusEffect(fruit.name, fruit.transform.position);
        }

        if (slicedFruit != null)
        {
            InstantiateSlicedFruit(fruit, slicedFruit, rb);
        }

        InstantiateFruitJuice(fruit, fruitJuice);
        var explosion = Instantiate(explosionVFX, fruit.transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
        gameManager.AddScore(points, false);
    }

    private void Update()
    {
        transform.Rotate(Vector2.right * (Time.deltaTime * RotationForce));
    }

    private static void InstantiateSlicedFruit(GameObject fruit, GameObject slicedFruit, Rigidbody _rb)
    {
        var transformVar = fruit.transform; // Optimization
        var position = transformVar.position; // Optimization

        var instantiatedSlicedFruit = Instantiate(slicedFruit, position, transformVar.rotation);

        var slicedRb = instantiatedSlicedFruit.transform.GetComponentsInChildren<Rigidbody>();

        foreach (var srb in slicedRb)
        {
            srb.AddExplosionForce(Random.Range(20f, 70f), fruit.transform.position, 10, 0f, ForceMode.Impulse);
            srb.velocity = _rb.velocity * 1.2f;
            srb.mass = 8f;
        }

        Destroy(instantiatedSlicedFruit, 5);
    }

    private static void InstantiateFruitJuice(GameObject fruit, GameObject fruitJuice)
    {
        var position = fruit.transform.position; // Optimization
        var instantiatedJuice =
            Instantiate(fruitJuice, new Vector3(position.x, position.y, 0), fruitJuice.transform.rotation);

        Destroy(instantiatedJuice, 5);
    }
}