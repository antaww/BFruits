using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameManager gameManager;

    private Collider _spawnArea;

    public GameObject[] fruitsList;
    public GameObject bombPrefab;
    [Range(0f, 1f)] public float bombChance = 0.01f;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;
    
    public float left = -0.2f;
    public float right = 0.2f;

    private void Awake()
    {
        _spawnArea = GetComponent<Collider>(); // Get collider

        // Edit spawner stats based on difficulty
        if (gameManager.difficulty <= 1) return;
        bombChance *= gameManager.difficulty;
        minSpawnDelay /= gameManager.difficulty;
        maxSpawnDelay /= gameManager.difficulty;
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

            if (random < bombChance)
            {
                prefab = bombPrefab;
            }

            var position = new Vector3();
            var bounds = _spawnArea.bounds;
            position.x = Random.Range(bounds.min.x, bounds.max.x);
            position.y = Random.Range(_spawnArea.bounds.min.y, bounds.max.y);
            position.z = Random.Range(_spawnArea.bounds.min.z, bounds.max.z);

            var rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle)); // Random rotation

            var fruit = Instantiate(prefab, position, rotation); // Spawn fruit
            
            var randomDirection = new Vector2(Random.Range(left, right), 1).normalized; // Random direction
            var force = Random.Range(minForce, maxForce); // Random force
            fruit.GetComponent<Rigidbody>()
                .AddForce(randomDirection * force, ForceMode.Impulse); // Apply force to fruit

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay)); // Wait for random time
        }
    }
}