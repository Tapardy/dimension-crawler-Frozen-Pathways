using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XTrigger : MonoBehaviour
{
    [SerializeField] GameObject Spider;
    [SerializeField] GameObject Player;

    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Spider)
        {
            Player.GetComponent<TextEditor>().toggleMagneticTooltip();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Spider)
        {
            Player.GetComponent<TextEditor>().toggleMagneticTooltip();
        }
    }
}
