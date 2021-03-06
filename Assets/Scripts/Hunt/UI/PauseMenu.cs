﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Button pause;
    [SerializeField] private SendingBonusInfo sendInfo;
    private void Start()
    {
        Resume();
    }

    public void Pause()
    {
        menuPanel.SetActive(true);
        pause.enabled = false;
        Time.timeScale = 0;
    }


    public void Resume()
    {
        menuPanel.SetActive(false);
        pause.enabled = true;
        Time.timeScale = 1;
    }

    public void Highlited(GameObject menuButton)
    {
        menuButton.GetComponent<Animator>().SetInteger("State", 1);
    }

    public void UnHighlited(GameObject menuButton)
    {
        menuButton.GetComponent<Animator>().SetInteger("State", 2);
    }

    public void SaveDataOnQuit()
    {
        StartCoroutine(sendInfo.SendOfflineCheck("post"));
    }
}
