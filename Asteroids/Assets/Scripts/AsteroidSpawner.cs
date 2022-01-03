using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;

    public float spawnDistance = 15.0f;
    public float trajectoryVariance = 15.0f;
    private int spawnAmountMin = 5;
    private int spawnAmountMax = 10;

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
