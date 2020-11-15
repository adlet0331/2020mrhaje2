using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private GameObject Option;
    [SerializeField] private GameObject Paused;
    [SerializeField] private bool isPaused;
    [SerializeField] private bool optionOn;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionOn) optionOn = false;
            else isPaused = !isPaused;
        }

        if(optionOn)
        {
            ActivateOption();
        }
        else if(isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    public void ActivateOption()
    {
        Option.SetActive(true);
        Paused.SetActive(false);
        optionOn = true;
    }

    public void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        PauseMenuUI.SetActive(true);
        Option.SetActive(false);
        Paused.SetActive(true);
        optionOn = false;
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        PauseMenuUI.SetActive(false);
        isPaused = false;
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
