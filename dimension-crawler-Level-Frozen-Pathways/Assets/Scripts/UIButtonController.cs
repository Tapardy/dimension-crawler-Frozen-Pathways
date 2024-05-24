using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonController : MonoBehaviour
{
    [SerializeField] GameObject Player;

    public void UITutorialCloseButton()
    {
        Player.GetComponent<PlayerController>().HideTooltip();
    }
}
