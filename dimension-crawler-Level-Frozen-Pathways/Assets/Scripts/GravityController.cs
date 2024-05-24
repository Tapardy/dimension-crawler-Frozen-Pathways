using UnityEngine;

public class GravityController : MonoBehaviour
{
    public float gravityValue = 1f;

    void FixedUpdate()
    {
        ApplyGravity();
    }

    void ApplyGravity()
    {
        Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();

        foreach (Rigidbody rb in rigidbodies)
        {
            if (rb != null && rb != GetComponent<Rigidbody>())
            {
                Vector3 gravityDirection = Physics.gravity.normalized;
                rb.AddForce(gravityDirection * gravityValue, ForceMode.Acceleration);
            }
        }
    }
}