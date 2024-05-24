using UnityEngine;

public class FreeCamController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sensitivity = 2f;

    void Update()
    {
        // Camera Rotation
        if (Input.GetMouseButton(1)) // Right mouse button held down
        {
            RotateCamera();
        }

        // Camera Movement
        MoveCamera();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 rotation = new Vector3(-mouseY * sensitivity, mouseX * sensitivity, 0);
        transform.eulerAngles += rotation;
    }

    void MoveCamera()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float upDown = 0f;

        if (Input.GetKey(KeyCode.Space)) // Move camera upward
            upDown = 1f;
        else if (Input.GetKey(KeyCode.LeftControl)) // Move camera downward
            upDown = -1f;

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical + transform.up * upDown;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
