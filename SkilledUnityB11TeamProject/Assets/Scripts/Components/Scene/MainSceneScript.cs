using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnButtonMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}
