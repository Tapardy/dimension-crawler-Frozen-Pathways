using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 checkpointPosition;
    private bool checkpointReached = false; 
    public LayerMask collisionLayer;
    private Vector3 respawnPoint = new Vector3(201.889999f,15.1599998f,407.799988f);

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            checkpointPosition = respawnPoint;
            checkpointReached = true;
            Debug.Log("Checkpoint Coordinates: " + checkpointPosition);
            initialPosition = checkpointPosition;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            checkpointReached = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & collisionLayer) != 0)
        { 
            if (!checkpointReached)
            {
                ResetPlayer();
            }
            else
            {
                ResetPlayerToCheckpoint();
            }
        }
    }

    private void ResetPlayer()
    {
        transform.position = initialPosition;
        Debug.Log("Respawn Coordinates: " + initialPosition);
    }

    private void ResetPlayerToCheckpoint()
    {
        transform.position = checkpointPosition;
        Debug.Log("Respawn Coordinates: " + checkpointPosition);
    }
}
