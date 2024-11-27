using UnityEngine;

public class PlayerCollisionLogger : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 lastPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("No Rigidbody found on the player. This script requires a Rigidbody.");
        }

        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            // Log velocity
            if (rb.linearVelocity != Vector3.zero)
            {
                Debug.Log($"[DEBUG] Player velocity: {rb.linearVelocity}");
            }

            // Log forces (approximation via acceleration)
            Vector3 acceleration = rb.linearVelocity / Time.fixedDeltaTime;
            if (acceleration != Vector3.zero)
            {
                Debug.Log($"[DEBUG] Player acceleration (force applied): {acceleration}");
            }
        }

        // Log position changes
        Vector3 currentPosition = transform.position;
        if (currentPosition != lastPosition)
        {
            Debug.Log($"[DEBUG] Player position changed from {lastPosition} to {currentPosition}");
            lastPosition = currentPosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"[DEBUG] Collision detected with {collision.gameObject.name}. Collision force: {collision.impulse}");
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log($"[DEBUG] Sustained collision with {collision.gameObject.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[DEBUG] Trigger entered with {other.gameObject.name}");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"[DEBUG] Trigger exited with {other.gameObject.name}");
    }
}
