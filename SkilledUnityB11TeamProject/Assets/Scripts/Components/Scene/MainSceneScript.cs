using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneScript : MonoBehaviour
{
    public void OnButtonStart()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void OnButtonExit()
    {
        Application.Quit();
    }
}
