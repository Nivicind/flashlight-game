using UnityEngine;

public class EnemyRoamingSystem : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // Movement speed
    [SerializeField] private float roamDelay = 2f; // Time between roaming attempts
    [SerializeField] private float roamDistance = 2.0f; // Roaming distance   
    [SerializeField] private LayerMask obstacleLayer; // Layer to detect obstacles

    private Vector2 roamTarget; // Current roaming target
    private float roamTimer;
    private bool isMoving = false;

    private void Start()
    {
        roamTimer = roamDelay;
    }

    private void Update()
    {
        roamTimer -= Time.deltaTime;

        if (roamTimer <= 0 && !isMoving)
        {
            PickNewRoamTarget();
            roamTimer = roamDelay;
        }

        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    private void PickNewRoamTarget()
    {
        // Try to find a valid random point in the scene
        for (int i = 0; i < 10; i++) // Attempt 10 times to find a valid point
        {
            Vector2 randomDirection = Random.insideUnitCircle * roamDistance; // You can adjust the roam distance to be larger for full roaming
            Vector2 potentialTarget = (Vector2)transform.position + randomDirection;

            // Check if the path to the target is clear
            if (!Physics2D.CircleCast(transform.position, 0.5f, (potentialTarget - (Vector2)transform.position).normalized, Vector2.Distance(transform.position, potentialTarget), obstacleLayer))
            {
                roamTarget = potentialTarget;
                isMoving = true;
                return;
            }
        }

        // If no valid target is found, stay idle
        Debug.LogWarning("No valid roaming target found. Staying idle.");
        isMoving = false;
    }
    private void MoveTowardsTarget()
    {
        Vector2 direction = (roamTarget - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.position = Vector2.MoveTowards(transform.position, roamTarget, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, roamTarget) < 0.1f)
        {
            isMoving = false;
        }
    }

    public void ClearRoamTarget()
    {
        // This method clears the roaming target when AIPath is enabled (chasing)
        isMoving = false;
        roamTarget = Vector2.zero; // Clear the target
    }

    private void OnDrawGizmos()
    {
        // Visualize the current target
        if (isMoving)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, roamTarget);
            Gizmos.DrawSphere(roamTarget, 0.1f);
        }
    }
}
