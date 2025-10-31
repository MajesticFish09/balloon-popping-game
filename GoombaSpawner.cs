using UnityEngine;
using System.Collections;

public class GoombaSpawner : MonoBehaviour
{
    public GameObject goombaPrefab;
    public float spawnRate = 3f; // Time between spawns
    public float spawnHeight = 3f; // Y position range

    void Start()
    {
        StartCoroutine(SpawnGoombas());
    }

    IEnumerator SpawnGoombas()
    {
        while (true)
        {
            // Wait for next spawn
            yield return new WaitForSeconds(spawnRate);

            // Spawn Goomba at right side of screen
            Vector2 spawnPos = new Vector2(10f, Random.Range(-spawnHeight, spawnHeight));

            // Create the Goomba
            Instantiate(goombaPrefab, spawnPos, Quaternion.identity);
        }
    }
}