using System.Collections;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public LayerMask puzzleObjectLayer;
    [SerializeField] private float activatedOffset = 1f;
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private bool isActivated = false;
    [SerializeField] private bool isHorizontal = false;
    [SerializeField] private int moveIterations = 5;
    [SerializeField] private float moveDistancePerIteration = 1f;
    [SerializeField] private float delayBetweenIterations = 0.5f; 
    [SerializeField] private AudioClip activateSound;
    [SerializeField] private AudioClip deactivateSound;

    [SerializeField] private GameObject objectToDisable;
    private AudioSource audioSource;
    private Vector3 originalPosition;
    private Vector3 pressurePlatePosition;
    private Coroutine moveCoroutine;
    private int downMoveSteps = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalPosition = objectToMove.transform.position;
        pressurePlatePosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (puzzleObjectLayer == (puzzleObjectLayer | (1 << other.gameObject.layer)))
        {
            if (!isActivated)
            {
                
                audioSource.PlayOneShot(activateSound);
                objectToDisable.SetActive(true);
                if (isHorizontal)
                {
                    transform.position -= new Vector3(0,activatedOffset , 0);
                }
                else
                {
                    transform.position -= new Vector3(-activatedOffset, 0, 0);
                }
                isActivated = true;
                if (moveCoroutine != null)
                {
                    StopCoroutine(moveCoroutine);
                }
                moveCoroutine = StartCoroutine(MoveObject());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (puzzleObjectLayer == (puzzleObjectLayer | (1 << other.gameObject.layer)))
        {
            if (isActivated)
            {
                audioSource.PlayOneShot(deactivateSound);
                objectToDisable.SetActive(false);
                isActivated = false;
                transform.position = pressurePlatePosition;
                if (moveCoroutine != null)
                {
                    StopCoroutine(moveCoroutine);
                }
                moveCoroutine = StartCoroutine(MoveObjectBack());
            }
        }
    }

    private IEnumerator MoveObject()
    {
        for (int i = 0; i < moveIterations; i++)
        {
            yield return new WaitForSeconds(delayBetweenIterations);
            objectToMove.transform.position -= Vector3.up * moveDistancePerIteration;
            downMoveSteps++;
        }
    }

    private IEnumerator MoveObjectBack()
    {
        for (int i = 0; i < downMoveSteps; i++)
        {
            yield return new WaitForSeconds(delayBetweenIterations);
            objectToMove.transform.position += Vector3.up * moveDistancePerIteration;
        }
        downMoveSteps = 0;
    }
}
