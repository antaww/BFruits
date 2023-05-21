using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameManager gameManager;

    private Collider _spawnArea;

    public GameObject[] fruitsList;
    public GameObject bombPrefab;
    [Range(0f, 1f)] private float _bombChance = 0.05f;

    private float _minSpawnDelay = 0.25f;
    private float _maxSpawnDelay = 3f;

    private const float MinAngle = -15f;
    private const float MaxAngle = 15f;

    private const float MinForce = 18f;
    private const float MaxForce = 22f;

    private const float Left = -0.2f;
    private const float Right = 0.2f;

    private void Awake()
    {
        _spawnArea = GetComponent<Collider>(); // Get collider

        // Edit spawner stats based on difficulty
        if (gameManager.difficulty <= 1) return;
        _bombChance *= gameManager.difficulty * 2.5f;
        _minSpawnDelay /= gameManager.difficulty * 2f;
        _maxSpawnDelay /= gameManager.difficulty * 2f;
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn()); // Start spawning
    }

    private void OnDisable()
    {
        StopAllCoroutines(); // Stop all coroutines
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);

        while (enabled)
        {
            var prefab = fruitsList[Random.Range(0, fruitsList.Length)];
            var random = Random.value;

            if (random < _bombChance)
            {
                prefab = bombPrefab;
            }

            var position = new Vector3();
            var bounds = _spawnArea.bounds;
            position.x = Random.Range(bounds.min.x, bounds.max.x);
            position.y = Random.Range(_spawnArea.bounds.min.y, bounds.max.y);
            position.z = Random.Range(_spawnArea.bounds.min.z, bounds.max.z);

            var rotation = Quaternion.Euler(0f, 0f, Random.Range(MinAngle, MaxAngle)); // Random rotation

            var fruit = Instantiate(prefab, position, rotation); // Spawn fruit

            var randomDirection = new Vector2(Random.Range(Left, Right), 1).normalized; // Random direction
            var force = Random.Range(MinForce, MaxForce); // Random force
            fruit.GetComponent<Rigidbody>()
                .AddForce(randomDirection * force, ForceMode.Impulse); // Apply force to fruit

            yield return new WaitForSeconds(Random.Range(_minSpawnDelay, _maxSpawnDelay)); // Wait for random time
        }
    }
}