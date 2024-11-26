using UnityEngine;

public class TurnFlashlightOnAndOff : MonoBehaviour
{
    public GameObject flashlight; // Reference to the flashlight GameObject

    void Update()
    {
        if (Input.GetMouseButton(0)) // Right mouse button is held down
        {
            flashlight.SetActive(true);
        }
        else
        {
            flashlight.SetActive(false);
        }
    }
}
