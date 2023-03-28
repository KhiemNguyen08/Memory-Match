using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeoutDialog : Dialog
{
    // Start is called before the first frame update
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        if (SceneController.Ins)
            SceneController.Ins.LoadSceneCurent();
    }
    public void RePlay()
    {
        SceneManager.sceneLoaded += OnSceneLoadedEvent;
        if (SceneController.Ins)
            SceneController.Ins.LoadSceneCurent();
    }
    private void OnSceneLoadedEvent(Scene scene , LoadSceneMode mode)
    {
        if (GameManager.Ins)
            GameManager.Ins.PlayGame();
        SceneManager.sceneLoaded -= OnSceneLoadedEvent;

    }
}
