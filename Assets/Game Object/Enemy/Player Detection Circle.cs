using UnityEngine;

public class PlayerDetectionCircle : MonoBehaviour
{
    public float detectionRadius = 5f; // Radius of the detection circle
    public MonoBehaviour aiPathScript; // Assign your AI pathfinding script in the inspector

    private CircleCollider2D detectionCollider;

    void Start()
    {
        // Add a CircleCollider2D to the GameObject if it doesn't already exist
        detectionCollider = gameObject.AddComponent<CircleCollider2D>();
        detectionCollider.isTrigger = true; // Set it as a trigger
        detectionCollider.radius = detectionRadius; // Set the detection radius
    }

    void Update()
    {
        // Update the collider radius dynamically if the radius changes in runtime
        if (detectionCollider.radius != detectionRadius)
        {
            detectionCollider.radius = detectionRadius;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the detection radius
        if (other.CompareTag("Player"))
        {
            EnableAI();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player exits the detection radius
        if (other.CompareTag("Player"))
        {
            DisableAI();
        }
    }

    void EnableAI()
    {
        if (aiPathScript != null)
        {
            aiPathScript.enabled = true;
        }
        Debug.Log("AI Pathfinding Enabled");
    }

    void DisableAI()
    {
        if (aiPathScript != null)
        {
            aiPathScript.enabled = false;
        }
        Debug.Log("AI Pathfinding Disabled");
    }

    void OnDrawGizmosSelected()
    {
        // Draw the detection radius in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
