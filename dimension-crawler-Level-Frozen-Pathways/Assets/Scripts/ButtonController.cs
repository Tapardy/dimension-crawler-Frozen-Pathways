using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public bool playerButton = true;
    public bool sideways = false;
    [SerializeField] GameObject ButtonHead;
    private bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                EnableButton();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Spider" && !playerButton)
        {
            EnableButton();
        } else if(other.gameObject.tag == "Player" && playerButton)
        {
            playerInRange = true;
            other.gameObject.GetComponent<TextEditor>().toggleInteractTooltip();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Spider" && !playerButton)
        {
            DisableButton();
        } else if (other.gameObject.tag == "Player" && playerButton)
        {
            playerInRange = false;
            other.gameObject.GetComponent<TextEditor>().toggleInteractTooltip();
            DisableButton();
        }
    }

    void EnableButton()
    {
        this.GetComponent<ConditionController>().SetCondition(true);
        Vector3 ButtonPos = transform.position;
        Vector3 TargetPos = sideways ? new Vector3(ButtonPos.x, ButtonPos.y, ButtonPos.z + 0.4f) : new Vector3(ButtonPos.x, ButtonPos.y - 0.4f, ButtonPos.z);
        ButtonHead.GetComponent<Transform>().position = TargetPos;
    }

    void DisableButton()
    {
        this.GetComponent<ConditionController>().SetCondition(false);
        Vector3 ButtonPos = transform.position;
        Vector3 TargetPos = sideways ? new Vector3(ButtonPos.x, ButtonPos.y, ButtonPos.z - 0.4f) : new Vector3(ButtonPos.x, ButtonPos.y + 0.4f, ButtonPos.z);
        ButtonHead.GetComponent<Transform>().position = TargetPos;
    }

}
