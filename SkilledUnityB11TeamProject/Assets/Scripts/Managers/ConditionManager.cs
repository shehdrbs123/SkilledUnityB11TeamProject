using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Condition
{
    public float curValue;
    public float maxValue;
    public float startValue;
    public float decayRate;
    public Image uiBar;

    public void Initalize()
    {
        curValue = startValue;
    }

    public void Change(float amount)
    {
        curValue = Mathf.Clamp(curValue + amount, 0f, 100f);
        uiBar.fillAmount = curValue / maxValue;
    }

    public bool IsZero()
    {
        return curValue <= 0.0f;
    }

    public void Decay()
    {
        Change(-1 * decayRate * Time.deltaTime);
    }
}

public class ConditionManager : MonoBehaviour
{
    [Header("Game Stat")]
    public Condition hunger;
    public Condition thirsty;
    public Condition mental;

    [Header("Life")]
    public int battery;
    public List<Image> uiBattery = new List<Image>();

    private readonly List<GameObject> batteries = new List<GameObject>();

    private DayManager dayManager;

    private void Awake()
    {
        dayManager = GameManager.Instance._dayManager;
    }

    private void Start()
    {
        hunger.Initalize();
        thirsty.Initalize();
        mental.Initalize();
        battery = 4;

        for (int i = 0; i < battery; i++)
        {
            GameObject obj = GameManager.Instance.prefabManager.SpawnFromPool(PoolType.Battery);
            obj.transform.position += new Vector3(0, 0, -3 * i);
            obj.SetActive(true);
            batteries.Add(obj);
        }
    }

    private void Update()
    {
        hunger.Decay();
        thirsty.Decay();

        if (hunger.IsZero() || thirsty.IsZero())
        {
            mental.Decay();
        }

        if (mental.IsZero())
        {
            GameOver();
        }

        if (dayManager.day >= 7)
        {
            GameClear();
        }
    }

    public void GetDamaged()
    {
        if (battery > 0)
        {
            battery -= 1;
            StartCoroutine(CoDisapear(batteries[battery]));
            uiBattery[battery].gameObject.SetActive(false);
        }
        else
        {
            GameOver();
        }
    }

    private IEnumerator CoDisapear(GameObject battery)
    {
        while (battery.transform.position.y > -4.5f)
        {
            battery.transform.Translate(Vector3.down * 0.05f);
            yield return null;
        }

        battery.SetActive(false);
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
        // 게임 오버 씬
    }

    private void GameClear()
    {
        Debug.Log("GAME CLEAR");
        // 게임 클리어 씬
    }
}
