using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TextEditor : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI textMesh;
    private string displayedText;
    private bool textShown = false;

    private string magneticLegs = "Press [Q] to toggle Magnetic Legs On or Off";
    private string interactButton = "Press [E] to interact with the Button";
    private string interactChest = "Press [E] to open the chest";
    private string clearCon = "You are unable to leave alone";
    private string chestClearCon = "Find and open all 3 chests before you can open it";
    void Update()
    {
        if(textShown)
        {
            textMesh.text = displayedText;
            canvas.GetComponent<Canvas>().enabled = true;
        } else
        {
            canvas.GetComponent<Canvas>().enabled = false;
        }
        
    }

    public void EditText(string text)
    {
        displayedText = text;
        textShown = true;
    }

    public void ClearText()
    {
        displayedText = "";
        textShown = false;
    }

    public void toggleMagneticTooltip()
    {
        if(textShown)
        {
            ClearText();
        }
        else
        {
            EditText(magneticLegs);
        }
    }
    
    public void toggleChestInteractTooltip()
    {
        if(textShown)
        {
            ClearText();
        }
        else
        {
            EditText(interactChest);
        }
    }
    
    public void toggleChestClearConTooltip()
    {
        if(textShown)
        {
            ClearText();
        }
        else
        {
            EditText(chestClearCon);
        }
    }

    public void ToggleClearConditionTooltip()
    {
        if (textShown)
        {
            ClearText();
        }
        else
        {
            EditText(clearCon);
        }
    }

    public void toggleInteractTooltip()
    {
        if (textShown)
        {
            ClearText();
        }
        else
        {
            EditText(interactButton);
        }
    }
}
