using UnityEngine;
using System.Collections.Generic;

public class BlockSpawner : MonoBehaviour
{
    public Transform[] spawnPoints; // The predefined spawn points
    public GameObject blockPrefab;  // The block prefab to instantiate
    public float timeBetweenWaves = 1f; // Time interval between waves

    private float timeToSpawn = 2f; // Time for the next spawn

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timeToSpawn)
        {
            SpawnBlocks();
            timeToSpawn = Time.time + timeBetweenWaves;
        }
    }

    void SpawnBlocks()
    {
        int blocksToSpawn = Random.Range(2, 4); // Randomly choose between 2 or 3 blocks

        // Create a list of indices and shuffle it
        List<int> randomIndices = new List<int>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            randomIndices.Add(i);
        }
        Shuffle(randomIndices);

        // Spawn the required number of blocks at random spawn points
        for (int i = 0; i < blocksToSpawn; i++)
        {
            Instantiate(blockPrefab, spawnPoints[randomIndices[i]].position, Quaternion.identity);
        }
    }

    // Helper function to shuffle a list
    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(0, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
