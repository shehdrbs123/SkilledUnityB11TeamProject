using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> texts;
    [SerializeField] private GameObject buttonNext;
    [SerializeField] private GameObject buttonPrev;
    [SerializeField] private GameObject buttonGameStart;

    private int index;

    private void Start()
    {
        index = 0;
        OnButton(0);
    }

    private void ShowText(int index)
    {
        foreach (GameObject text in texts)
        {
            text.SetActive(false);
        }

        texts[index].SetActive(true);
    }

    public void OnButton(int change)
    {
        index += change;

        buttonNext.SetActive(index < texts.Count - 1);
        buttonPrev.SetActive(index > 0);
        buttonGameStart.SetActive(index == texts.Count - 1);

        ShowText(index);
    }

    public void OnButtonGameStart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
