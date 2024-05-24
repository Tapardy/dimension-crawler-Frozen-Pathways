using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonControllerFrozenPathways : MonoBehaviour
{
    [SerializeField] GameObject Player;

    public void UITutorialCloseButton()
    {
        Player.GetComponent<PlayerControllerFrozenPathways>().HideTooltip();
    }
}
