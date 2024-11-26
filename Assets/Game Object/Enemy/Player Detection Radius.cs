using Pathfinding;
using UnityEngine;

public class PlayerDetectionRadius : MonoBehaviour
{
    private Transform target; // The player's transform

    public float detectionRadius = 5f; // Radius for detecting the player
    private bool isPlayerDetected = false;

    private AIPath pathfindingScript; // Reference to the AIPath script

    private void Start()
    {
        // Get the AIPath component from the prefab
        pathfindingScript = GetComponent<AIPath>();

        // Dynamically find the player in the scene
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the Player has the correct tag.");
        }
    }

    private void Update()
    {
        if (target == null || pathfindingScript == null)
            return;

        // Check if the player is within the detection radius
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        isPlayerDetected = distanceToPlayer <= detectionRadius;

        // Enable or disable the AIPath script based on detection
        pathfindingScript.enabled = isPlayerDetected;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the detection radius in the Scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
