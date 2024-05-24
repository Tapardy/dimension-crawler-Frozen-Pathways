using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] GameObject DoorClosed;
    [SerializeField] GameObject DoorOpen; 
    [SerializeField] List<GameObject> conditions;
    [SerializeField] GameObject[] conditionLights;

    [SerializeField] Material lightOffMaterial;
    [SerializeField] Material lightOnMaterial;

    private int conditionCount;

    // Start is called before the first frame update
    void Start()
    {
        DoorOpen.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!conditions.Any(c => c.GetComponent<ConditionController>().conditionMet == false))
        {
            OpenDoor();
        }

        conditionCount = conditions.Where(c => c.GetComponent<ConditionController>().conditionMet == true).Count();

        if(conditions.Any(c => c.GetComponent<ConditionController>().conditionMet == false))
        {
            for (int i = 0; i < conditionCount; i++)
            {
                conditionLights[i].GetComponent<Renderer>().material = lightOnMaterial;
            }

            for (int i = conditionCount; i < conditions.Count(); i++)
            {
                conditionLights[i].GetComponent<Renderer>().material = lightOffMaterial;
            }
        } else
        {
            foreach (var condition in conditions)
            {
                condition.GetComponent<ConditionController>().LockCondition();    
            }
            foreach (var light in conditionLights)
            {
                light.GetComponent<Renderer>().material = lightOnMaterial;
            }
        }
        
    }

    public void OpenDoor()
    {
        DoorClosed.GetComponent<MeshRenderer>().enabled = false;
        DoorOpen.GetComponent<BoxCollider>().enabled = true;
    }
}
