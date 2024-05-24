using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFreeAnim : MonoBehaviour
{
    [SerializeField]
    private SpiderControllerFrozenPathways spiderController;

    Vector3 rot = Vector3.zero;
    float rotSpeed = 40f;
    Animator anim;

    // Use this for initialization
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        gameObject.transform.eulerAngles = rot;
    }

    // Update is called once per frame
    void Update()
    {
        if (spiderController != null)
        {
            // Check if the spider is moving
            if (spiderController.IsMoving())
            {
                anim.SetBool("Walk_Anim", true);
            }
            else
            {
                anim.SetBool("Walk_Anim", false);
            }
        }
    }

    void CheckKey()
    {
        // Walk
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("Walk_Anim", true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("Walk_Anim", false);
        }

        // Rotate Left
        if (Input.GetKey(KeyCode.A))
        {
            rot[1] -= rotSpeed * Time.fixedDeltaTime;
        }

        // Rotate Right
        if (Input.GetKey(KeyCode.D))
        {
            rot[1] += rotSpeed * Time.fixedDeltaTime;
        }
    }
}