using UnityEngine;
using UnityEngine.AI;

public class SpiderReset : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 initialSpeed;
    public LayerMask collisionLayer;
    [SerializeField] private GameObject spiderObject;

    private void Start()
    {
        initialPosition = transform.position;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            initialSpeed = rb.velocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & collisionLayer) != 0 || collision.gameObject.tag.Contains("LethalObject"))
        {
            ResetSpider();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetSpider();
        }
    }

    private void ResetSpider()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = initialSpeed;
        }
        transform.position = initialPosition;

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.destination = transform.position;
        }

        if (spiderObject != null)
        {
            spiderObject.transform.position = new Vector3(0f, 0f, 0f);
        }
    }
}