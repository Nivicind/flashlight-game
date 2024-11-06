using UnityEngine;

public class LightController : MonoBehaviour
{
    private UnityEngine.Rendering.Universal.Light2D light2D;
    private Collider2D collider2D_;

    void Start()
    {
        // Get the Light2D component attached to the GameObject
        light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();

        // Get the Collider2D component attached to the GameObject
        collider2D_ = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Check if any of the specified keys are being held down
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {

            light2D.enabled = true;
            collider2D_.enabled = true;
        }
        else
        {

            light2D.enabled = false;
            collider2D_.enabled = false;
        }
    }
}
