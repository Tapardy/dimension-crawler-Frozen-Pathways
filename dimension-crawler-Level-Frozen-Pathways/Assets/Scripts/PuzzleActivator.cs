using UnityEngine;

public class PuzzleActivator : MonoBehaviour
{
    public LayerMask puzzleObjectLayer;

    private Collider[] collidersInRange = new Collider[5];
    public string laserTutorial = "laser"; 
    private PlayerControllerFrozenPathways playerController;
    private bool triggered = false;
    private bool seenStasis = false;
    private bool seenLaser = false;
    private bool tutorialComplete = false;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerControllerFrozenPathways>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int numColliders =
                Physics.OverlapSphereNonAlloc(transform.position, 1000, collidersInRange, puzzleObjectLayer);

            for (int i = 0; i < numColliders; i++)
            {
                Rigidbody puzzleRigidbody = collidersInRange[i].GetComponent<Rigidbody>();
                if (puzzleRigidbody != null)
                {
                    puzzleRigidbody.isKinematic = false;
                }
            }
        }

        if (playerController != null && triggered == false)
        {
            if (!tutorialComplete)
            {
                HandleTutorial();
            }
            triggered = true;
        }
    }

 private void HandleTutorial()
    {
        if (!tutorialComplete)
        {
            Debug.Log("Triggered tutorial of laser");
            playerController.HandleLaser();
            tutorialComplete = true;
        }
    }
 
    private void ActivatePuzzleObjects(Collider[] puzzleObjects)
    {
        foreach (Collider puzzleCollider in puzzleObjects)
        {
            Rigidbody puzzleRigidbody = puzzleCollider.GetComponent<Rigidbody>();
            if (puzzleRigidbody != null)
            {
                puzzleRigidbody.isKinematic = false;
            }
        }
    }
}
