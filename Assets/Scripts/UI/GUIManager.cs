using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : Singleton<GUIManager>
{
    public GameObject mainMenu;
    public GameObject playGame;
    public Image timeBar;
    public PauseDialog pauseDialog;
    public TimeoutDialog timeoutDialog;
    public GameoverDialog gameoverDialog;
    public override void Awake()
    {
        MakeSingleton(false);
    }
    public void ShowGamePlay(bool isshow)
    {
        if (playGame)
            playGame.SetActive(isshow);
        if (mainMenu)
            mainMenu.SetActive(!isshow);
    }
    public void UpdateTimeBar(float curTime, float totalTime)
    {
        float rate = curTime / totalTime;
        if (timeBar)
            timeBar.fillAmount = rate;
    }
}
