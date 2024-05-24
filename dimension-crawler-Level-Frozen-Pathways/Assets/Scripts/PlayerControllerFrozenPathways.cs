using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControllerFrozenPathways : MonoBehaviour
{
    private int minutes;
    private bool paused = false;
    private bool finished = false;
    private bool tutorialOpen = false;
    private bool firstLost = false;
    private bool seenMove = false;
    private bool seenLaser = false;
    private bool seenReset = false;
    private bool seenInteract = false;
    private bool seenMagnetic = false;
    private bool seenStasis = false;
    private bool tutorialComplete = false;
    public float timer = 0.0f;
    private bool stasisTutorialManager = false;
    private int openedChests;
    public bool openedAllChests = false;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI chestCount;
    [SerializeField] Canvas laserTutorial;
    [SerializeField] Canvas interactTutorial;
    [SerializeField] Canvas pauseMenu;
    [SerializeField] Canvas tooltipHelp;
    [SerializeField] Canvas finishMenu;
    [SerializeField] Canvas moveTutorial;
    [SerializeField] Canvas introMenu;
    [SerializeField] Canvas stasisTutorial;
    [SerializeField] Canvas resetTutorial;


    private void Start()
    {
        ShowTooltip("intro");
    }

    public bool CanSetDestination()
    {
        if (paused || tutorialOpen) return false;
        return true;
    }

    public bool CanUseLaser()
    {
        if (paused || tutorialOpen) return false;
        return true;
    }

    public bool CanUseStasis()
    {
        if (paused || tutorialOpen) return false;
        else if (!seenStasis) return false;
        return true;
    }

    void Update()
    {
        if (finished) return;
        Cursor.lockState = tutorialOpen || paused ? CursorLockMode.None : CursorLockMode.Locked;
        GetComponent<FirstPersonControllerFrozenPathways>().cameraCanMove = tutorialOpen || paused ? false : true;

        if(Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            ResetTutorial();
            ShowTooltip("move");
        }

        timer += Time.deltaTime;
        if(timer >= 60) { minutes++; timer = 0f; }
        timerText.text = $"{minutes}:{Mathf.Round(timer)}s";
        if(minutes > 2)
        {
            Lost();
        }
    }

    public void ChestCounter()
    {
        openedChests++;
        if (openedChests == 3)
        {
            openedAllChests = true;
        }
    }

    public void Lost()
    {
        if (firstLost) return;
        firstLost = true;
        ShowTooltip("Lost");
    }

    public void TogglePause()
    {
        if (tutorialOpen) return;
        paused = !paused;
        Time.timeScale = paused ? 0f : 1f;
        pauseMenu.GetComponent<Canvas>().enabled = paused;  
    }

    public void ResetTutorial()
    {
        tutorialComplete = false;
        seenInteract = false;
        seenReset = false;
        seenMove = false;
    }

    public void FinishLevel()
    {
        Time.timeScale = 0f;
        GetComponent<FirstPersonControllerFrozenPathways>().cameraCanMove = false;
        finishMenu.GetComponent<Canvas>().enabled = true;
        finished = true;
    }
        

    public void ShowTooltip(string tooltip)
    {
        switch(tooltip)
        {
            default:
            case "lost":
                tooltipHelp.GetComponent<Canvas>().enabled = true;
                break;
            case "intro":
                introMenu.GetComponent<Canvas>().enabled = true;
                break;
            case "laser":
                laserTutorial.GetComponent<Canvas>().enabled = true;
                seenLaser = true;
                break;
            case "reset":
                resetTutorial.GetComponent<Canvas>().enabled = true;
                seenReset = true;
                break;
            case "interact":
                interactTutorial.GetComponent<Canvas>().enabled = true;
                seenInteract = true;
                break;
            case "move":
                moveTutorial.GetComponent<Canvas>().enabled = true;
                seenMove = true;
                break;
            case "stasis":
                stasisTutorial.GetComponent<Canvas>().enabled = true;
                seenStasis = true;
                break;
        }
        tutorialOpen = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideTooltip()
    {
        tutorialOpen = false;
        Time.timeScale = 1f;
        introMenu.GetComponent<Canvas>().enabled = false;
        laserTutorial.GetComponent<Canvas>().enabled = false;
        interactTutorial.GetComponent<Canvas>().enabled = false;
        tooltipHelp.GetComponent<Canvas>().enabled = false;
        moveTutorial.GetComponent<Canvas>().enabled = false;
        stasisTutorial.GetComponent<Canvas>().enabled = false;
        resetTutorial.GetComponent<Canvas>().enabled = false;

        if(!tutorialComplete)
        {
            HandleTutorial();
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        GetComponent<FirstPersonControllerFrozenPathways>().cameraCanMove = true;
    }

    public void HandleTutorial()
    {
        if (tutorialComplete) return;
        if (!seenMove)
        {
            ShowTooltip("move");
            return;
        }

        if (!seenInteract)
        {
            ShowTooltip("interact");
            return;
        }

        if (!seenReset)
        {
            ShowTooltip("reset");
            return;
        }
    }

    public void HandleStasis()
    {
        if (stasisTutorialManager) return;
        if (!seenStasis)
        {
            ShowTooltip("stasis");
            return;
        }


    }
    public void HandleLaser()
    {
        ShowTooltip("laser");
    }
}
