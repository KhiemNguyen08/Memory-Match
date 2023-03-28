using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverDialog : Dialog
{
    public Text totalMoveTExt;
    public Text bestMoveText;
    public override void Show(bool isShow)
    {
        base.Show(isShow);
        if (totalMoveTExt && GameManager.Ins)
            totalMoveTExt.text = GameManager.Ins.TotalMoving.ToString();
        if (bestMoveText)
            bestMoveText.text = Prefs.bestMove.ToString();
    }
    public void Continue()
    {
        SceneManager.sceneLoaded += OnSceneLoadedEvent;
        if (SceneController.Ins)
            SceneController.Ins.LoadSceneCurent();
    }
    private void OnSceneLoadedEvent(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.Ins)
            GameManager.Ins.PlayGame();
        SceneManager.sceneLoaded -= OnSceneLoadedEvent;

    }
}
