using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.DebugUI.Table;

public class SpiderController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public GameObject destination;
    public bool magneticLegsEnabled = true;
    private Rigidbody rb;
    private bool gravity = false;
    private bool kinematic = true;
    [SerializeField] GameObject[] horizontalSurfaces;
    [SerializeField] GameObject[] verticalSurfaces;
    [SerializeField] GameObject Player;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Q))
        {
            MagneticToggle();
        }


        if (Input.GetMouseButtonDown(0) && agent.enabled && Player.GetComponent<PlayerController>().CanSetDestination())
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);

                destination.transform.position = hit.point;
            }
        }
        
    }

    public void MagneticToggle()
    {
        magneticLegsEnabled = !magneticLegsEnabled;
        foreach (var surface in verticalSurfaces)
        {
            surface.GetComponent<NavMeshSurface>().enabled = magneticLegsEnabled;
        }

        gravity = !gravity;
        kinematic = !kinematic;

        rb.useGravity = gravity;
        rb.isKinematic = kinematic;

        if (gravity)
        {
            agent.enabled = false;

            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.AddForce(Vector3.down * 10f, ForceMode.Impulse);
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);

            StartCoroutine(ReenableAgentAfterFall(2));
        }
    }

    IEnumerator ReenableAgentAfterFall(float delay)
    {
        yield return new WaitForSeconds(delay);
        gravity = !gravity;
        kinematic = !kinematic;

        rb.useGravity = gravity;
        rb.isKinematic = kinematic;
        agent.enabled = true;
    }
}
