using System.Collections.Generic;
using UnityEngine;

public class StasisController : MonoBehaviour
{
    private List<GameObject> lockedObjects = new List<GameObject>();
    private Vector3 storedVelocity;
    [SerializeField] private float lockDuration = 3f;
    private float cooldownTimer = 0f;
    private float unfreezeCooldownTimer = 0f; // New cooldown timer for unfreezing
    [SerializeField] private float cooldownDuration = 1f;
    [SerializeField] private LayerMask ignoreLayerMask;
    [SerializeField] private AudioClip freezeSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        List<GameObject> objectsToRemove = new List<GameObject>();

        if (Input.GetMouseButtonDown(1)) //right click to toggle stasis
        {
            if (Time.time >= cooldownTimer)
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)); //raycast at crosshair
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayerMask))
                {
                    if (hit.collider.CompareTag("StasisAble"))
                    {
                        ToggleStasis(hit.collider.gameObject);
                    }
                }
            }
        }

        foreach (GameObject lockedObject in lockedObjects)
        {
            if (Time.time >= GetUnlockTime(lockedObject))
            {
                objectsToRemove.Add(lockedObject);
            }
        }

        foreach (GameObject obj in objectsToRemove)
        {
            UnlockObject(obj);
            lockedObjects.Remove(obj);
        }
    }

    private void LockObject(GameObject obj)
    {
        if (!lockedObjects.Contains(obj))
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                storedVelocity = rb.velocity;
                rb.isKinematic = true;
                audioSource.PlayOneShot(freezeSound);
            }
            SetUnlockTime(obj, Time.time + lockDuration);
            lockedObjects.Add(obj);
            cooldownTimer = Time.time + cooldownDuration;
        }
    }

    private void UnlockObject(GameObject obj)
    {
        lockedObjects.Remove(obj);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = storedVelocity;
        }
    }

    private void ToggleStasis(GameObject obj)
    {
        if (lockedObjects.Contains(obj))
        {
            UnlockObject(obj);
            return;
        }

        List<GameObject> objectsToUnlock = new List<GameObject>(lockedObjects);

        foreach (GameObject lockedObject in objectsToUnlock)
        {
            UnlockObject(lockedObject);
        }

        lockedObjects.Clear();

        LockObject(obj);
    }

    private float GetUnlockTime(GameObject obj)
    {
        float unlockTime = 0f;
        obj.TryGetComponent(out StasisObject stasisObject);
        if (stasisObject != null)
        {
            unlockTime = stasisObject.UnlockTime;
        }
        return unlockTime;
    }

    private void SetUnlockTime(GameObject obj, float unlockTime)
    {
        obj.TryGetComponent(out StasisObject stasisObject);
        if (stasisObject != null)
        {
            stasisObject.UnlockTime = unlockTime;
        }
        else
        {
            stasisObject = obj.AddComponent<StasisObject>();
            stasisObject.UnlockTime = unlockTime;
        }
    }
}