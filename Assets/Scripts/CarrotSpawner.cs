using UnityEngine;
using System.Collections.Generic;

public class CarrotSpawner : MonoBehaviour
{
    public GameObject carrotPrefab; // Reference to the carrot prefab
    public Transform[] spawnPoints; // Array of spawn points
    public int numberOfCarrotsToSpawn = 2; // Number of carrots to spawn
    private List<Transform> availableSpawnPoints = new List<Transform>();

    void Start()
    {
        // Populate the list of available spawn points
        availableSpawnPoints.AddRange(spawnPoints);

        // Spawn the specified number of carrots
        SpawnCarrots(numberOfCarrotsToSpawn);
    }

    void SpawnCarrots(int numberOfCarrots)
    {
        if (numberOfCarrots > availableSpawnPoints.Count)
        {
            Debug.LogWarning("Not enough available spawn points for the desired number of carrots.");
            numberOfCarrots = availableSpawnPoints.Count;
        }

        for (int i = 0; i < numberOfCarrots; i++)
        {
            // Randomly select an available spawn point
            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            Transform spawnPoint = availableSpawnPoints[randomIndex];

            // Spawn a carrot at the selected spawn point
            Instantiate(carrotPrefab, spawnPoint.position, Quaternion.identity);

            // Remove the used spawn point from the available list
            availableSpawnPoints.RemoveAt(randomIndex);
        }
    }
}