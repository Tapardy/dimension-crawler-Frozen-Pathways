using System.Collections.Generic;
using UnityEngine;

public class BreakIce : MonoBehaviour
{
    [SerializeField] private GameObject Ice;
    [SerializeField] private AudioSource audio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Ice.GetComponent<Collider>().enabled = false;
            Ice.GetComponent<MeshRenderer>().enabled = false;
            audio.Play();
        }
    }
}