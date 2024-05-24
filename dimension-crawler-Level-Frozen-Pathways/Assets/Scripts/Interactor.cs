using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Interactor : MonoBehaviour
{
    public TMP_Text Text;
    public GameObject Button;

    private bool isInteracting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entered button trigger");
            Text.text = "Press 'E' to press button"; 
            isInteracting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Exited button trigger");
            Text.text = "";
            isInteracting = false;
        }
    }

    private void Update()
    {
        if (isInteracting)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Button pressed");
                Button.transform.Translate(0, -0.5f, 0);
                Text.text = "";
            }
        }
    }
}