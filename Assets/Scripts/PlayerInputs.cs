using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public float lateralInput { get; private set; }

    void Update()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        lateralInput = Input.GetAxis("Horizontal");
    }
}
