using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    // Start is called before the first frame update
    public override void Awake()
    {
        MakeSingleton(false);
    }
    public void LoadSceneCurent()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
