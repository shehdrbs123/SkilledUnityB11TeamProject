using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneScript : MonoBehaviour
{
    public void OnButtonStart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("IntroScene");
    }

    public void OnButtonExit()
    {
        Application.Quit();
    }

    public void OnButtonMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}
