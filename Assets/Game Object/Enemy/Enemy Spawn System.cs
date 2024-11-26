using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign the enemy prefab in the inspector
    public Transform player; // Assign the player's transform in the inspector
    public float playerAvoidanceRadius = 5f; // Minimum radius from the player for spawning
    public int maxEnemies = 10; // Maximum number of enemies allowed to spawn
    public BoxCollider2D spawnArea; // The BoxCollider2D that defines the spawn area
    public LayerMask obstacleLayer; // Layer for obstacles to detect collisions
    public float spawnRate = 2f; // Time in seconds between spawns

    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private float spawnTimer;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnRate)
        {
            TrySpawnEnemy();
            spawnTimer = 0f;
        }
    }

    public void TrySpawnEnemy()
    {
        if (spawnedEnemies.Count >= maxEnemies)
        {
            Debug.Log("Max enemy limit reached.");
            return;
        }

        Vector2 spawnPosition = GetValidSpawnPosition();

        if (spawnPosition != Vector2.zero)
        {
            SpawnEnemy(spawnPosition);
        }
        else
        {
            Debug.Log("No valid spawn position found.");
        }
    }

    private Vector2 GetValidSpawnPosition()
    {
        int maxAttempts = 100; // Limit attempts to avoid infinite loops
        for (int i = 0; i < maxAttempts; i++)
        {
            // Generate a random position within the bounds of the BoxCollider2D
            Bounds bounds = spawnArea.bounds;
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            Vector2 randomPosition = new Vector2(x, y);

            // Check if position is valid
            if (IsValidSpawnPosition(randomPosition))
            {
                return randomPosition;
            }
        }

        return Vector2.zero; // Return zero vector if no valid position is found
    }

    private bool IsValidSpawnPosition(Vector2 position)
    {
        // Check if position is inside the spawn area
        if (!spawnArea.OverlapPoint(position))
        {
            return false;
        }

        // Check for obstacles using an overlap check with the BoxCollider2D layer
        Collider2D obstacle = Physics2D.OverlapPoint(position, obstacleLayer);
        if (obstacle != null)
        {
            return false; // Position is inside an obstacle
        }

        // Check if position is within the player's avoidance radius
        float distanceToPlayer = Vector2.Distance(position, player.position);
        if (distanceToPlayer < playerAvoidanceRadius)
        {
            return false; // Position is too close to the player
        }

        return true; // Position is valid
    }

    private void SpawnEnemy(Vector2 position)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);

        // Assign the player Transform to the enemy's seeker script dynamically
        var seekerScript = newEnemy.GetComponent<AIDestinationSetter>(); // Replace with your actual seeker script's name
        if (seekerScript != null)
        {
            seekerScript.target = player;
        }

        spawnedEnemies.Add(newEnemy);
        Debug.Log("Enemy spawned at: " + position);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
        Destroy(enemy);
    }

    private void OnDrawGizmos()
    {
        // Draw the player avoidance radius in the Scene view
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(player.position, playerAvoidanceRadius);
        }

        // Draw the spawn area bounds in the Scene view
        if (spawnArea != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(spawnArea.bounds.center, spawnArea.bounds.size);
        }
    }
}
