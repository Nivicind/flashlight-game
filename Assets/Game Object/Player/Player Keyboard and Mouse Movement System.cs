using UnityEngine;

public class PlayerKeyboardAndMouseMovementSystem : MonoBehaviour
{
    Vector2 moveInput;
    private Animator animator;

    private Rigidbody2D rb;
    public float moveSpeed = 0.0f;
    public float rotationSpeed = 0.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

    }

    void Update()
    {
        KeyboardInput();
        MouseFacingDirection();
    }

    public void KeyboardInput()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        
        if (moveInput != Vector2.zero)
        {
            animator.SetBool("isWalking", true); // Turn on walk animation
        }
        else
        {
            animator.SetBool("isWalking", false); // Turn off walk animation
        }
    }

    private void MouseFacingDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
