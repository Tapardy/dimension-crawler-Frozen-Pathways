using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;

public class ChestController : MonoBehaviour
{
    public TextEditor textEditor;
    [SerializeField] private GameObject closedChest;
    [SerializeField] private GameObject openChest;
    [SerializeField] private bool openedChest = false;
    [SerializeField] private AudioSource audio;
    [SerializeField] private float chestDelay = 1.0f;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject objectToDisable;
    [SerializeField] private GameObject objectToEnable;

    private bool inRange = false;
    

    private void Update()
    {
        if (inRange && openedChest == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                openedChest = true;
                player.GetComponent<PlayerControllerFrozenPathways>().ChestCounter();
                OpenChest();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            textEditor.toggleChestInteractTooltip();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            textEditor.ClearText();
        }
    }

    private void OpenChest()
    {
        audio.Play(); //play audio before opening chest
        objectToDisable.SetActive(false);
        objectToEnable.SetActive(true);
        StartCoroutine(OpenChestWithDelay());
    }
    
    private IEnumerator OpenChestWithDelay()
    {
        yield return new WaitForSeconds(chestDelay);
        Debug.Log("opened chest");
        closedChest.SetActive(false);
        openChest.SetActive(true);
        textEditor.ClearText();
        closedChest.SetActive(false);
    }
}