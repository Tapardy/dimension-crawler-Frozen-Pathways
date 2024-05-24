using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionController : MonoBehaviour
{
    public bool conditionMet;
    public bool locked = false;

    public void SetCondition(bool result)
    {
        if (locked) return;
        conditionMet = result;
    }

    public void LockCondition()
    {
        conditionMet = true;
        locked = true;
    }
}
