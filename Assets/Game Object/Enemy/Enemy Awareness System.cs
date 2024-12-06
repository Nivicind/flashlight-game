using Pathfinding;
using UnityEngine;

public class EnemyAwarenessSystem : MonoBehaviour
{
    private Transform target; // The player's transform

    [SerializeField] private float detectionRadius = 10.0f; // Radius for detecting the player
    private bool isPlayerDetected = false;

    private AIPath pathfindingScript; // Reference to the AIPath script
    private EnemyRoamingSystem roamingScript; // Reference to the EnemyRoamingSystem script

    private void Start()
    {
        // Get the AIPath component
        pathfindingScript = GetComponent<AIPath>();

        // Get the EnemyRoamingSystem component
        roamingScript = GetComponent<EnemyRoamingSystem>();

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
        if (target == null || pathfindingScript == null || roamingScript == null)
            return;

        // Check if the player is within the detection radius
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        isPlayerDetected = distanceToPlayer <= detectionRadius;

        // Toggle between chasing and roaming
        if (isPlayerDetected)
        {
            pathfindingScript.enabled = true;  // Enable AIPath to chase the player
            roamingScript.enabled = false;    // Disable roaming

            // When AIPath is enabled, clear the roaming destination to avoid weird movement
            roamingScript.ClearRoamTarget();
        }
        else
        {
            pathfindingScript.enabled = false; // Disable chasing
            roamingScript.enabled = true;     // Enable roaming
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the detection radius in the Scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
