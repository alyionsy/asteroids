using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;

    public float trajectoryVariance = 15.0f;
    public int spawnAmountMin = 5;
    public int spawnAmountMax = 10;
    private readonly float spawnDistance = 15.0f;

    private void Start()
    {
        Spawn(Random.Range(spawnAmountMin, spawnAmountMax));
    }

    public void Spawn(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPoint = transform.position + spawnDirection;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            Asteroid asteroid = Instantiate(asteroidPrefab, spawnPoint, rotation);
            asteroid.SetTrajectory(rotation * - spawnDirection);
        }
    }
}
