using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider _spawnArea;

    public GameObject[] fruitsList;
    public GameObject bombPrefab;
    [Range(0f, 1f)] public float bombChance = 0.05f;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;

    public float maxLifetime = 5f;

    private void Awake()
    {
        _spawnArea = GetComponent<Collider>(); // Get collider
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
            GameObject prefab = fruitsList[Random.Range(0, fruitsList.Length)];

            if (Random.value < bombChance) {
                prefab = bombPrefab;
            }

            Vector3 position = new Vector3();
            position.x = Random.Range(_spawnArea.bounds.min.x, _spawnArea.bounds.max.x);
            position.y = Random.Range(_spawnArea.bounds.min.y, _spawnArea.bounds.max.y);
            position.z = Random.Range(_spawnArea.bounds.min.z, _spawnArea.bounds.max.z);

            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle)); // Random rotation

            GameObject fruit = Instantiate(prefab, position, rotation); // Spawn fruit
            Destroy(fruit, maxLifetime); // Destroy fruit after maxLifetime
            
            float randomSize = Random.Range(200f, 250f); // Random size
            fruit.transform.localScale = new Vector3(randomSize, randomSize, randomSize); // Apply size to fruit

            float force = Random.Range(minForce, maxForce); // Random force
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse); // Apply force to fruit

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay)); // Wait for random time
        }
    }

}