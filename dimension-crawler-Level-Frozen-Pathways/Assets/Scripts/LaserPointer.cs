using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    public Camera playerCamera;
    public LineRenderer laserLine;
    public LineRenderer stasisLine;
    public TextEditor textEditor;
    [SerializeField] GameObject Player;
    [SerializeField] private float minDistance = 1.4f;
    [SerializeField] private float maxDistance = 50f;

    private bool isLaserActive = false;
    private bool isStasisActive = false;

    private IEnumerator LaserActivationDelay()
    {
        isLaserActive = true;
        laserLine.enabled = true;
        
        yield return new WaitForSeconds(0.15f);

        while (Input.GetMouseButton(0))
        {
            UpdateLaserPointer();
            yield return null;
        }
    }
    
    private IEnumerator StasisActivationDelay()
    {
        isStasisActive = true;
        stasisLine.enabled = true;
        
        yield return new WaitForSeconds(0.15f);

        while (Input.GetMouseButton(0))
        {
            UpdateStasis();
            yield return null;
        }
    }
    
    private void Update()
    {

        if (Input.GetMouseButtonDown(0)&& Player.GetComponent<PlayerControllerFrozenPathways>().CanUseLaser())
        {
            isLaserActive = true;
            StartCoroutine(LaserActivationDelay());
        }
        if (Input.GetMouseButtonDown(1)&& Player.GetComponent<PlayerControllerFrozenPathways>().CanUseStasis())
        {
            isStasisActive = true;
            StartCoroutine(StasisActivationDelay());
        }
        if (Input.GetMouseButtonUp(0))
        {
            isLaserActive = false;
            laserLine.enabled = false;
        }
        
        if (Input.GetMouseButtonUp(1))
        {
            isStasisActive = false;
            stasisLine.enabled = false;
        }

        if (isLaserActive)
        {
            UpdateLaserPointer();
        }

        if (isStasisActive)
        {
            UpdateStasis();
        }
        
    }

    private void UpdateLaserPointer()
    {
        Vector3 startPoint = transform.position;
        Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0f);
        Ray ray = playerCamera.ViewportPointToRay(screenCenter);
    
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            //check for a minimal distance, so laser doesn't phase through the laser pointer
            if (Vector3.Distance(transform.position, hit.point) > minDistance)
            {
                laserLine.enabled = true;
                laserLine.SetPosition(1, hit.point);
            }
            else
            {
                laserLine.enabled = false;
            }
        }
        else
        {
            Vector3 endPoint = ray.origin + ray.direction * maxDistance;
            laserLine.enabled = true;
            laserLine.SetPosition(1, endPoint);
        }

        //update start point of the line renderer
        laserLine.SetPosition(0, startPoint);
    }
    
    private void UpdateStasis()
    {
        Vector3 startPoint = transform.position;
        Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0f);
        Ray ray = playerCamera.ViewportPointToRay(screenCenter);
    
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            //check for a minimal distance, so laser doesn't phase through the laser pointer
            if (Vector3.Distance(transform.position, hit.point) > minDistance)
            {
                stasisLine.enabled = true;
                stasisLine.SetPosition(1, hit.point);
            }
            else
            {
                stasisLine.enabled = false;
            }
        }
        else
        {
            Vector3 endPoint = ray.origin + ray.direction * maxDistance;
            stasisLine.enabled = true;
            stasisLine.SetPosition(1, endPoint);
        }

        //update start point of the line renderer
        stasisLine.SetPosition(0, startPoint);
    }

    // private void CreatePointOnGround()
    // {
    //     //ray from mouse to screen
    //     Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
    //
    //     //raycast to get collision, check if the layer is ground
    //     RaycastHit hit;
    //     if (Physics.Raycast(ray, out hit, maxDistance, LayerMask.GetMask("Ground")))
    //     {
    //         //get point
    //         Vector3 point = hit.point;
    //
    //         //log point
    //         Debug.Log("Point created on the ground at: " + point);
    //     }
    // }
}