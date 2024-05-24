using UnityEngine;

public class ObjectReset : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 initialSpeed;
    public LayerMask collisionLayer;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & collisionLayer) != 0 || collision.gameObject.tag.Contains("LethalObject"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = initialSpeed;
            }
            transform.position = initialPosition;
        }
    }
}