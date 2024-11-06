using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public GameObject spotlight; // Reference to the spotlight

    private float distance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Follow Player
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Adjust angle to face y-axis

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the enemy enters the spotlight's range
        if (other.gameObject == spotlight)
        {
            // Destroy the enemy
            Destroy(gameObject);
        }
    }
}
