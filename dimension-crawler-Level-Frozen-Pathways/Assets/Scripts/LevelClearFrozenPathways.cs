using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelClearFrozenPathways : MonoBehaviour
{
    [FormerlySerializedAs("objectToDisable")] public GameObject objectToEnable;
    private bool playerEntered = false;
    private bool spiderEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Spider"))
        {
            if (other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Spider"))
            {
                playerEntered = true;
                Debug.Log("Player entered");

            }
            else if (other.gameObject.CompareTag("Spider"))
            {
                spiderEntered = true;
                Debug.Log("Spider entered");
            }

            if (playerEntered && spiderEntered)
            {
                LevelClear();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerEntered = false;
        }
        else if (other.gameObject.CompareTag("Spider"))
        {
            spiderEntered = false;
        }
    }

    private void LevelClear()
    {
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
        }
    }
}
